using System;
using NetSfmlLib;

namespace LunaDenyCakesGame
{
    public enum Difficult { Easy, Medi, Hard };

    public class CustomOptionsParams: OptionsParams
    {
        public Difficult difficult { get; set; }
        public bool apply_after_select { get; set; }
    }

    public class CustomOptions: Options
    {
        // Статическая ссылка на приведенный объект расширенных опций.
        // Может использоваться наравне с ObjModule.opt
        public static CustomOptions customopt;

        private Difficult difficult = Difficult.Medi;
        private bool apply_after_select = false;
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
        public bool isApplyAfterSelect()
        {
            return apply_after_select;
        }
        public void setApplyAfterSelect(bool value)
        {
            apply_after_select = value;
        }
        public void switchApplyAfterSelect()
        {
            apply_after_select = !apply_after_select;
            SaveParam();
        }
        // Здесь добавляем загрузку дополнительных полей
        protected override void loadCustom(Object obj)
        {
            difficult = ((CustomOptionsParams)obj).difficult;
            apply_after_select = ((CustomOptionsParams)obj).apply_after_select;
        }
        protected override void saveCustom(Object obj)
        {
            ((CustomOptionsParams)obj).difficult = difficult;
            ((CustomOptionsParams)obj).apply_after_select = apply_after_select;
        }
    }
}
