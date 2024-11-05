using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using NetSfmlLib;

namespace LunaDenyCakesGame
{
    public class SceneGameOver : Scene
    {
        // Ресурсы и константы
        private Text text;
        private Text text_main;
        private int BUTRESTART_X;
        private int BUTMENU_X;
        private int BUT_Y = 730;
        private bool iswin;
        private String msg;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 22);
            text_main = new Text("", CommonData.font, 36);
            text_main.FillColor = iswin ? new Color(70, 255, 0): new Color(255, 0, 0);
            BUTRESTART_X = ObjModule.opt.getWindowWidth() / 2 - 384 / 2 + 188 / 2;
            BUTMENU_X = ObjModule.opt.getWindowWidth() / 2 + 384 / 2 - 188 / 2;
        }

        public SceneGameOver(bool iswin, String msg)
        {
            this.iswin = iswin;
            this.msg = msg;
        }

        // Проверка, входит ли курсор в позицию меню
        private bool isMousePosOverSmallButton(int x, int y)
        {
            int mx = getMousePosition().X;
            int my = getMousePosition().Y;

            return ((x - CommonData.button_small.Texture.Size.X / 2 < mx) &&
                    (y - CommonData.button_small.Texture.Size.Y / 2 < my) &&
                    (x + CommonData.button_small.Texture.Size.X / 2 > mx) &&
                    (y + CommonData.button_small.Texture.Size.Y / 2 > my));
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
                        if (isMousePosOverSmallButton(BUTRESTART_X, BUT_Y))
                        {
                            setNextScene(new ScenePlay());
                            return SceneResult.Switch;
                        }
                        if (isMousePosOverSmallButton(BUTMENU_X, BUT_Y))
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
                rect.Size = new Vector2f(ObjModule.opt.getWindowWidth() - 400, 300);
                rect.Position = new Vector2f(200, 100);
                rect.FillColor = new Color(40, 40, 40, 128);
                window.Draw(rect);
            }

            DrawTextCentered(window, text_main,
                iswin ? ObjModule.texts.getText("text_win") : ObjModule.texts.getText("text_fail"),
                ObjModule.opt.getWindowWidth() / 2, 150);

            DrawTextCentered(window, text_main, msg, ObjModule.opt.getWindowWidth() / 2, 200);

            // Рендер пунктов меню
            if (isMousePosOverSmallButton(BUTRESTART_X, BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTRESTART_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menurestart"), BUTRESTART_X, BUT_Y - 16);

            if (isMousePosOverSmallButton(BUTMENU_X, BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTMENU_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menumenu"), BUTMENU_X, BUT_Y - 16);

            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
