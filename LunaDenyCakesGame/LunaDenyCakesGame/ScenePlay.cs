using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class ScenePlay : Scene
    {
        // Ресурсы и константы
        private Text text;
        private Sprite block;
        private Sprite chicken;
        private Sprite[] cakes;
        private SfmlAnimation celestia_walk;
        private SfmlAnimation celestia_eat;
        private SfmlAnimation luna_walk;
        private SfmlAnimation luna_wait;
        private SfmlAnimation laser;
        private Sound galop;        
        private Sprite deny;
        private Color[] colorset;
        private Game game;
        private bool islunawalk;        
        private List<GameAction> actions;
        private Dictionary<string,Sprite> actionsprites;
        private int tekaction;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 28);
            block = SfmlHelper.LoadSprite("images/block.png");
            chicken = SfmlHelper.LoadSprite("images/chicken.png", SpriteLoaderOptions.sloCentered);
            galop = SfmlHelper.LoadSound("sounds/galop.ogg");
            galop.Loop = true;
            cakes = new Sprite[3];
            cakes[0] = SfmlHelper.LoadSprite("images/cake1.png",SpriteLoaderOptions.sloCentered);
            cakes[1] = SfmlHelper.LoadSprite("images/cake2.png", SpriteLoaderOptions.sloCentered);
            cakes[2] = SfmlHelper.LoadSprite("images/cake3.png", SpriteLoaderOptions.sloCentered);            
            deny = SfmlHelper.LoadSprite("images/deny.png");
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
            laser = new SfmlAnimation("images/laser.png", 8, 16);
            laser.Play();
            luna_wait.Origin = new Vector2f(luna_wait.Texture.Size.X / 2, 0);
            luna_wait.Play();

            colorset = new Color[] { new Color(255, 0, 0), new Color(255, 128, 0), new Color(255, 255, 0), new Color(0, 255, 0) };
                        
            game = new Game();

            actions = new List<GameAction>();
            actions.Add(new GAJump(game));
            actions.Add(new GAChicken(game));
            actions.Add(new GALaser(game));
            actionsprites = new Dictionary<string, Sprite>();
            actionsprites.Add(actions[0].getCode(),SfmlHelper.LoadSprite("images/action_jump.png"));
            actionsprites.Add(actions[1].getCode(),SfmlHelper.LoadSprite("images/action_chicken.png"));
            actionsprites.Add(actions[2].getCode(), SfmlHelper.LoadSprite("images/action_laser.png"));
            tekaction = 0;

            islunawalk = false;            
        }

        public override void UnInit()
        {
            galop.Stop();   
        }
        
        public override SceneResult Frame(float dt, IEnumerable<EventArgsEx> events)
        {
            // Обход событий, для Esc - выход из игры, для мыши - вызов действия меню
            foreach (EventArgsEx args in events)
            {                
                if ((args.e is KeyEventArgs keyEventArg)&&(!args.released))
                {
                    if (keyEventArg.Code == Keyboard.Key.Escape)
                    {
                        setNextScene(new SceneMenu());
                        return SceneResult.Switch;
                    }
                }

                if (args.e is MouseButtonEventArgs mouseButtonEventArgs)
                {
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                        if (args.released)
                            actions[tekaction].Finish();
                        else
                            actions[tekaction].Apply(getMousePosition());
                    if ((mouseButtonEventArgs.Button == Mouse.Button.Right) && (!args.released))
                        tekaction = (tekaction + 1) % actions.Count;                    
                }
            }

            islunawalk = false;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left)|| Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                islunawalk = game.sendLunaLeft(dt);                
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) || Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                islunawalk = game.sendLunaRight(dt);                
            }
            celestia_walk.Update(dt);
            celestia_eat.Update(dt);
            luna_walk.Update(dt);
            luna_wait.Update(dt);
            laser.Update(dt);

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

            DrawAt(window, cakes[0], 100 + 4 * 84, 300 - 24);
            DrawAt(window, cakes[1], 100 + 3 * 84, 300 + 110 - 24);
            DrawAt(window, cakes[2], 100 + 7 * 84, 300 + 220 - 24);

            float v = 0.33f;
            DrawIndicator(window, 100 + 4 * 84 - 24, 300 - 24 + 24, 48, 8, v, colorset);

            for (int i = 0; i < game.getChickenCount(); i++)
                DrawAt(window, chicken, game.getChickenPos(i).X,game.getChickenPos(i).Y - 30);
                        
            DrawMirrHorzAt(window, game.isCelestiaEat()?celestia_eat:celestia_walk, 
                    game.getCelestiaPos().X,game.getCelestiaPos().Y-128,false);

            DrawMirrHorzAt(window, islunawalk ? luna_walk : luna_wait,
                    game.getLunaPos().X, game.getLunaPos().Y - 126,game.getLunaDir()==Direction.Left);

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

            // Курсор
            if (!actions[tekaction].isAllowed(getMousePosition()))
                DrawAt(window, deny, (Vector2f)getMousePosition());
            DrawAt(window, actionsprites[actions[tekaction].getCode()], (Vector2f)getMousePosition());
        }
    }
}
