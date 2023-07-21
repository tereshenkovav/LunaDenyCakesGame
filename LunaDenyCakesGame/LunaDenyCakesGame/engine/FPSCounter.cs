using System;

namespace SfmlNetEngine
{
    public class FPSCounter
    {
        private int cnt;
        private int fps;
        private float fullsec;

        public FPSCounter()
        {
            fullsec = 1.0f;
            cnt = 0;
            fps = 0;
        }
        public void Update(float dt)
        {
            if (fps == 0) fps = 60;

            cnt++;
            fullsec -= dt;
            if (fullsec <= 0)
            {
                fullsec = 1.0f;
                fps = cnt;
                cnt = 0;
            }
        }
        public int getFPS()
        {
            return fps;
        }
    }
}
