using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using NetSfmlLib;

namespace LunaDenyCakesGame
{    
    public class SceneConfirmExit : Scene
    {
        // Ресурсы и константы
        private Text text;
        private int BUTBACK_X;
        private int BUTEXIT_X;
        private int BUT_Y = 420;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 22);
            BUTBACK_X = ObjModule.opt.getWindowWidth() / 2 - 384 / 2 + 188 / 2;
            BUTEXIT_X = ObjModule.opt.getWindowWidth() / 2 + 384 / 2 - 188 / 2;            
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
            foreach (EventArgsEx args in events)
            {
                if (args.released) continue;

                if (args.e is KeyEventArgs keyEventArg)
                    if (keyEventArg.Code == Keyboard.Key.Escape)
                    {
                        return SceneResult.Switch;
                    }
                
                if (args.e is MouseButtonEventArgs mouseButtonEventArgs)
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                    {
                        if (isMousePosOverSmallButton(BUTBACK_X, BUT_Y))
                        {
                            return SceneResult.Switch;
                        }
                        if (isMousePosOverSmallButton(BUTEXIT_X, BUT_Y))
                        {
                            return SceneResult.Exit;
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
                rect.Size = new Vector2f(440, 180);
                rect.Position = new Vector2f(ObjModule.opt.getWindowWidth() / 2 - 220, 280);
                rect.FillColor = new Color(40, 40, 40, 128);
                window.Draw(rect);
            }

            DrawTextCentered(window, text, ObjModule.texts.getText("text_confirm_exit"), ObjModule.opt.getWindowWidth() / 2, 320);

            if (isMousePosOverSmallButton(BUTBACK_X,BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTBACK_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menuback"), BUTBACK_X, BUT_Y - 16);

            if (isMousePosOverSmallButton(BUTEXIT_X, BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTEXIT_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menuquit"), BUTEXIT_X, BUT_Y - 16);
            
            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
