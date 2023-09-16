using System;
using System.Collections.Generic;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SfmlNetEngine
{
    public class EventArgsEx
    {
        public EventArgs e;
        public bool released;
        public EventArgsEx(EventArgs e, bool released)
        {
            this.e = e;
            this.released = released;
        }
    }
    // Класс окна игры
    public class Window
    {
        private List<EventArgsEx> events = new List<EventArgsEx>();
        private Image icon = null;
        private Type confirmexitscene = null;
        private bool closehandled = false;
        private Scene prevscene = null;

        public void SetIcon(Image icon)
        {
            this.icon = icon;
        }
        public void SetSceneConfirmExit(Type confirmexitscene)
        {
            this.confirmexitscene = confirmexitscene;
        }

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
                window = new RenderWindow(mode, ObjModule.texts.getText("gametitle"), Styles.Close);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(60);
            window.SetMouseCursorVisible(false);

            if (icon != null) window.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);

            // Привязка событий
            window.Closed += (obj, e) => { closehandled = true; };
            window.KeyPressed += (sender, e) => { events.Add(new EventArgsEx(e,false)); };
            window.KeyReleased += (sender, e) => { events.Add(new EventArgsEx(e, true)); };
            window.MouseButtonPressed += (sender, e) => { events.Add(new EventArgsEx(e, false)); };
            window.MouseButtonReleased += (sender, e) => { events.Add(new EventArgsEx(e, true)); };

            Clock clock = new Clock();
            FPSCounter fps = new FPSCounter();
            bool debuginfo = false;

            // Начальный цикл игры - главное меню            
            if (tekscene==null) tekscene = (Scene)Activator.CreateInstance(initscene);
            tekscene.Init();
                        
            while (window.IsOpen)
            {
                // Очистка событий, сбор событий, установка курсора
                closehandled = false;
                events.Clear();
                window.DispatchEvents();
                tekscene.setMousePosition(Mouse.GetPosition(window));

                float dt = clock.Restart().AsSeconds();
                fps.Update(dt);

                foreach (EventArgsEx args in events)
                    if ((args.e is KeyEventArgs keyEventArg) && (args.released))
                        if ((keyEventArg.Code == Keyboard.Key.F10) && (keyEventArg.Shift)) {
                            debuginfo = !debuginfo;
                            if (!debuginfo)
                            {
                                window.SetTitle(ObjModule.texts.getText("gametitle"));
                                window.SetMouseCursorVisible(false);
                            }
                            else
                            {
                                window.SetMouseCursorVisible(true);
                            }
                        }

                if (debuginfo) window.SetTitle(String.Format("FPS: {0}",fps.getFPS()));

                // Завершаем обновление игры при неактивном окне
                if (!window.HasFocus()) continue;
                                
                // Обновление состояния игры
                SceneResult r = tekscene.Frame(dt, events) ;
                // Если выход, то стоп окну
                switch (r)
                {
                    case SceneResult.Exit:
                        window.Close();
                        break;
                    // Если переключение, то переводим на другой цикл, который вернули
                    case SceneResult.Switch:
                        tekscene.UnInit();
                        if (prevscene != null)
                        {
                            tekscene = prevscene;
                            prevscene = null;
                        }
                        else
                        {
                            tekscene = tekscene.getNextScene();
                            tekscene.Init();
                        }
                        break;
                    case SceneResult.RebuildWindow:
                        tekscene.UnInit();
                        window.Close();
                        tekscene = (Scene)Activator.CreateInstance(optscene);
                        goto lab_reset_fullscreen;                        
                    default:
                        // Иначе просто выводим игру на экран
                        window.Clear();
                        tekscene.Render(window);
                        window.Display();
                        break;
                }

                if (closehandled)
                {
                    if (confirmexitscene == null)
                    {
                        window.Close();
                        break;
                    }
                    else
                    {
                        if (tekscene.GetType() != confirmexitscene)
                        {
                            prevscene = tekscene;
                            tekscene = (Scene)Activator.CreateInstance(confirmexitscene);
                            tekscene.Init();
                        }
                    }
                }
            }
        }
    }
}
