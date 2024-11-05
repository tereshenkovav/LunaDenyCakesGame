using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using NetSfmlLib;
using System.IO;
using System.Text.Json;

namespace LunaDenyCakesGame
{
    public class SceneAbout : Scene
    {
        // Ресурсы и константы
        private Text text;
        private Text text_ver;
        private Text text_info;
        private Text text_credits;
        private Text text_credits_list;
        protected List<string> items;
        private int TOP = 700;
        private int STEP = 70;

        public override void Init()
        {
            text = new Text("", CommonData.font, 22);

            text_info = new Text("", CommonData.font, 24);
            text_info.FillColor = new Color(255, 255, 255);
            text_info.DisplayedString = ObjModule.texts.getText("about_info");
            text_info.Position = new Vector2f(30, 110);

            text_ver = new Text("", CommonData.font, 22);
            text_ver.FillColor = new Color(200, 200, 200);
            if (File.Exists("version.json"))
            {
                var ver = JsonSerializer.Deserialize<Dictionary<string,string>>(File.ReadAllText("version.json"));
                text_ver.DisplayedString = String.Format(ObjModule.texts.getText("about_version"), 
                    ver["tag"], ver["commit"], ver["branch"]);
            }
            else
                text_ver.DisplayedString = "";
            
            text_credits = new Text("", CommonData.font, 26);
            text_credits.FillColor = new Color(255, 255, 255);
            text_credits_list = new Text("", CommonData.font, 22);
            text_credits_list.FillColor = new Color(255, 255, 255);
            if (File.Exists("credits.json"))
            { 
                text_credits.DisplayedString = ObjModule.texts.getText("about_credits");
                var credits = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("credits.json"));
                var str = "";
                foreach (var s in credits)
                    str += s + Environment.NewLine;
                text_credits_list.DisplayedString = str;
                text_credits_list.Position = new Vector2f(ObjModule.opt.getWindowWidth() / 2 - 80, 350);
            }
            else
            {
                text_credits.DisplayedString = "";
                text_credits_list.DisplayedString = "";                
            } 

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

            return ((ObjModule.opt.getWindowWidth() / 2 - CommonData.button_small.Texture.Size.X / 2 < mx) &&
                    (TOP + STEP * i - CommonData.button_small.Texture.Size.Y / 2 < my) &&
                    (ObjModule.opt.getWindowWidth() / 2 + CommonData.button_small.Texture.Size.X / 2 > mx) &&
                    (TOP + STEP * i + CommonData.button_small.Texture.Size.Y / 2 > my));
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

            window.Draw(text_info);
            DrawTextCentered(window, text_ver, ObjModule.opt.getWindowWidth() / 2, 240);
            DrawTextCentered(window, text_credits, ObjModule.opt.getWindowWidth() / 2, 300);
            window.Draw(text_credits_list);
            
            // Рендер пунктов меню
            for (int i = 0; i < items.Count; i++)
            {
                // Выделение яркостью
                if (isMousePosOverButton(i))
                    CommonData.button_small.Color = CommonData.color_over;
                else
                    CommonData.button_small.Color = CommonData.color_norm;                
                DrawAt(window, CommonData.button_small, ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i);
                DrawTextCentered(window, text, items[i], ObjModule.opt.getWindowWidth() / 2, TOP + STEP * i - 20);
            }
                        
            // Курсор
            DrawAt(window, CommonData.cursor, (Vector2f)getMousePosition());
        }
    }
}
