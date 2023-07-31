using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{    
    public class SceneCtrl : Scene
    {
        // Ресурсы и константы
        private Text text;        
        private int TOP = 250;
        private int STEP = 54;
        private int BUTBACK_X;
        private int BUTDEF_X;
        private int BUT_Y = 730;
        private KeyConfig keyconfig;
        private int active_idx = -1;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 22);            
            keyconfig = ObjModule.opt.keyconfig;
            BUTBACK_X = ObjModule.opt.getWindowWidth() / 2 - 384 / 2 + 188 / 2;
            BUTDEF_X = ObjModule.opt.getWindowWidth() / 2 + 384 / 2 - 188 / 2;
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
            // Запись изменений клавиш выполняется здесь
        }
        
        public override SceneResult Frame(float dt, IEnumerable<EventArgsEx> events)
        {
            if (active_idx != -1)
            {
                foreach (EventArgsEx args in events)
                {
                    if (args.released) continue;
                    if (keyconfig.setKeyByEvent(active_idx, args.e))
                    {
                        active_idx = -1;
                        break;
                    }
                }
            }
            else
            // Обход событий, для Esc - выход из игры, для мыши - вызов действия меню
            foreach (EventArgsEx args in events)
            {
                if (args.released) continue;

                if (args.e is KeyEventArgs keyEventArg)
                    if (keyEventArg.Code == Keyboard.Key.Escape)
                    {
                        setNextScene(new SceneMenu());
                        return SceneResult.Switch;
                    }
                
                if (args.e is MouseButtonEventArgs mouseButtonEventArgs)
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                    {
                        for (int i=0; i<keyconfig.getCount(); i++)
                            if (isMousePosOverButton(i))
                                active_idx = i;
                            
                        if (isMousePosOverSmallButton(BUTBACK_X, BUT_Y))
                        {
                            setNextScene(new SceneMenu());
                            return SceneResult.Switch;
                        }
                        if (isMousePosOverSmallButton(BUTDEF_X, BUT_Y))
                        {
                            keyconfig.setDefault();
                        }
                    }                
            }
            return SceneResult.Normal;

        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);

            // Рендер пунктов меню
            for (int i = 0; i < keyconfig.getCount(); i++)
            {
                // Выделение яркостью
                if (isMousePosOverButton(i))
                    CommonData.button.Color = CommonData.color_over;
                else
                    CommonData.button.Color = CommonData.color_norm;                
                DrawAt(window, CommonData.button, ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i);
                DrawTextCentered(window, text, 
                    ObjModule.texts.getText(keyconfig.getActionName(i))+" : "+(active_idx==i?"  ":keyconfig.getKeyView(i)), 
                    ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i - 20);
            }

            if (isMousePosOverSmallButton(BUTBACK_X,BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTBACK_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menuback"), BUTBACK_X, BUT_Y - 16);

            if (isMousePosOverSmallButton(BUTDEF_X, BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTDEF_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menudefault"), BUTDEF_X, BUT_Y - 16);

            DrawAt(window, CommonData.logo, ObjModule.opt.getWindowWidth() / 2, 100);

            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
