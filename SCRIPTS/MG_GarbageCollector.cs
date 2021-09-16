////////////////////////////////////////////////////////////////////////////////
//
//	MG_GarbageCollector.cs
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
    public static class MG_GarbageCollector
    {
        #region Fields
        private static HashSet<Vehicle> _vehicles = new HashSet<Vehicle>();
        #endregion Fields

        #region Properties      
        public static bool NeedToCleanAfterPlayerDeath { get; set; } = false;
        public static List<Blip> vehicleBlips { get; set; } = new List<Blip>();


        public static int Distance_DestroyedVehicle { get; set; } = 100;
        public static int Distance_EmptyVehicle { get; set; } = 200;
        public static int Distance_NotEmptyVehicle { get; set; } = 650;
        #endregion Properties

        #region Public Methods

        public static void AddVehicle(Vehicle vehicle)
        {
            _vehicles.Add(vehicle);
        }

        public static void StartCleaning()
        {
            foreach (var vehicle in _vehicles.ToArray())
            {
                if (vehicle != null)
                {
                    if (vehicle.Occupants.Length > 0)
                    {
                        foreach (var ped in vehicle.Occupants)
                        {
                            //ped.IsPersistent = false;
                            //ped.MarkAsNoLongerNeeded();
                            ped.Delete();
                        }
                    }
                    //vehicle.MarkAsNoLongerNeeded();

                    //NEW
                    if (vehicle.CurrentBlip != null)
                    {
                        vehicle.CurrentBlip.Remove();
                    }
                    //NEW

                    vehicle.Delete();
                }
            }
            //MG_Message.SubTitle("MG_GarbageCollector CLEARING EVERYTHING!(): COUNT CLEARED=" + _vehicles.Count, 9000);
            _vehicles.Clear();
        }

        public static void CheckVeryFarVehiclesAndRemove()
        {
            foreach (var vehicle in _vehicles.ToArray())
            {
                ///---NEW запоминаем блипы (ля удаление багнутых блипов от машин)
                //Blip blip = vehicle.CurrentBlip;
                //if (blip != null)
                //{
                //    if (vehicleBlips.Contains(blip))
                //    {
                //        vehicleBlips.Add(blip);
                //    }
                //}
                CheckAndRemoveBuggedBlips();
                ///---NEW запоминаем блипы (ля удаление багнутых блипов от машин)

                if (vehicle != null)
                {
                    bool remove = true;
                    float distance;
                    if (vehicle.IsDead)
                    {
                        if (vehicle.CurrentBlip != null)
                        {
                            vehicle.CurrentBlip.Remove();
                        }
                        distance = Distance_DestroyedVehicle;
                    }
                    else
                    {
                        if (vehicle.Occupants.Length == 0)
                        {
                            distance = Distance_EmptyVehicle;
                        }
                        else
                        {
                            distance = Distance_NotEmptyVehicle;
                            //Remove if only forgotten enemies inside
                            foreach (var ped in vehicle.Occupants)
                            {                             
                                if (MG_Target.Ped != null)
                                {
                                    if ((MG_ForgottenEnemy.Contains(ped) == false))
                                    {
                                        if (ped.IsAlive)//NEW
                                        {
                                            remove = false;
                                            break;
                                        }                                       
                                    }
                                }
                            }
                        }
                    }
                    if (remove)
                    {
                        if (Vector2.Distance(vehicle.Position, MG_Player.Ped.Position) > distance)
                        {
                            //MG_Message.SubTitle("MG_GarbageCollector: CAR REMOVED! Distance=" + World.GetDistance(vehicle.Position, MG_Player.Ped.Position) + " Vehicle=" + vehicle.FriendlyName + " | COUNT=" + (_vehicles.Count - 1), 9000);
                            //vehicle.MarkAsNoLongerNeeded();
                            if (vehicle.CurrentBlip != null)
                            {
                                vehicle.CurrentBlip.Remove();
                            }
                            _vehicles.Remove(vehicle);
                            vehicle.Delete();///NEW
                        }
                    }
                }
            }
        }

        public static void CheckAndRemoveBuggedBlips()
        {
            if (vehicleBlips.Any())
            {
                if (_vehicles.Any())
                {
                    int countBlips = vehicleBlips.Count;
                    int countVehicles = _vehicles.Count;
                    if (countBlips > countVehicles)//НЕ СОВПАДАЕТ!
                    {
                        foreach (var blip in vehicleBlips.ToArray())
                        {
                            if (blip == null) continue;
                            bool remove = true;
                            foreach (var vehicle in _vehicles.ToArray())
                            {
                                Blip blipV = vehicle.CurrentBlip;
                                if (blip.Equals(blipV))
                                {
                                    remove = false;
                                    break;
                                }                               
                            }
                            if (remove)
                            {
                                vehicleBlips.Remove(blip);
                                blip.Remove();
                                //MG_Message.SubTitle("CheckAndRemoveBuggedBlips(): SUCCESS! ONE FOUND!", 10000);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var blip in vehicleBlips)
                    {
                        if (blip != null) blip.Remove();
                    }
                    vehicleBlips.Clear();
                    //MG_Message.SubTitle("CheckAndRemoveBuggedBlips(): SUCCESS! FULL REMOVE!", 10000);
                }

            }
        }
        #endregion Public Methods

    }
}
