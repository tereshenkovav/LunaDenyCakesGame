﻿using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using NetSfmlLib;
using System;

namespace LunaDenyCakesGame
{
    public class ScenePlay : Scene
    {
        // Переделать на систему эффектов в движок
        private class Effect
        {
            public float x;
            public float y;
            public float vx;
            public float vy;
            public float angle;
        }
        // Ресурсы и константы
        private Text text;
        private Text text_pause;
        private Sprite block;
        private Sprite chicken;
        private Sprite chickenfallen;
        private Sprite[] cakes;
        private SfmlAnimation celestia_walk;
        private SfmlAnimation celestia_eat;
        private SfmlAnimation luna_walk;
        private SfmlAnimation luna_wait;
        private SfmlAnimation laser;
        private SfmlAnimation shield;
        private Sound snd_galop;
        private Sound snd_galop2;
        private Sound snd_laser;
        private Sound snd_teleport;
        private Sound snd_chicken;
        private Sprite deny;
        private Color[] colorset;
        private Game game;
        private bool islunawalk;
        private bool iscelestiawalk;
        private bool islaseron;
        private int oldcelestiazoneidx;
        private List<GameAction> actions;
        private Dictionary<string,Sprite> actionsprites;
        private int tekaction;        
        private const int INDICATOR_W = 48;
        private List<Effect> effects;
        private float celestia_effect_x;
        private float celestia_effect_y;
        private float celestia_effect_vy;
        private float celestia_effect_vx;
        private bool is_celestia_effect;
        private bool is_celestia_effect_eaten;
        private bool is_celestia_effect_mirror;
        private Color manacolor = new Color(35, 20, 250);
        private Color hpcolor = new Color(240, 240, 240);
        private bool pause;

        public override void Init()
        {
            text = new Text("", CommonData.font, 28);
            block = SfmlHelper.LoadSprite("images/block.png");
            chicken = SfmlHelper.LoadSprite("images/chicken.png", SpriteLoaderOptions.sloCentered);
            chickenfallen = SfmlHelper.LoadSprite("images/chicken.png", SpriteLoaderOptions.sloCentered);
            snd_galop = SfmlHelper.LoadSound("sounds/galop.ogg");
            snd_galop.Loop = true;
            snd_galop2 = SfmlHelper.LoadSound("sounds/galop.ogg");
            snd_galop2.Loop = true;
            snd_laser = SfmlHelper.LoadSound("sounds/laser.ogg");
            snd_laser.Loop = true;
            snd_teleport = SfmlHelper.LoadSound("sounds/teleport.ogg");
            snd_teleport.Loop = false;
            snd_chicken = SfmlHelper.LoadSound("sounds/chicken.ogg");
            snd_chicken.Loop = false;
            cakes = new Sprite[3];
            cakes[0] = SfmlHelper.LoadSprite("images/cake1.png",SpriteLoaderOptions.sloCentered);
            cakes[1] = SfmlHelper.LoadSprite("images/cake2.png", SpriteLoaderOptions.sloCentered);
            cakes[2] = SfmlHelper.LoadSprite("images/cake3.png", SpriteLoaderOptions.sloCentered);            
            deny = SfmlHelper.LoadSprite("images/deny.png", SpriteLoaderOptions.sloCentered);
            celestia_walk = new SfmlAnimation("images/celestia_walk.png", 6, 6);
            celestia_walk.Origin = new Vector2f(celestia_walk.Texture.Size.X / 2, 0);
            celestia_walk.Play();
            celestia_eat = new SfmlAnimation("images/celestia_eat.png", 6, 6);
            celestia_eat.Origin = new Vector2f(celestia_eat.Texture.Size.X / 2, 0);
            celestia_eat.Play();
            luna_walk = new SfmlAnimation("images/luna_walk.png", 6, 6);
            luna_walk.Origin = new Vector2f(luna_walk.Texture.Size.X / 2, 0);
            luna_walk.Play();
            luna_wait = new SfmlAnimation("images/luna_wait.png", 6, 6);
            luna_wait.Origin = new Vector2f(luna_wait.Texture.Size.X / 2, 0);
            luna_wait.Play();
            luna_wait.Origin = new Vector2f(luna_wait.Texture.Size.X / 2, 0);
            luna_wait.Play();
            laser = new SfmlAnimation("images/laser.png", 8, 16);
            laser.Play();
            shield = new SfmlAnimation("images/shield.png", 80, 80, 28, 14);
            shield.Origin = new Vector2f(shield.Texture.Size.X / 2, shield.Texture.Size.Y / 2);
            shield.Play();

            text_pause = new Text("", CommonData.font, 24);
            text_pause.FillColor = new Color(255, 255, 255);
            
            colorset = new Color[] { new Color(255, 0, 0), new Color(255, 128, 0), new Color(255, 255, 0), new Color(0, 255, 0) };

            game = new Game();
            game.procFallenChicken = procFallenChicken;

            actions = new List<GameAction>();
            actions.Add(new GAJump(game));
            actions.Add(new GALaser(game));
            actions.Add(new GAChicken(game));
            actions.Add(new GAShield(game));
            actionsprites = new Dictionary<string, Sprite>();
            actionsprites.Add(actions[0].getCode(), SfmlHelper.LoadSprite("images/action_jump.png", SpriteLoaderOptions.sloCentered));
            actionsprites.Add(actions[1].getCode(), SfmlHelper.LoadSprite("images/action_laser.png", SpriteLoaderOptions.sloCentered));
            actionsprites.Add(actions[2].getCode(), SfmlHelper.LoadSprite("images/action_chicken.png", SpriteLoaderOptions.sloCentered));
            actionsprites.Add(actions[3].getCode(), SfmlHelper.LoadSprite("images/action_shield.png", SpriteLoaderOptions.sloCentered));
            tekaction = 0;
            
            effects = new List<Effect>();

            islunawalk = false;
            iscelestiawalk = false;
            islaseron = false;
            oldcelestiazoneidx = game.getCelestiaZoneIdx();

            is_celestia_effect = false;

            ObjModule.achievementstore.ResetDetector();

            pause = false;

            // Добавить пул звуков для управления
            snd_galop.Volume = ObjModule.opt.isSoundOn() ? 100.0f : 0.0f;
            snd_galop2.Volume = ObjModule.opt.isSoundOn() ? 100.0f : 0.0f;
            snd_laser.Volume = ObjModule.opt.isSoundOn() ? 100.0f : 0.0f;
            snd_teleport.Volume = ObjModule.opt.isSoundOn() ? 100.0f : 0.0f;
            snd_chicken.Volume = ObjModule.opt.isSoundOn() ? 100.0f : 0.0f;
        }

