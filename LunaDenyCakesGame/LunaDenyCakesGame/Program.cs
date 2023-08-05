using SfmlNetEngine;
using System.IO;
using System.Diagnostics;
using System;
using SFML.Window;

namespace LunaDenyCakesGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // Если есть аргумент, то ставим каталог данных.
            // Нужно для вынесения каталога данных за пределы каталога сборки при отладке.
            if (args.Length == 0)
                Directory.SetCurrentDirectory(new FileInfo(Process.GetCurrentProcess().MainModule.FileName).DirectoryName);
            else
                Directory.SetCurrentDirectory(args[0]);

            String localdir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                Path.DirectorySeparatorChar + "LunaDenyCakesGame";
            if (!Directory.Exists(localdir)) Directory.CreateDirectory(localdir);

            CommonData.Load();            
            ObjModule.opt.setWindowParams(1024, 768);
            ObjModule.opt.keyconfig.addKey(CommonData.action_switch, Mouse.Button.Right);
            ObjModule.opt.keyconfig.addKey(CommonData.action_apply, Mouse.Button.Left);
            ObjModule.opt.keyconfig.addKey(CommonData.action_sel_teleport, Keyboard.Key.Num1);
            ObjModule.opt.keyconfig.addKey(CommonData.action_sel_laser, Keyboard.Key.Num2);
            ObjModule.opt.keyconfig.addKey(CommonData.action_sel_chicken, Keyboard.Key.Num3);
            ObjModule.opt.keyconfig.addKey(CommonData.action_sel_shield, Keyboard.Key.Num4);
            ObjModule.opt.keyconfig.addKey(CommonData.action_left, Keyboard.Key.A);
            ObjModule.opt.keyconfig.addKey(CommonData.action_right, Keyboard.Key.D);
            ObjModule.opt.LoadParams(localdir+ Path.DirectorySeparatorChar + "options.json");
            ObjModule.texts.loadFromFile("strings.ru.json");
            ObjModule.achievementstore.addAchievement(new AchievementWinEasy());
            ObjModule.achievementstore.addAchievement(new AchievementWinMedi());
            ObjModule.achievementstore.addAchievement(new AchievementWinHard());
            ObjModule.achievementstore.addAchievement(new AchievementWinMediChicken());
            ObjModule.achievementstore.addAchievement(new AchievementWinMediShield());
            ObjModule.achievementstore.addAchievement(new AchievementWinMedi50());
            ObjModule.achievementstore.addAchievement(new AchievementWinEasy75());
            ObjModule.achievementstore.LoadAchievements(localdir + Path.DirectorySeparatorChar + "achievements.json");

            var window = new SfmlNetEngine.Window();
            window.Show(typeof(SceneMenu), typeof(SceneMenu));
            CommonData.UnLoad();
        }
    }
}
