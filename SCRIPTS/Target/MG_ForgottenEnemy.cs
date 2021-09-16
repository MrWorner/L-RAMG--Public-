////////////////////////////////////////////////////////////////////////////////
//
//	MG_ForgottenEnemy.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    class MG_ForgottenEnemy : Script
    {
        #region Fields
        private static bool _forgottensAdded = false;
        private static HashSet<Ped> _forgottens = new HashSet<Ped>();
        #endregion Fields

        #region Properties
        //public static List<Ped> Forgottens { get; set; } = new List<Ped>();
        public static int Distance_to_delete_alive_ped { get; set; } = 300;
        public static int Distance_to_delete_dead_ped { get; set; } = 50;

        #endregion Properties

        #region Constructor
 
        public MG_ForgottenEnemy()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (_forgottensAdded)
            {
                CheckForgottens();
            }
        }
        #endregion OnTick

        #region Public Methods

        public static void AddPed(Ped ped)
        {
            _forgottens.Add(ped);
            if (_forgottensAdded == false)
            {
                _forgottensAdded = true;
            }
        }

        public static bool Contains(Ped ped)
        {
            return _forgottens.Contains(ped);
        }

        public static void RemoveEveryone()
        {
            if (_forgottensAdded)
            {
                foreach (var ped in _forgottens.ToList())
                {
                    //if (ped.IsPersistent) ped.IsPersistent = false;
                    //ped.MarkAsNoLongerNeeded();
                    ped.Delete();///NEW
                }
                _forgottensAdded = false;
                _forgottens.Clear();
            }
        }
        #endregion Public Methods

        #region Private Methods

        private static void CheckForgottens()
        {
            if (_forgottens.Any())
            {
                foreach (var ped in _forgottens.ToList())
                {
                    if (ped != null)
                    {
                        float distance = Vector2.Distance(MG_Player.Ped.Position, ped.Position);
                        if (ped.IsAlive)
                        {
                            //bool isInVehicle = ped.IsInVehicle();
                            //if (isInVehicle)
                            //{
                            //    if (distance > 750f)
                            //    {
                            //        _forgottens.Remove(ped);
                            //        //if (ped.IsPersistent) ped.IsPersistent = false;                               
                            //        //ped.MarkAsNoLongerNeeded();
                            //        if (ped.CurrentBlip != null) ped.CurrentBlip.Remove();
                            //        ped.Delete();
                            //    }
                            //}
                            //else
                            //{
                                if (distance > Distance_to_delete_alive_ped)
                                {
                                    _forgottens.Remove(ped);
                                    //if (ped.IsPersistent) ped.IsPersistent = false;                                    
                                    //ped.MarkAsNoLongerNeeded();
                                    if (ped.CurrentBlip != null) ped.CurrentBlip.Remove();
                                    ped.Delete();
                                }
                            //}
                        }
                        else
                        {
                            //if (ped.IsPersistent) ped.IsPersistent = false;if (ped.CurrentBlip != null) ped.CurrentBlip.Remove();
                            if (ped.CurrentBlip != null) ped.CurrentBlip.Remove();
                            if (distance > Distance_to_delete_dead_ped)
                            {
                                _forgottens.Remove(ped);
                               
                                ped.Delete();
                            }
                        }
                    }
                }
            }
            else
            {
                _forgottensAdded = false;
            }
            Wait(1000);
        }
        #endregion Private Methods
    }
}