        public override void UnInit()
        {
            snd_galop.Stop();
            snd_galop2.Stop();
            snd_laser.Stop();
            snd_teleport.Stop();
            snd_chicken.Stop();
        }

        private void processAction(bool isreleased)
        {
            if (isreleased)
            {
                foreach (var action in actions)
                    action.Finish();
            }
            else
            {
                if (actions[tekaction].Apply(getMousePosition()))
                {                    
                    // Тоже переделать на наблюдателя или коды эффектов
                    if (actions[tekaction] is GAJump) snd_teleport.Play();
                    if (actions[tekaction] is GAChicken) snd_chicken.Play();
                }
            }
        }

        public override SceneResult Frame(float dt, IEnumerable<EventArgsEx> events)
        {
            string actionname = "";

            if (pause)
            {
                foreach (EventArgsEx args in events)
                    if ((args.e is KeyEventArgs keyEventArg) && (!args.released)) 
                    { 
                        if (keyEventArg.Code == Keyboard.Key.Escape) pause = false;
                        if (keyEventArg.Code == Keyboard.Key.F10)
                        {
                            setNextScene(new SceneMenu());
                            return SceneResult.Switch;
                        }
                    }
                return SceneResult.Normal;
            }

            // Обход событий, для Esc - выход из игры, для мыши - вызов действия меню
            foreach (EventArgsEx args in events)
            {
                if ((args.e is KeyEventArgs keyEventArg) && (!args.released))
                    if (keyEventArg.Code == Keyboard.Key.Escape)
                    {
                        pause = true;
                        return SceneResult.Normal;
                    }

                if (ObjModule.opt.keyconfig.isMatchEvent(args.e, ref actionname))
                {
                    if ((actionname == CommonData.action_switch) && (!args.released)) tekaction = (tekaction + 1) % actions.Count;

                    if (CustomOptions.customopt.isApplyAfterSelect())
                    {
                        int keyactionidx = -1;
                        if (actionname == CommonData.action_sel_teleport) keyactionidx = 0;
                        if (actionname == CommonData.action_sel_laser) keyactionidx = 1;
                        if (actionname == CommonData.action_sel_chicken) keyactionidx = 2;
                        if (actionname == CommonData.action_sel_shield) keyactionidx = 3;
                        if (keyactionidx != -1)
                        {
                            tekaction = keyactionidx;
                            processAction(args.released);
                        }
                    }
                    else
                    {
                        if ((actionname == CommonData.action_sel_teleport) && (!args.released)) tekaction = 0;
                        if ((actionname == CommonData.action_sel_laser) && (!args.released)) tekaction = 1;
                        if ((actionname == CommonData.action_sel_chicken) && (!args.released)) tekaction = 2;
                        if ((actionname == CommonData.action_sel_shield) && (!args.released)) tekaction = 3;
                        if (actionname == CommonData.action_apply) processAction(args.released);
                    }
                }
            }

            // Переделать на паттерн наблюдатель для звуковых эффектов
            bool newlunawalk = false;
            if (ObjModule.opt.keyconfig.isMatchState(ref actionname)) 
            {
                if (actionname == CommonData.action_left) newlunawalk = game.sendLunaLeft(dt);
                if (actionname == CommonData.action_right) newlunawalk = game.sendLunaRight(dt);
            }
            if ((newlunawalk) && (!islunawalk)) snd_galop.Play();
            if ((!newlunawalk) && (islunawalk)) snd_galop.Stop();
            islunawalk = newlunawalk;

            bool newcelestiawalk = (!game.isCelestiaEaten()) && (game.getCelestiaDir()!=Direction.No);
            if ((newcelestiawalk) && (!iscelestiawalk)) snd_galop2.Play();
            if ((!newcelestiawalk) && (iscelestiawalk)) snd_galop2.Stop();
            iscelestiawalk = newcelestiawalk;

            if (oldcelestiazoneidx!=game.getCelestiaZoneIdx())
            {
                snd_teleport.Play();
                oldcelestiazoneidx = game.getCelestiaZoneIdx();
            }

            if ((game.getLaser().ison) && (!islaseron)) snd_laser.Play();
            if ((!game.getLaser().ison) && (islaseron)) snd_laser.Stop();
            islaseron = game.getLaser().ison;

            celestia_walk.Update(dt);
            celestia_eat.Update(dt);
            luna_walk.Update(dt);
            luna_wait.Update(dt);
            laser.Update(dt);
            shield.Update(dt);

            if ((!game.isFail()) && (!game.isWin()))
            {
                game.Update(dt);
                // Обязательно сразу после обновления игры
                ObjModule.achievementstore.Update(game);
            }

            if (game.isFail())
            {
                if (!is_celestia_effect)
                {
                    // Здесь должны быть созданы эффекты самой игрой
                    if (game.getCelestiaHPin100() <= 0)
                    {
                        celestia_effect_vx = 0;
                        celestia_effect_vy = 0;
                        is_celestia_effect_eaten = true;
                    }
                    else
                    {
                        celestia_effect_vx = 100 * Math.Sign(game.getCelestiaPos().X - game.getLunaPos().X);
                        celestia_effect_vy = -100;
                        is_celestia_effect_eaten = false;
                    }
                    celestia_effect_x = game.getCelestiaPos().X;
                    celestia_effect_y = game.getCelestiaPos().Y - 128;
                    is_celestia_effect_mirror = game.getCelestiaDir() == Direction.Left;
                    is_celestia_effect = true;
                }
                else
                {
                    celestia_effect_y += celestia_effect_vy * dt;
                    celestia_effect_x += celestia_effect_vx * dt;
                    celestia_effect_vy += 800 * dt;
                    if (celestia_effect_y>ObjModule.opt.getWindowHeigth()+100)
                    {
                        setNextScene(new SceneGameOver(false,game.getGameOverMsg()));
                        return SceneResult.Switch;
                    }
                }
            }
            if (game.isWin())
            {
                setNextScene(new SceneGameOver(true, game.getGameOverMsg()));
                return SceneResult.Switch;
            }

            foreach (var effect in effects)
            {
                effect.x += effect.vx * dt;
                effect.y += effect.vy * dt;
                effect.angle += 2*effect.vx * dt;
            }

            int k = 0;
            while (k < effects.Count)
                if (effects[k].y > ObjModule.opt.getWindowHeigth() + 100)
                    effects.RemoveAt(k);
                else
                    k++;

            return SceneResult.Normal;
        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);
            
