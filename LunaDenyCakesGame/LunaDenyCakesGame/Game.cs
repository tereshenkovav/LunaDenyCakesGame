using System;
using System.Collections.Generic;
using SFML.System;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class Zone
    {
        public int y;
        public int left;
        public int right;
    }

    public enum Direction {  No, Left, Right };
    
    public class Chicken
    {
        public int zoneidx;
        public float x;
        public float vx;
        public bool removed;
    }

    public class Laser
    {
        public bool ison;
        public Direction dir;
    }

    public class Cake
    {
        public int zoneidx;
        public float x;
        public int spriteidx;
        public float hp;
        public float shieldleft;
    }

    public delegate void CreateFallenChicken(float x, float y, float vx, float vy);

    public class Game
    {
        public enum GameState { Normal, Win, Fail };

        private List<Zone> zones;
        private List<Chicken> chickens;        
        private List<Cake> cakes;
        private static int ZONEW = 84;
        private static int ZONEH1 = 110-24;
        private static int ZONEH = 110;
        private static int CAKEW = 48;
        private float celestiax;
        private int celestiazoneidx;
        private Direction celestiadir;
        private float lunax;
        private Direction lunadir;
        private int lunazoneidx;
        private float celestiahp;
        private static int PONYW = 30;
        private GameState state;
        private String gameovermsg;
        public Balance balance;
        public float mana;
        private Cake eaten;
        private EventTimer wintimer;
        // Заменить на наблюдателя или глобальный объект создания эффектов
        public CreateFallenChicken procFallenChicken = null;

        private Laser laser;
                
        public Game()
        {
            zones = new List<Zone>();
            zones.Add(new Zone() { y = 90, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 1 * ZONEH, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 2 * ZONEH, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 3 * ZONEH, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 4 * ZONEH, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 5 * ZONEH, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 90 + 6 * ZONEH, left = 50, right = 50 + ZONEW * 11 });

            laser = new Laser() { ison = false };

            chickens = new List<Chicken>();
            
            cakes = new List<Cake>();            
            for (int i=0; i<zones.Count; i++)
            {
                cakes.Add(new Cake() { x = 200 + (ObjModule.rnd.Next(200) - 100), zoneidx = i, spriteidx = ObjModule.rnd.Next(3), 
                    hp = 1.0f, shieldleft = 0.0f });
                cakes.Add(new Cake() { x = 500 + (ObjModule.rnd.Next(200) - 100), zoneidx = i, spriteidx = ObjModule.rnd.Next(3), 
                    hp = 1.0f, shieldleft = 0.0f });
                cakes.Add(new Cake() { x = 800 + (ObjModule.rnd.Next(200) - 100), zoneidx = i, spriteidx = ObjModule.rnd.Next(3), 
                    hp = 1.0f, shieldleft = 0.0f });
            }

            celestiax = (zones[0].left+ zones[0].right)/ 2;
            celestiazoneidx = 0;
            lunax = (zones[6].left + zones[6].right) / 2;
            lunazoneidx = 6;
            lunadir = Direction.Right;
            state = GameState.Normal;
            gameovermsg = "";
            balance = new Balance(ObjModule.opt.getDifficult());
            mana = balance.MaxMana;
            celestiadir = Direction.No;
            eaten = null;
            celestiahp = balance.CelestiaStartHP;
            wintimer = new EventTimer();
        }
        public int getMana()
        {
            return (int)mana;
        }
        public void decMana(int delta)
        {
            if (mana>delta) mana-=delta;
        }
        public Vector2f getCelestiaPos()
        {
            return new Vector2f(celestiax,zones[celestiazoneidx].y);
        }
        public Direction getCelestiaDir()
        {
            return celestiadir;
        }
        public bool isCelestiaEaten()
        {
            return eaten != null;
        }
        public int getCelestiaZoneIdx()
        {
            return celestiazoneidx;
        }
        public int getCelestiaHPin100()
        {
            return (int)(100*(celestiahp/balance.CelestiaStartHP));
        }
        public Vector2f getLunaPos()
        {
            return new Vector2f(lunax, zones[lunazoneidx].y);
        }
        public Direction getLunaDir()
        {
            return lunadir;
        }
        public int getLunaZoneIdx()
        {
            return lunazoneidx;
        }
        public int getZoneCount()
        {
            return zones.Count;
        }
        public Zone getZone(int i)
        {
            return zones[i];
        }
        public int getChickenCount()
        {
            return chickens.Count;
        }
        public Vector2f getChickenPos(int i)
        {
            return new Vector2f(chickens[i].x, zones[chickens[i].zoneidx].y);
        }
        public Direction getChickenDir(int i)
        {
            return (chickens[i].vx<0?Direction.Left:Direction.Right);
        }
        public int getCakeCount()
        {
            return cakes.Count;
        }
        public Vector2f getCakePos(int i)
        {
            return new Vector2f(cakes[i].x, zones[cakes[i].zoneidx].y);
        }
        public int getCakeSpriteIdx(int i)
        {
            return cakes[i].spriteidx;
        }
        public float getCakeHP(int i)
        {
            return cakes[i].hp;
        }
        public bool isCakeShieldOn(int i)
        {
            return cakes[i].shieldleft>0.0f;
        }
        public bool sendLunaLeft(float dt)
        {
            if (laser.ison) return false;

            float newlunax = lunax - balance.LunaVel * dt;
            if (newlunax >= zones[lunazoneidx].left + PONYW / 2)
            {
                lunax = newlunax;
                lunadir = Direction.Left;
                return true;
            }
            else return false;
        }
        public bool sendLunaRight(float dt)
        {
            if (laser.ison) return false;

            float newlunax = lunax + balance.LunaVel * dt;
            if (newlunax <= zones[lunazoneidx].right - PONYW / 2)
            {
                lunax = newlunax;
                lunadir = Direction.Right;
                return true;
            }
            else
                return false;
        }
        public int getZoneByXY(Vector2i mxy)
        {
            for (int i = 0; i < zones.Count; i++)
            {
                if ((zones[i].left < mxy.X) && (mxy.X < zones[i].right) && (zones[i].y > mxy.Y) && (mxy.Y > zones[i].y - ZONEH1)) return i;
            }
            return -1;
        }
        public int getCakeAt(Vector2i mxy)
        {
            for (int i = 0; i < cakes.Count; i++)
            {
                if ((cakes[i].x - CAKEW / 2 < mxy.X) && (cakes[i].x + CAKEW / 2 > mxy.X) &&
                    (zones[cakes[i].zoneidx].y - CAKEW / 2 - 30 < mxy.Y) && (zones[cakes[i].zoneidx].y + CAKEW / 2 - 30 > mxy.Y))
                    return i;
            }
            return -1;
        }
        public bool jumpLunaToXY(Vector2i mxy)
        {
            int idx = getZoneByXY(mxy);
            if (idx == -1) return false;
            lunazoneidx = idx;
            lunax = mxy.X;
            if (lunax < zones[idx].left + PONYW / 2) lunax = zones[idx].left + PONYW / 2;
            if (lunax > zones[idx].right - PONYW / 2) lunax = zones[idx].right - PONYW / 2;
            return true;
        }
        public void jumpCelestiaToBestZone()
        {
            var zonesforjump = new List<int>();
            foreach (var cake in cakes)
                if (cake.shieldleft <= 0.0f) 
                    if (!zonesforjump.Contains(cake.zoneidx)) zonesforjump.Add(cake.zoneidx);
            foreach (var chicken in chickens)
                zonesforjump.Remove(chicken.zoneidx);
            // Если подходящие зоны с кексами и без куриц не найдены, но на зоне Селестии находится курица
            if ((zonesforjump.Count == 0) && chickens.Exists((c) => c.zoneidx == celestiazoneidx)) {
                // Добавляем все зоны, свободные от куриц
                for (int i = 0; i < zones.Count; i++)
                    if (!chickens.Exists((c) => c.zoneidx == i)) zonesforjump.Add(i);
            }
            if (zonesforjump.Count > 0)
                celestiazoneidx = zonesforjump[ObjModule.rnd.Next(zonesforjump.Count)];
        }
        public bool addChicken(Vector2i mxy)
        {
            int idx = getZoneByXY(mxy);
            if (idx == -1) return false;
  
            float chickenx = mxy.X;
            if (chickenx < zones[idx].left + PONYW / 2) chickenx = zones[idx].left + PONYW / 2;
            if (chickenx > zones[idx].right - PONYW / 2) chickenx = zones[idx].right - PONYW / 2;

            float vsig ;
            if (celestiazoneidx == idx)
                vsig = Math.Sign(celestiax - chickenx);
            else
                if (ObjModule.rnd.Next(2) == 1) vsig = 1; else vsig = -1;

            chickens.Add(new Chicken() { x = chickenx, zoneidx = idx, vx = balance.ChickenVel* vsig, removed = false });

            return true;
        }
        public Laser getLaser()
        {
            return laser;
        }
        public void startLaser(Vector2i mxy)
        {
            laser.dir = (mxy.X > lunax) ? Direction.Right : Direction.Left;
            laser.ison = true;
            lunadir = laser.dir;
        }
        public void finishLaser()
        {
            laser.ison = false;
        }
        public bool setShieldToCakeByXY(Vector2i mxy)
        {
            int idx = getCakeAt(mxy);
            if (idx == -1) return false;
            cakes[idx].shieldleft = balance.ShieldTime;
            return true;
        }
        public bool isWin()
        {
            return state==GameState.Win;
        }
        public bool isFail()
        {
            return state == GameState.Fail;
        }
        public String getGameOverMsg()
        {
            return gameovermsg;
        }
        public void Update(float dt)
        {            
            foreach (var cake in cakes)
                if (cake.shieldleft>0) cake.shieldleft -= dt;
            foreach (var chicken in chickens)
                chicken.x += chicken.vx * dt;

            if (laser.ison)
            {
                foreach (var cake in cakes)
                    if ((cake.zoneidx == lunazoneidx) && (cake.shieldleft <= 0.0f))
                    {
                        if ((laser.dir == Direction.Left) && (cake.x <= lunax)) cake.hp -= balance.LaserPowerInSec * dt;
                        else
                        if ((laser.dir == Direction.Right) && (cake.x >= lunax)) cake.hp -= balance.LaserPowerInSec * dt;
                    }
                foreach (var chicken in chickens)
                    if (chicken.zoneidx == lunazoneidx)
                    {
                        if ((laser.dir == Direction.Left) && (chicken.x <= lunax)) chicken.removed = true;
                        else
                        if ((laser.dir == Direction.Right) && (chicken.x >= lunax)) chicken.removed = true;
                    }
                if (celestiazoneidx == lunazoneidx)
                {
                    if ((laser.dir == Direction.Left) && (celestiax <= lunax))
                    {
                        gameovermsg = ObjModule.texts.getText("msg_laserfail");
                        state = GameState.Fail;
                    }
                    if ((laser.dir == Direction.Right) && (celestiax >= lunax))
                    {
                        gameovermsg = ObjModule.texts.getText("msg_laserfail");
                        state = GameState.Fail;
                    }
                }
                mana -= balance.LaserCostInSec * dt;
                if (mana <= 0)
                {
                    mana = 0;
                    laser.ison = false;
                }
            }
            else
            {
                // Заменить на значение с ограничителем
                mana += balance.RegenManaInSec * dt;
                if (mana >= balance.MaxMana) mana = balance.MaxMana;
            }

            int i = 0;
            while (i<chickens.Count)
            {
                if (((chickens[i].vx < 0) && (chickens[i].x < zones[chickens[i].zoneidx].left)) ||
                     ((chickens[i].vx > 0) && (chickens[i].x > zones[chickens[i].zoneidx].right)))
                {
                    if (procFallenChicken != null) procFallenChicken(chickens[i].x, zones[chickens[i].zoneidx].y, chickens[i].vx, 200);
                    chickens.RemoveAt(i);
                }
                else 
                if (chickens[i].removed)
                {
                    chickens.RemoveAt(i);
                }
                else
                    i++;
            }

            // Расчет Селестии
            foreach(var chicken in chickens) 
                if (chicken.zoneidx==celestiazoneidx)
                {
                    jumpCelestiaToBestZone();
                    break;
                }

            eaten = null;
            foreach(var cake in cakes)
                if ((Math.Abs(celestiax-cake.x)<(PONYW/2+CAKEW/2))&&(cake.zoneidx == celestiazoneidx)&&(cake.shieldleft<=0.0f))
                    eaten = cake;

            if (eaten != null)
            {
                celestiadir = (eaten.x - celestiax > 0) ? Direction.Right : Direction.Left;
                float dh = balance.EatInSec * dt;
                if (dh > eaten.hp) dh = eaten.hp;
                eaten.hp -= dh;
                celestiahp -= dh;
            }
            else
            {
                Cake near = null;
                float dist = 99999;
                foreach (var cake in cakes)
                {
                    if ((cake.zoneidx == celestiazoneidx) && (cake.shieldleft <= 0.0f))
                        if (dist > Math.Abs(celestiax - cake.x))
                        {
                            dist = Math.Abs(celestiax - cake.x);
                            near = cake;
                        }
                }
                if (near == null) // Не найден кексик на уровне
                { 
                    celestiadir = Direction.No;
                    jumpCelestiaToBestZone();
                }
                else
                {
                    celestiadir = (near.x - celestiax > 0) ? Direction.Right : Direction.Left;
                    if (celestiadir == Direction.Left) 
                        celestiax -= balance.CelestiaVel * dt;
                    else
                        celestiax += balance.CelestiaVel * dt;
                }
            }

            // Заменить на обработку коллекции с условием удаления
            i = 0;
            while (i < cakes.Count)
            {
                if (cakes[i].hp<=0)
                    cakes.RemoveAt(i);
                else
                    i++;
            }

            if (cakes.Count == 0)
            {
                if (!wintimer.isActive()) wintimer.Start(2.0f, new Action( ()=> {
                    gameovermsg = ObjModule.texts.getText("msg_cakeover");
                    state = GameState.Win;
                }));
            }

            wintimer.Update(dt);

            if (getCelestiaHPin100()<=0)
            {
                gameovermsg = ObjModule.texts.getText("msg_celestiafail");
                state = GameState.Fail;
            }
        }        
    }
}
