using System;

namespace SfmlNetEngine
{
    public enum Difficult { Easy, Medi, Hard };

    public class Options
    {
        private int window_w = 800;
        private int window_h = 600;
        private bool soundon = true;
        private bool musicon = true;
        private bool fullscreen = false;
        private Difficult difficult = Difficult.Medi;
        
        public void setWindowParams(int w, int h)
        {
            window_w = w;
            window_h = h;
        }
        public int getWindowWidth()
        {
            return window_w ;
        }
        public int getWindowHeigth()
        {
            return window_h;
        }
        public void invertSoundOn()
        {
            soundon = !soundon;
            SaveParam();
        }
        public bool isSoundOn()
        {
            return soundon;
        }
        public void invertMusicOn()
        {
            musicon = !musicon;            
        }
        public bool isMusicOn()
        {
            return musicon;
        }
        public void invertFullScreen()
        {
            fullscreen = !fullscreen;            
        }
        public bool isFullScreen()
        {
            return fullscreen;
        }
        public Difficult getDifficult()
        {
            return difficult;
        }
        public String getDifficultCode()
        {
            switch (difficult) {
                case Difficult.Easy: return "easy";
                case Difficult.Medi: return "medi";
                case Difficult.Hard: return "hard";
                default: return "medi";
            }
        }
        public void switchDifficult()
        {
            if (difficult == Difficult.Easy) difficult = Difficult.Medi; else
            if (difficult == Difficult.Medi) difficult = Difficult.Hard; else
            if (difficult == Difficult.Hard) difficult = Difficult.Easy;
        }
        public void LoadParams()
        {            
        }
        public void SaveParam()
        {            
        }        
    }
}
