////////////////////////////////////////////////////////////////////////////////
//
//	MG_InnocentManager.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MG_Liquidator
{
    class MG_InnocentManager : Script
    {

        #region Properties      
        public static List<Ped> VictimsJackedCar { get; set; } = new List<Ped>();
        public static bool VictimsJackedCarUnmanaged { get; set; } = false;
        #endregion Properties

        #region Constructor
 
        public MG_InnocentManager()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (VictimsJackedCarUnmanaged)
            {
                Wait(2000);
                MakeJackedPedsFlee();
            }
        }
        #endregion OnTick

        #region Public Methods

        public static void All_GetOut(Vehicle vehicle)
        {
            Ped[] occupants = vehicle.Occupants;
            if (occupants.Length > 0)
            {
                foreach (var crew in occupants)
                {
                    //_crew.IsPersistent = true;
                    crew.Task.ClearAllImmediately();//NEW CAREFUL
                    crew.Task.LeaveVehicle(vehicle, false);
                    VictimsJackedCar.Add(crew);
                }
                VictimsJackedCarUnmanaged = true;
            }
        }

        public static void Reset()
        {
            VictimsJackedCarUnmanaged = false;
            VictimsJackedCar = new List<Ped>();

            //if (VictimsJackedCar.Count > 0)
            //{
            //    foreach (var _ped in VictimsJackedCar)
            //    {
            //        _ped.IsPersistent = false;
            //        _ped.MarkAsNoLongerNeeded();
            //    }
            //    VictimsJackedCar.Clear();
            //}
        }
        #endregion Public Methods

        #region Private Methods

        public static void MakeJackedPedsFlee()
        {
            List<Ped> tempList = new List<Ped>();

            foreach (var ped in VictimsJackedCar)
            {
                if (ped.IsInVehicle()) return;
                if (ped.IsRagdoll) return;
                if (ped.IsGettingUp) return;
                if (ped.IsBeingJacked) return;
                if (ped.IsBeingStunned) return;

                ped.IsPersistent = false;
                ped.Task.FleeFrom(MG_Target.Ped);
                ped.AlwaysKeepTask = true;
                tempList.Add(ped);
                Function.Call(GTA.Native.Hash.RESET_PED_LAST_VEHICLE, ped);
            }

            VictimsJackedCar = VictimsJackedCar.Except(tempList).ToList();
            if (VictimsJackedCar.Count == 0) VictimsJackedCarUnmanaged = false;
        }
        #endregion Private Methods
    }
}
