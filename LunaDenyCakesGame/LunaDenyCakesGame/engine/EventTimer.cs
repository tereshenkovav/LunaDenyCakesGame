using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace SfmlNetEngine
{
    public class EventTimer
    {
        private float left;
        private Action proc;

        public EventTimer()
        {
            left = -1.0f;
        }
        public EventTimer(float value, Action timerproc)
        {
            Start(value,timerproc);
        }
        public bool isActive()
        {
            return left > 0.0f;
        }
        public void Start(float value, Action timerproc)
        {
            left = value;
            proc = timerproc;
        }
        public void Update(float dt)
        {
            if (left>0.0f)
            {
                left -= dt;
                if (left <= 0.0f) proc();
            }
        }
    }
}
