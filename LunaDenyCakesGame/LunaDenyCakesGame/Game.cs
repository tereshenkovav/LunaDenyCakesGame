using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class Zone
    {
        public int y;
        public int left;
        public int right;
    }

    public enum Walking {  No, Left, Right };

    public class Game
    {
        private List<Zone> zones;
        private static int ZONEW = 84;
        private float celestiax;
        private int celestiazoneidx;
        private float lunax;
        private int lunazoneidx;
        private static int LUNAVX = 100;
        private static int PONYW = 30;

        public Game()
        {
            zones = new List<Zone>();
            zones.Add(new Zone() { y = 90, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 200, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 310, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 420, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 530, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 640, left = 50, right = 50 + ZONEW * 11 });
            zones.Add(new Zone() { y = 750, left = 50, right = 50 + ZONEW * 11 });

            celestiax = (zones[0].left+ zones[0].right)/ 2;
            celestiazoneidx = 0;
            lunax = (zones[6].left + zones[6].right) / 2;
            lunazoneidx = 6;            
        }
        public Vector2f getCelestiaPos()
        {
            return new Vector2f(celestiax,zones[celestiazoneidx].y);
        }
        public bool isCelestiaEat()
        {
            return false;
        }
        public Vector2f getLunaPos()
        {
            return new Vector2f(lunax, zones[lunazoneidx].y);
        }
        public int getZoneCount()
        {
            return zones.Count;
        }
        public Zone getZone(int i)
        {
            return zones[i];
        }
        public bool sendLunaLeft(float dt)
        {
            float newlunax = lunax - LUNAVX * dt;
            if (newlunax >= zones[lunazoneidx].left + PONYW / 2)
            {
                lunax = newlunax;
                return true;
            }
            else return false;
        }
        public bool sendLunaRight(float dt)
        {
            float newlunax = lunax + LUNAVX * dt;
            if (newlunax <= zones[lunazoneidx].right - PONYW / 2)
            {
                lunax = newlunax;
                return true;
            }
            else
                return false;
        }
        public void Update(float dt)
        {

        }
    }
}
