using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    // Абстрактный класс меню - содержит ресурсы и шаблоны метода
    public class SceneMenu : Scene
    {
        // Ресурсы и константы
        private Text text;
        protected List<string> items;
        private int TOP = 300;
        private int STEP = 70;
        
        public override void Init()
        {
            text = new Text("", CommonData.font, 28);
            items = new List<string>();
            rebuildItems();
            if (CommonData.music_main.Status != SoundStatus.Playing) CommonData.music_main.Play();
        }

        private void rebuildItems()
        {
            items.Clear();
            items.Add(ObjModule.texts.getText("menustart"));
            items.Add(ObjModule.texts.getText("menuhelp"));
            items.Add(ObjModule.texts.getText("menusound") + " : " + 
                ObjModule.texts.getText(ObjModule.opt.isSoundOn()? "text_on":"text_off"));
            items.Add(ObjModule.texts.getText("menumusic") + " : " +
                ObjModule.texts.getText(ObjModule.opt.isMusicOn() ? "text_on" : "text_off"));
            items.Add(ObjModule.texts.getText("menufullscreen") + " : " +
                ObjModule.texts.getText(ObjModule.opt.isFullScreen() ? "text_on" : "text_off"));
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
                        if (isMousePosOverButton(1))
                        {
                            setNextScene(new SceneHelp());
                            return SceneResult.Switch;
                        }
                        if (isMousePosOverButton(2))
                        {
                            ObjModule.opt.invertSoundOn();
                            rebuildItems();
                        }
                        if (isMousePosOverButton(3))
                        {
                            ObjModule.opt.invertMusicOn();
                            CommonData.music_main.Volume = ObjModule.opt.isMusicOn() ? 100.0f : 0.0f;
                            rebuildItems();
                        }
                        if (isMousePosOverButton(4))
                        {
                            ObjModule.opt.invertFullScreen();
                            return SceneResult.RebuildWindow;
                        }
                        if (isMousePosOverButton(5)) return SceneResult.Exit;
                    }
                                
                }
            }
            return SceneResult.Normal;

        }

        public override void Render(RenderWindow window)
        {
            DrawAt(window, CommonData.back, 0,0);

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

            DrawAt(window, CommonData.logo, ObjModule.opt.getWindowWidth() / 2, 100);

            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
