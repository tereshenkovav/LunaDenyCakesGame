using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using NetSfmlLib;

namespace LunaDenyCakesGame
{
    public class SceneStart : Scene
    {
        // Ресурсы и константы
        private Sprite intro;
        private float t;

        public override void Init()
        {
            t = 0.0f;
            intro = SfmlHelper.LoadSprite(@"images/intro.png");
            intro.Position = new Vector2f(0, 0);
        }

        public override void UnInit()
        {
            
        }
        
        public override SceneResult Frame(float dt, IEnumerable<EventArgsEx> events)
        {
            // Обход событий, для Esc - выход из игры, для мыши - вызов действия меню
            foreach (EventArgsEx args in events)
            {
                if (args.released) continue;

                if (args.e is KeyEventArgs)
                {
                    setNextScene(new SceneMenu());
                    return SceneResult.Switch;                        
                }

                if (args.e is MouseButtonEventArgs)
                {
                    setNextScene(new SceneMenu());
                    return SceneResult.Switch;
                }
            }
            t += dt;
            if (t>=5.0f)
            {
                setNextScene(new SceneMenu());
                return SceneResult.Switch;
            }
            return SceneResult.Normal;
        }

        public override void Render(RenderWindow window)
        {
            if ((int)(100 * t) < 255)
                intro.Color = SfmlHelper.createSFMLColor(255, 255, 255, (int)(100 * t));
            else
                intro.Color = SfmlHelper.createSFMLColor(255, 255, 255, 255);
            window.Draw(intro);
        }
    }
}
