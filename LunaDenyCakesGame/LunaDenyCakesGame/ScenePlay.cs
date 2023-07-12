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
        private Sound galop;
        private Sprite action_laser;
        private Sprite action_chicken;
        private Sprite action_jump;
        private float x = 100;
        
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
            action_laser = SfmlHelper.LoadSprite("images/action_laser.png", SpriteLoaderOptions.sloCentered);
            action_chicken = SfmlHelper.LoadSprite("images/action_chicken.png", SpriteLoaderOptions.sloCentered);
            action_jump = SfmlHelper.LoadSprite("images/action_jump.png", SpriteLoaderOptions.sloCentered);
            celestia_walk = new SfmlAnimation("images/celestia_walk.png", 6, 6);
            celestia_walk.Play();
            celestia_eat = new SfmlAnimation("images/celestia_eat.png", 6, 6);
            celestia_eat.Play();
            luna_walk = new SfmlAnimation("images/luna_walk.png", 6, 6);
            luna_walk.Play();
            luna_wait = new SfmlAnimation("images/luna_wait.png", 6, 6);
            luna_wait.Play();
        }

        public override void UnInit()
        {
            galop.Stop();   
        }
        
        public override SceneResult Frame(float dt, IEnumerable<EventArgs> events)
        {
            // Обход событий, для Esc - выход из игры, для мыши - вызов действия меню
            foreach (EventArgs args in events)
            {
                if (args is KeyEventArgs keyEventArg)
                {
                    if (keyEventArg.Code == Keyboard.Key.Escape)
                    {
                        setNextScene(new SceneMenu());
                        return SceneResult.Switch;
                    }
                }

                if (args is MouseButtonEventArgs mouseButtonEventArgs)
                {
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                    {
                        
                    }
                                
                }
            }
            celestia_walk.Update(dt);
            celestia_eat.Update(dt);
            luna_walk.Update(dt);
            luna_wait.Update(dt);

            x += dt * 30;

            return SceneResult.Normal;
        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);
            
            for (int i=0; i<10; i++)
            {
                DrawAt(window, block, 100 + i * 84, 300 + 220);
                DrawAt(window, block, 100 + i * 84, 300 + 110);
                DrawAt(window, block, 100 + i * 84, 300);
            }

            DrawAt(window, cakes[0], 100 + 4 * 84, 300 - 24);
            DrawAt(window, cakes[1], 100 + 3 * 84, 300 + 110 - 24);
            DrawAt(window, cakes[2], 100 + 7 * 84, 300 + 220 - 24);

            DrawAt(window, chicken, 100 + 2 * 84, 300 + 220 - 30);

            DrawAt(window, celestia_walk, x, 300 + 220 - 128);
            DrawAt(window, luna_walk, x, 300 + 110 - 128);

            DrawAt(window, celestia_eat, 500, 300 + 110 - 128);
            DrawAt(window, luna_wait, 800, 300 + 110 - 128);

            // Курсор
            DrawAt(window, action_jump, (Vector2f)getMousePosition());
        }
    }
}
