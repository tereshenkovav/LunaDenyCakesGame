using System;
using SfmlNetEngine;

namespace LunaDenyCakesGame
{
    public class AchievementWinEasy: Achievement
    {        
        public override String getCode()
        {
            return "win_easy";
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Easy)) iscompleted = true;
        }
    }
    public class AchievementWinMedi : Achievement
    {
        public override String getCode()
        {
            return "win_medi";
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Medi)) iscompleted = true;
        }
    }
    public class AchievementWinHard : Achievement
    {
        public override String getCode()
        {
            return "win_hard";
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Hard)) iscompleted = true;
        }
    }
    public class AchievementWinMediChicken : Achievement
    {
        private bool ischickenused;

        public override String getCode()
        {
            return "win_medi_chicken";
        }
        public override void Reset()
        {
            base.Reset();
            ischickenused = false;
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            if (game.getChickenCount() > 0) ischickenused = true;
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Medi)) 
                if (!ischickenused) iscompleted = true;
        }
    }

    public class AchievementWinMediShield : Achievement
    {
        private bool isshieldused;

        public override String getCode()
        {
            return "win_medi_shield";
        }
        public override void Reset()
        {
            base.Reset();
            isshieldused = false;
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            for (int i=0; i<game.getCakeCount(); i++) 
                if (game.isCakeShieldOn(i)) isshieldused = true;

            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Medi))
                if (!isshieldused) iscompleted = true;
        }
    }

    public class AchievementWinMedi50 : Achievement
    {        
        public override String getCode()
        {
            return "win_medi_50";
        }        
        public override void Update(Object obj)
        {
            Game game = (Game)obj;            
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Medi))
                if (game.getCelestiaHPin100()>=50) iscompleted = true;
        }
    }

    public class AchievementWinEasy75 : Achievement
    {
        public override String getCode()
        {
            return "win_easy_75";
        }
        public override void Update(Object obj)
        {
            Game game = (Game)obj;
            if (game.isWin() && (ObjModule.opt.getDifficult() == Difficult.Easy))
                if (game.getCelestiaHPin100() >= 75) iscompleted = true;
        }
    }

}
