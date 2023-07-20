using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class SceneGameOver : Scene
    {
        // Ресурсы и константы
        private Text text;
        private Text text_main;
        protected List<string> items;
        private int TOP = 300;
        private int STEP = 70;        
        private bool iswin;
        private String msg;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 28);
            text_main = new Text("", CommonData.font, 36);
            text_main.FillColor = iswin ? new Color(70, 160, 0): new Color(200, 0, 0);
            items = new List<string>();
            rebuildItems();
        }

        public SceneGameOver(bool iswin, String msg)
        {
            this.iswin = iswin;
            this.msg = msg;
        }

        private void rebuildItems()
        {
            items.Clear();
            items.Add(ObjModule.texts.getText("menurestart"));
            items.Add(ObjModule.texts.getText("menuexit"));
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
                    if (keyEventArg.Code == Keyboard.Key.Escape) return SceneResult.Exit;
                }

                if (args.e is MouseButtonEventArgs mouseButtonEventArgs)
                {
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                    {
                        if (isMousePosOverButton(0))
                        {
                            setNextScene(new ScenePlay());
                            return SceneResult.Switch;
                        }                        
                        if (isMousePosOverButton(1)) return SceneResult.Exit;
                    }                                
                }
            }
            return SceneResult.Normal;

        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);

            DrawTextCentered(window, text_main,
                iswin ? ObjModule.texts.getText("text_win") : ObjModule.texts.getText("text_fail"),
                ObjModule.opt.getWindowWidth() / 2, 100);

            DrawTextCentered(window, text_main, msg, ObjModule.opt.getWindowWidth() / 2, 150);

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
