using System;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public enum Difficult { Easy, Medi, Hard };

    public class CustomOptionsParams: OptionsParams
    {
        public Difficult difficult { get; set; }
    }

    public class CustomOptions: Options
    {
        // Статическая ссылка на приведенный объект расширенных опций.
        // Может использоваться наравне с ObjModule.opt
        public static CustomOptions customopt;

        private Difficult difficult = Difficult.Medi;
        public Difficult getDifficult()
        {
            return difficult;
        }
        public String getDifficultCode()
        {
            switch (difficult)
            {
                case Difficult.Easy: return "easy";
                case Difficult.Medi: return "medi";
                case Difficult.Hard: return "hard";
                default: return "medi";
            }
        }
        public void switchDifficult()
        {
            if (difficult == Difficult.Easy) difficult = Difficult.Medi;
            else
            if (difficult == Difficult.Medi) difficult = Difficult.Hard;
            else
            if (difficult == Difficult.Hard) difficult = Difficult.Easy;
            SaveParam();
        }
        // Здесь добавляем загрузку дополнительных полей
        protected override void loadCustom(Object obj)
        {
            difficult = ((CustomOptionsParams)obj).difficult;
        }
        protected override void saveCustom(Object obj)
        {
            ((CustomOptionsParams)obj).difficult = difficult;
        }
    }
}
