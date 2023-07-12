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
        public virtual SceneResult Frame(float dt, IEnumerable<EventArgs> events)
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

        public void DrawTextCentered(RenderWindow window, Text text, String data, int x, int y)
        {
            text.DisplayedString = data;
            float textWidth = text.GetLocalBounds().Width;
            text.Position = new Vector2f(x-textWidth/2,y);
            window.Draw(text);
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
