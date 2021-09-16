////////////////////////////////////////////////////////////////////////////////
//
//	MG_Reward.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    public static class MG_Reward
    {
        //Game.Player.Money = Game.Player.Money + Reward;

        #region Fields
        #endregion Fields

        #region Properties
        #endregion Properties

        #region Constructor
        #endregion Constructor

        #region Public Methods
        public static void Activate()
        {
            int reward = 5000;

            reward += MG_Statistic.TotalTargetsEliminated * 100;
            reward += MG_TargetBodyGuards.AmountCanBeSpawn * 100;
            //reward += MG_Watchers.AmountSquadsCanBeSpawn * 10000;
            Game.Player.Money += reward;
        }

        public static void ActivatePenality()
        {
            int penality = 5000;

            penality += MG_Statistic.TotalTargetsEliminated * 100;
            penality += MG_TargetBodyGuards.AmountCanBeSpawn * 100;
            penality *= 3;
            //reward += MG_Watchers.AmountSquadsCanBeSpawn * 10000;
            Game.Player.Money -= penality;
        }
        #endregion Public Methods

        #region Private Methods
        #endregion Private Methods
    }
}