            for (int i=0; i<game.getZoneCount(); i++)
            {
                int n = (game.getZone(i).right-game.getZone(i).left)/84;
                for (int j=0; j<n; j++)
                    DrawAt(window, block, game.getZone(i).left+j*84, game.getZone(i).y);
            }
                        
            for (int i = 0; i < game.getChickenCount(); i++)
                DrawMirrHorzAt(window, chicken, game.getChickenPos(i).X,game.getChickenPos(i).Y - 30, game.getChickenDir(i)==Direction.Left);

            if (!game.isFail())
            {
                var celestia_sprite = (game.isCelestiaEaten() || (game.getCelestiaDir() == Direction.No)) ? celestia_eat : celestia_walk;
                DrawMirrHorzAt(window, celestia_sprite,
                        game.getCelestiaPos().X, game.getCelestiaPos().Y - 128, game.getCelestiaDir() == Direction.Left);
            }

            DrawMirrHorzAt(window, islunawalk ? luna_walk : luna_wait,
                    game.getLunaPos().X, game.getLunaPos().Y - 126,game.getLunaDir()==Direction.Left);

            for (int i = 0; i < game.getCakeCount(); i++)
            {
                DrawAt(window, cakes[game.getCakeSpriteIdx(i)], game.getCakePos(i).X, game.getCakePos(i).Y - 30);
                if (game.isCakeShieldOn(i))
                    DrawAt(window, shield, game.getCakePos(i).X, game.getCakePos(i).Y - 30);
                if (game.getCakeHP(i) < 1.0f)
                    DrawIndicator(window, game.getCakePos(i).X - INDICATOR_W / 2, game.getCakePos(i).Y, INDICATOR_W, 8, game.getCakeHP(i), colorset);
            }

