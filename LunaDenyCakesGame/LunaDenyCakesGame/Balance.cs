using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class Balance
    {
        public int LunaVel = 100;
        public int CelestiaVel = 70;
        public int ShieldTime = 10;
        public int ChickenVel = 50;
        public float LaserPowerInSec = 1.0f;
        public float LaserCostInSec = 30.0f;
        public int ShieldCost = 10;
        public int JumpCost = 40;
        public int ChickenCost = 20;
        public int MaxMana = 200;
        public float RegenManaInSec = 5.0f;
        public float EatInSec = 0.20f;
        public float CelestiaStartHP = 5.0f;
    }
}
