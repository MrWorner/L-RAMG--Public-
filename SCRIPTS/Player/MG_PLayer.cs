////////////////////////////////////////////////////////////////////////////////
//
//	MG_Player.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System;

namespace MG_Liquidator
{
    public static class MG_Player
    {
        public static bool DisableHealthRegeneration { get; set; } = false;

        //public static Ped Ped { get => Game.Player.Character;  }
        public static Ped Ped { get; set; } = Game.Player.Character;
        public static Player Player { get; set; } = Game.Player;
        public static bool IsUsingCellphone { get; set; } = false;
        public static int Group { get; set; } = World.AddRelationshipGroup("PLAYER_TEAM");
        public static int RelationsGroup { get; set; } = Function.Call<int>(Hash.GET_HASH_KEY, "PLAYER_TEAM");
        #region Public Methods

        public static void DisableHealthRecharge()
        {
            Function.Call(Hash.SET_PLAYER_HEALTH_RECHARGE_MULTIPLIER, Player, 0.0);
        }

        public static void DisableHUD(bool disable)
        {
            if (disable)
            {
                Function.Call(Hash.DISPLAY_HUD, 0);
            }
            else
            {
                Function.Call(Hash.DISPLAY_HUD, 1);
            }
           
        }

        public static void DisableRadar(bool disable)
        {
            if (disable)
            {
                Function.Call(Hash.DISPLAY_RADAR, 0);
            }
            else
            {
                Function.Call(Hash.DISPLAY_RADAR, 1);
            }
           
        }

        public static void UpWantedLevel()
        {
            int wantedLevel = Player.WantedLevel;
            if (wantedLevel + 1 < 5)
            {
                Player.WantedLevel = wantedLevel + 1;
            }
            if (Player.WantedLevel == 1)
            {
                Player.WantedLevel = 2; 
            }
        }

        public static bool IsAlive()
        {
            return Ped.IsAlive;
        }

        public static bool IsDead()
        {
            return Ped.IsDead;
        }

        #endregion Public Methods
    }
}
