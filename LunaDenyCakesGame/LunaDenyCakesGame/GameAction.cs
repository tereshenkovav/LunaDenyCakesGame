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
    public abstract class GameAction
    {
        protected Game game;
        public GameAction(Game game)
        {
            this.game = game;
        }
        public abstract string getCode();
        public abstract bool isAllowed(Vector2i mxy);
        public abstract bool Apply(Vector2i mxy);
        public abstract int getCost();
        public virtual void Finish()
        {
            // No action default
        }
    }

    public class GAJump : GameAction
    {
        public GAJump(Game game): base(game)
        {            
        }
        public override string getCode()
        {
            return "jump";
        }
        public override bool isAllowed(Vector2i mxy)
        {            
            return (game.getZoneByXY(mxy) != -1)&&(game.getMana()>=getCost());
        }
        public override bool Apply(Vector2i mxy)
        {
            if (!isAllowed(mxy)) return false;
            game.decMana(getCost());
            return game.jumpLunaToXY(mxy);
        }
        public override int getCost()
        {
            return game.balance.JumpCost;
        }
    }

    public class GAChicken : GameAction
    {
        public GAChicken(Game game) : base(game)
        {
        }
        public override string getCode()
        {
            return "chicken";
        }
        public override bool isAllowed(Vector2i mxy)
        {
            return (game.getZoneByXY(mxy) != -1) && (game.getMana() >= getCost());
        }
        public override bool Apply(Vector2i mxy)
        {
            if (!isAllowed(mxy)) return false;
            game.decMana(getCost());
            return game.addChicken(mxy);
        }
        public override int getCost()
        {
            return game.balance.ChickenCost;
        }
    }

    public class GAShield : GameAction
    {
        public GAShield(Game game) : base(game)
        {
        }
        public override string getCode()
        {
            return "shield";
        }
        public override bool isAllowed(Vector2i mxy)
        {
            return (game.getCakeAt(mxy)!=-1) && (game.getMana() >= getCost());
        }
        public override bool Apply(Vector2i mxy)
        {
            if (!isAllowed(mxy)) return false;
            game.decMana(getCost());
            return game.setShieldToCakeByXY(mxy);
        }
        public override int getCost()
        {
            return game.balance.ShieldCost;
        }
    }

    public class GALaser : GameAction
    {
        public GALaser(Game game) : base(game)
        {
        }
        public override string getCode()
        {
            return "laser";
        }
        public override bool isAllowed(Vector2i mxy)
        {
            return (game.getZoneByXY(mxy) == game.getLunaZoneIdx()) && (game.getMana() >= getCost());
        }
        public override bool Apply(Vector2i mxy)
        {
            if (!isAllowed(mxy)) return false;
            game.startLaser(mxy);
            return true;
        }
        public override void Finish()
        {
            game.finishLaser();
        }
        public override int getCost()
        {
            return (int)game.balance.LaserCostInSec;
        }
    }
}