            if (game.getLaser().ison)
            {
                if (game.getLaser().dir == Direction.Right)
                {
                    float start = game.getLunaPos().X+30;
                    while (start<ObjModule.opt.getWindowWidth()+laser.Texture.Size.X)
                    {
                        DrawAt(window, laser, start, game.getLunaPos().Y - 80);
                        start += laser.Texture.Size.X;
                    }
                }
                else
                {
                    float start = game.getLunaPos().X - 30 - laser.Texture.Size.X;
                    while (start > -laser.Texture.Size.X)
                    {
                        DrawAt(window, laser, start, game.getLunaPos().Y - 80);
                        start -= laser.Texture.Size.X;
                    }
                }
            }

            foreach (var effect in effects)
            {
                chickenfallen.Rotation = effect.angle;
                DrawMirrHorzAt(window, chickenfallen, effect.x, effect.y - 30,effect.vx<0);
            }

            if (is_celestia_effect)
            {
                DrawMirrHorzAt(window, is_celestia_effect_eaten ? celestia_eat : celestia_walk,
                   celestia_effect_x, celestia_effect_y, is_celestia_effect_mirror);
            }

            text.FillColor = manacolor;
            DrawTextCentered(window, text, game.getMana().ToString(), ObjModule.opt.getWindowWidth() - 40, 5);
            
            text.FillColor = hpcolor;
            DrawTextCentered(window, text, game.getCelestiaHPin100().ToString(), 25, 5);

            using (RectangleShape rect = new RectangleShape())
            {
                rect.Origin = new Vector2f(0, 0);
                rect.OutlineThickness = 0;
                rect.Size = new Vector2f(30, 700*(float)game.getMana()/(float)game.balance.MaxMana);
                rect.Position = new Vector2f(ObjModule.opt.getWindowWidth() - 40, ObjModule.opt.getWindowHeigth()-rect.Size.Y);
                rect.FillColor = manacolor;
                window.Draw(rect);
            }

            using (RectangleShape rect = new RectangleShape())
            {
                rect.Origin = new Vector2f(0, 0);
                rect.OutlineThickness = 0;
                rect.Size = new Vector2f(30, 7 * game.getCelestiaHPin100());
                rect.Position = new Vector2f(10, ObjModule.opt.getWindowHeigth() - rect.Size.Y);
                rect.FillColor = hpcolor;
                window.Draw(rect);
            }

            if (pause)
            {
                using (RectangleShape rect = new RectangleShape())
                {
                    rect.Origin = new Vector2f(0, 0);
                    rect.OutlineThickness = 0;
                    rect.Size = new Vector2f(300, 180);
                    rect.Position = new Vector2f(ObjModule.opt.getWindowWidth() / 2 - 150, 240);
                    rect.FillColor = new Color(40, 40, 40, 128);
                    window.Draw(rect);
                }
                DrawTextEveryLineCentered(window, text_pause, ObjModule.texts.getText("text_pause"), ObjModule.opt.getWindowWidth() / 2, 290);
            }

            // Курсор
            if (!actions[tekaction].isAllowed(getMousePosition()))
                DrawAt(window, deny, (Vector2f)getMousePosition());
            DrawAt(window, actionsprites[actions[tekaction].getCode()], (Vector2f)getMousePosition());
        }

        private void procFallenChicken(float x, float y, float vx, float vy)
        {
            effects.Add(new Effect() { x = x, y = y, vx = vx, vy = vy, angle = 0.0f });
        }
    }
}
