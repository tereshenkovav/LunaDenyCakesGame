namespace SfmlNetEngine
{
    public class Options
    {        
        private int window_w = 800;
        private int window_h = 600;
        private bool soundon = true;
        private bool musicon = true;
        private bool fullscreen = false;
        
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
        public void LoadParams()
        {            
        }
        public void SaveParam()
        {            
        }        
    }
}
