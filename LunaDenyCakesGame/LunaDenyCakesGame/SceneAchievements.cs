using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using NetSfmlLib;

namespace LunaDenyCakesGame
{    
    public class SceneAchievements : Scene
    {
        // Ресурсы и константы
        private Text text;
        private int TOP = 250;
        private int STEP = 54;
        private int BUTBACK_X;
        private int BUTRESET_X;
        private int BUT_Y = 730;
        private Sprite ok;
        private Sprite cancel;

        public override void Init()
        {
            text = new Text("", CommonData.font, 22);                        
            BUTBACK_X = ObjModule.opt.getWindowWidth() / 2 - 384 / 2 + 188 / 2;
            BUTRESET_X = ObjModule.opt.getWindowWidth() / 2 + 384 / 2 - 188 / 2;
            ok = SfmlHelper.LoadSprite(@"images/ok.png");
            cancel = SfmlHelper.LoadSprite(@"images/cancel.png");
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
                        setNextScene(new SceneMenu());
                        return SceneResult.Switch;
                    }
                
                if (args.e is MouseButtonEventArgs mouseButtonEventArgs)
                    if (mouseButtonEventArgs.Button == Mouse.Button.Left)
                    {                           
                        if (isMousePosOverSmallButton(BUTBACK_X, BUT_Y))
                        {
                            setNextScene(new SceneMenu());
                            return SceneResult.Switch;
                        }
                        if (isMousePosOverSmallButton(BUTRESET_X, BUT_Y))
                        {
                            ObjModule.achievementstore.ResetAchievements();
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
                rect.Size = new Vector2f(ObjModule.opt.getWindowWidth() - 200, 450);
                rect.Position = new Vector2f(100, 180);
                rect.FillColor = new Color(40, 40, 40, 128);
                window.Draw(rect);
            }

            // Рендер пунктов меню
            for (int i = 0; i < ObjModule.achievementstore.getCount(); i++)
            {                
                DrawTextCentered(window, text, 
                    ObjModule.achievementstore.getName(i), 
                    ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i - 20);                
                DrawAt(window, ObjModule.achievementstore.isCompleted(i)?ok:cancel, 
                    ObjModule.opt.getWindowWidth() / 2 - text.GetLocalBounds().Width / 2 - 56, TOP + STEP * i - 28);                
            }

            if (isMousePosOverSmallButton(BUTBACK_X,BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTBACK_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menuback"), BUTBACK_X, BUT_Y - 16);

            if (isMousePosOverSmallButton(BUTRESET_X, BUT_Y))
                CommonData.button_small.Color = CommonData.color_over;
            else
                CommonData.button_small.Color = CommonData.color_norm;
            DrawAt(window, CommonData.button_small, BUTRESET_X, BUT_Y);
            DrawTextCentered(window, text, ObjModule.texts.getText("menureset"), BUTRESET_X, BUT_Y - 16);

            DrawAt(window, CommonData.logo, ObjModule.opt.getWindowWidth() / 2, 100);

            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
