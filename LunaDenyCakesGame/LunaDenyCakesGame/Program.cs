using System;
using SfmlNetEngine;
using System.IO;
using System.Diagnostics;

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

            CommonData.Load();
            ObjModule.opt.setWindowParams(1024, 768);
            ObjModule.opt.LoadParams();
            ObjModule.texts.loadFromFile("strings.ru.json");

            var window = new Window();
            window.Show(typeof(ScenePlay), typeof(SceneMenu));
            CommonData.UnLoad();
        }
    }
}
