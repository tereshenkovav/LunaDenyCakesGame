using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SfmlNetEngine
{
    // Результат выполнения игрового цикла - обычный, выход, переключение
    public enum SceneResult
    {
        Normal,
        Exit,
        Switch,
        RebuildWindow
    }
    // Абстрактный класс игрового цикла - содержит методы для определения в потомках
    public abstract class Scene
    {
        private Vector2i mousepos;
        private static Vector2f left_scale = new Vector2f(1, 1);
        private static Vector2f right_scale = new Vector2f(-1, 1);

        // Ссылка на объект игрового цикла, куда перейдет управление
        protected Scene nextscene = null;

        // Инициализация объекта цикла
        public virtual void Init()
        {

        }

        // Очистка ресурсов, если нужно
        public virtual void UnInit()
        {

        }

        // Процедура фрейма - обработка событий и сдвига времени, возвращает указание, что делать далее
        public virtual SceneResult Frame(float dt, IEnumerable<EventArgsEx> events)
        {
            return SceneResult.Normal;
        }

        // Процедура рендера - выводит в окно текущее состояние игры
        public virtual void Render(RenderWindow window)
        {

        }
                
        public void setMousePosition(Vector2i pos)
        {
            mousepos = pos;
        }

        public Vector2i getMousePosition()
        {
            return mousepos;
        }

        // Вывод спрайта в заданной позиции окна
        public void DrawAt(RenderWindow window, Sprite spr, float x, float y)
        {
            spr.Position = new Vector2f(x, y);
            window.Draw(spr);
        }

        public void DrawAt(RenderWindow window, Sprite spr, Vector2f pos)
        {
            spr.Position = pos;
            window.Draw(spr);
        }

        public void DrawMirrHorzAt(RenderWindow window, Sprite spr, float x, float y, bool ismirr)
        {
            spr.Position = new Vector2f(x, y);
            spr.Scale = ismirr ? right_scale : left_scale;
            window.Draw(spr);
        }

        public void DrawMirrHorzAt(RenderWindow window, Sprite spr, Vector2f pos, bool ismirr)
        {
            spr.Position = pos;
            spr.Scale = ismirr ? right_scale : left_scale;
            window.Draw(spr);
        }

        public void DrawTextCentered(RenderWindow window, Text text, String data, int x, int y)
        {
            text.DisplayedString = data;
            float textWidth = text.GetLocalBounds().Width;
            text.Position = new Vector2f(x-textWidth/2,y);
            window.Draw(text);
        }

        public float GetTextWidth(Text text, String data)
        {
            text.DisplayedString = data;
            return text.GetLocalBounds().Width;
        }

        public void DrawText(RenderWindow window, Text text, String data, int x, int y)
        {
            text.DisplayedString = data;
            text.Position = new Vector2f(x, y);
            window.Draw(text);
        }

        public void DrawIndicator(RenderWindow window, float x, float y, int w, int h, float v, Color[] colorset)
        {
            if (v < 0.0f) v = 0.0f;
            if (v > 1.0f) v = 1.0f;

            using (RectangleShape rect = new RectangleShape())
            {
                rect.Origin = new Vector2f(0, 0);
                rect.OutlineThickness = 0;

                int teksize = (int)(w * v);
                if (teksize < 1) teksize = 1;
                rect.Size = new Vector2f(teksize, h);
                rect.Position = new Vector2f(x, y);

                if (colorset.Length == 1)
                    rect.FillColor = colorset[0];
                else
                {
                    float dc = 1.0f / colorset.Length;
                    for (int i = colorset.Length - 1; i >= 0; i--)
                        if (v > dc * i)
                        {
                            rect.FillColor = colorset[i];
                            break;
                        }
                }
                window.Draw(rect);
            }
        }

        // Установка следующего цикла, если нужно
        protected void setNextScene(Scene scene)
        {
            nextscene = scene;
        }

        public Scene getNextScene()
        {
            return nextscene;
        }
    }
}
