using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SfmlNetEngine;
using System.IO;

namespace LunaDenyCakesGame
{
    public class SceneHelp : Scene
    {
        // Ресурсы и константы
        private Text text;
        private Text text_main;
        protected List<string> items;
        private int TOP = 700;
        private int STEP = 70;
        private String help;
                
        public override void Init()
        {
            text = new Text("", CommonData.font, 28);
            text_main = new Text("", CommonData.font, 24);
            text_main.FillColor = new Color(255, 255, 255);
            help = File.ReadAllText(@"help.ru.txt");

            text_main.DisplayedString = help;
            text_main.Position = new Vector2f(30, 100);

            items = new List<string>();
            rebuildItems();
        }
                
        private void rebuildItems()
        {
            items.Clear();            
            items.Add(ObjModule.texts.getText("menuback"));
        }

        // Проверка, входит ли курсор в позицию меню
        private bool isMousePosOverButton(int i)
        {
            int mx = getMousePosition().X;
            int my = getMousePosition().Y;

            return ((ObjModule.opt.getWindowWidth() / 2 - CommonData.button.Texture.Size.X / 2 < mx) &&
                    (TOP + STEP * i - CommonData.button.Texture.Size.Y / 2 < my) &&
                    (ObjModule.opt.getWindowWidth() / 2 + CommonData.button.Texture.Size.X / 2 > mx) &&
                    (TOP + STEP * i + CommonData.button.Texture.Size.Y / 2 > my));
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

                if (args.e is KeyEventArgs keyEventArg)
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
                    {
                        if (isMousePosOverButton(0))
                        {
                            setNextScene(new SceneMenu());
                            return SceneResult.Switch;
                        }                        
                    }                                
                }
            }
            return SceneResult.Normal;

        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);

            using (RectangleShape rect = new RectangleShape())
            {
                rect.Origin = new Vector2f(0, 0);
                rect.OutlineThickness = 0;
                rect.Size = new Vector2f(ObjModule.opt.getWindowWidth()-40, 520);
                rect.Position = new Vector2f(20,80);
                rect.FillColor = new Color(40,40,40,128);
                window.Draw(rect);
            }

            window.Draw(text_main);

            // Рендер пунктов меню
            for (int i = 0; i < items.Count; i++)
            {
                // Выделение яркостью
                if (isMousePosOverButton(i))
                    CommonData.button.Color = CommonData.color_over;
                else
                    CommonData.button.Color = CommonData.color_norm;                
                DrawAt(window, CommonData.button, ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i);
                DrawTextCentered(window, text, items[i], ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i - 24);
            }
                        
            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
