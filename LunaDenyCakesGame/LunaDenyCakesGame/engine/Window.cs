using System;
using System.Collections.Generic;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SfmlNetEngine
{
    // Класс окна игры
    public class Window
    {
        private List<EventArgs> events = new List<EventArgs>();
        
        public void Show(Type initscene, Type optscene)
        {            
            VideoMode mode = new VideoMode((uint)ObjModule.opt.getWindowWidth(), (uint)ObjModule.opt.getWindowHeigth(), 32);
            RenderWindow window = null;

            Scene tekscene = null;
            
        // Точка входа в окно первая, или после смены разрешения
        lab_reset_fullscreen:
            // Создание окна
            if (ObjModule.opt.isFullScreen())
                window = new RenderWindow(mode, ObjModule.texts.getText("gametitle"), Styles.Fullscreen);
            else
                window = new RenderWindow(mode, ObjModule.texts.getText("gametitle"));
            window.SetVerticalSyncEnabled(true);
            window.SetMouseCursorVisible(false);
                            
            // Привязка событий
            window.Closed += (obj, e) => { window.Close(); };
            window.KeyPressed += (sender, e) => { events.Add(e); };
            window.MouseButtonPressed += (sender, e) => { events.Add(e); };
                                    
            Clock clock = new Clock();

            // Начальный цикл игры - главное меню
            
            if (tekscene==null) tekscene = (Scene)Activator.CreateInstance(initscene);
            tekscene.Init();

            while (window.IsOpen)
            {                
                // Очистка событий, сбор событий, установка курсора
                events.Clear();
                window.DispatchEvents();
                tekscene.setMousePosition(Mouse.GetPosition(window));

                // Обновление состояния игры
                SceneResult r = tekscene.Frame(clock.Restart().AsSeconds(), events) ;
                // Если выход, то стоп окну
                switch (r)
                {
                    case SceneResult.Exit:
                        window.Close();
                        break;
                    // Если переключение, то переводим на другой цикл, который вернули
                    case SceneResult.Switch:
                        tekscene.UnInit();
                        tekscene = tekscene.getNextScene();
                        tekscene.Init();
                        break;
                    case SceneResult.RebuildWindow:
                        tekscene.UnInit();
                        window.Close();
                        tekscene = (Scene)Activator.CreateInstance(initscene);
                        goto lab_reset_fullscreen;                        
                    default:
                        // Иначе просто выводим игру на экран
                        window.Clear();
                        tekscene.Render(window);
                        window.Display();
                        break;
                }                
            }
        }
    }
}
