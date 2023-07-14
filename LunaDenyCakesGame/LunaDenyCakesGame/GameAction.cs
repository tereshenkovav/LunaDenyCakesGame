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
            return (game.getZoneByXY(mxy) != -1);
        }
        public override bool Apply(Vector2i mxy)
        {
            return game.jumpLunaToXY(mxy);
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
            return (game.getZoneByXY(mxy) != -1);
        }
        public override bool Apply(Vector2i mxy)
        {
            return game.addChicken(mxy);
        }
    }
}
