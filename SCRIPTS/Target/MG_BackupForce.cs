////////////////////////////////////////////////////////////////////////////////
//
//	MG_BackupForce.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MG_Liquidator
{

    public class MG_BackupForce : Script
    {
        #region Fields
        //private static List<Blip> _carBlips = new List<Blip>();
        private static bool _spawned = false;
        #endregion Fields

        #region Properties
        //public static List<Vehicle> CreatedCars { get; set; } = new List<Vehicle>();
        public static List<Ped> Peds { get; set; } = new List<Ped>();
        public static int AmountSquadsCanBeSpawn { get; set; } = 3;
        #endregion Properties

        #region Constructor

        public MG_BackupForce()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick
 
        private void OnTick(object sender, EventArgs e)
        {
            if (_spawned)
            {
                CheckAlives();
            }
        }
        #endregion OnTick


        #region Public Methods

        public static void CreateBackup()
        {
            for (int i = 0; i < AmountSquadsCanBeSpawn; i++)
            {
                SpawnSquad();
                Wait(100);
                SpawnSquad();
                Wait(100);
                SpawnSquad();
                Wait(100);
            }
            Wait(100);
            Attack();
        }

        public static void Reset()
        {
            foreach (var ped in Peds)
            {
                //ped.CurrentBlip.Remove();
                //ped.IsPersistent = false;
                MG_ForgottenEnemy.AddPed(ped);
            }
            Peds.Clear();
            _spawned = false;
            //foreach (var blip in _carBlips)
            //{
            //    if (blip != null)
            //    {
            //        blip.Remove();
            //    }
            //}
        }
        #endregion Public Methods


        #region Private Methods

        private static void SpawnSquad()
        {
            string carModel = GetVehicleModel();

            Vehicle vehicle = MG_Vehicle.CreateVehicle(MG_Player.Ped.Position, 100f + MG_Random.Random(100), carModel);
            Blip blip = vehicle.AddBlip();
            blip.Sprite = BlipSprite.GetawayCar;
            blip.Color = BlipColor.Yellow;
            //_carBlips.Add(blip);
            MG_GarbageCollector.vehicleBlips.Add(blip);

            for (int i = 0; i < 4; i++)
            {
                if (MG_Vehicle.HasAnyFreeSeats(vehicle) == false) break;
                string pedModel = GetPedModel();
                Ped ped = MG_Vehicle.CreatePedInsideVehicle(vehicle, pedModel);
                Wait(100);
                if (ped.CurrentVehicle == null)
                {
                    //ЕСТЬ МОДЕЛЬ ТРАНСПОРТА, ГДЕ БОТЫ СОЗДАЛИСЬ В КОРОБКЕ ГРУЗОВИКА И НЕ СИДЕЛИ ТАМ, А ДВИГАЛИСЬ!
                    //Создадим транспорт для педа, который тупо отспавнился в коробке и не получил свое место
                    string vehicleStr = "NEMESIS";
                    Vehicle vehicleG = MG_Vehicle.CreateVehicle(MG_Player.Ped.Position, 50f + MG_Random.Random(30), vehicleStr);
                    blip = vehicleG.AddBlip();
                    blip.Sprite = BlipSprite.GetawayCar;
                    blip.Color = BlipColor.Orange;
                    //_carBlips.Add(blip);
                    MG_GarbageCollector.vehicleBlips.Add(blip);
                    ped.SetIntoVehicle(vehicleG, VehicleSeat.Driver);

                }
                InitPed(ped);
            }
            //Function.Call(Hash._TASK_VEHICLE_FOLLOW, vehicle.Driver, vehicle, MG_Target.Ped, 20f, DrivingStyle.Normal.GetHashCode(), 100);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!               
        }

        private static void InitPed(Ped ped)
        {
            Peds.Add(ped);
            ped.IsPersistent = true;
            Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 0, 0, 0);//HELMET

            MG_Group.SetGroup(ped, MG_Target.Ped, MG_TargetGroup.RelationsGroup, MG_TargetGroup.GroupID);

            Blip blip = ped.AddBlip();
            blip.Color = BlipColor.Yellow;

            //--Function.Call(Hash.SET_PED_AS_ENEMY, ped, true);
            Function.Call(Hash.SET_PED_COMBAT_RANGE, ped, (int)CombatRange.Far);
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);  // force peds to fight
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped, (int)CombatMovement.Offensive);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 2 | 46, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            ped.DrivingStyle = DrivingStyle.IgnoreLights;


            if (MG_Random.Random() > 50)
            {
                ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Handguns), 35, true, true);
            }
            else if (MG_Random.Random() > 35)
            {
                ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Shotguns), 12, true, true);
            }
            else
            {
                ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.AssaultRifles), 60, true, true);
            }

            if (MG_Target.Type.Equals(TargetType.Terrorist))
            {
                MG_Bombermania.MakeSuicideBomber(ped);
            }

            if (MG_Target.Type.Equals(TargetType.Police))
            {
                Function.Call(Hash.SET_PED_AS_COP, ped, true);
            }
        }

        private static void Attack()
        {
            Ped player = MG_Player.Ped;
            foreach (var ped in Peds)
            {
                //if (isPolice)
                //{
                //    guard.MarkAsNoLongerNeeded();
                //}

                ped.Task.FightAgainst(player);
                ped.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                if (ped.CurrentVehicle != null)
                {
                    Function.Call(Hash.SET_VEHICLE_SIREN, ped.CurrentVehicle, true);
                }

                //_guard.AlwaysKeepTask = true;
            }
        }

        private static void CheckAlives()
        {
            foreach (var ped in Peds.ToList())
            {
                if (ped.IsDead)
                {
                    ped.CurrentBlip.Remove();
                    Peds.Remove(ped);
                }
            }
        }

        private static string GetPedModel()
        {
            var type = MG_Target.Type;

            string[] chosenArray;

            switch (type)
            {
                case TargetType.Normal:
                    chosenArray = DB_Peds.BodyGuard_Normal;
                    break;
                case TargetType.Assasin:
                    chosenArray = DB_Peds.BodyGuard_Assasin;
                    break;
                case TargetType.Terrorist:
                    chosenArray = DB_Peds.BodyGuard_Bomber;
                    break;
                case TargetType.Police:
                    chosenArray = DB_Peds.BodyGuard_Police;
                    break;
                case TargetType.Cartel:
                    chosenArray = DB_Peds.BodyGuard_Cartel;
                    break;
                case TargetType.Hacker:
                    chosenArray = DB_Peds.BodyGuard_Hacker;
                    break;
                case TargetType.Military:
                    chosenArray = DB_Peds.BodyGuard_Military;
                    break;
                case TargetType.GangMember:
                    chosenArray = DB_Peds.BodyGuard_GangMember;
                    break;
                default:
                    chosenArray = DB_Peds.BodyGuard_Normal;
                    break;
            }

            string chosen = MG_Random.RandomElement(chosenArray);

            return chosen;
        }

        private static string GetVehicleModel()
        {
            var type = MG_Target.Type;

            string[] chosenArray;

            switch (type)
            {
                case TargetType.Normal:
                    chosenArray = DB_Vehicles.BodyGuard_Normal;
                    break;
                case TargetType.Assasin:
                    chosenArray = DB_Vehicles.BodyGuard_Assasin;
                    break;
                case TargetType.Terrorist:
                    chosenArray = DB_Vehicles.BodyGuard_Bomber;
                    break;
                case TargetType.Police:
                    chosenArray = DB_Vehicles.BodyGuard_Police;
                    break;
                case TargetType.Cartel:
                    chosenArray = DB_Vehicles.BodyGuard_Cartel;
                    break;
                case TargetType.Hacker:
                    chosenArray = DB_Vehicles.BodyGuard_Hacker;
                    break;
                case TargetType.Military:
                    chosenArray = DB_Vehicles.BodyGuard_Military;
                    break;
                case TargetType.GangMember:
                    chosenArray = DB_Vehicles.BodyGuard_GangMember;
                    break;
                default:
                    chosenArray = DB_Vehicles.BodyGuard_Normal;
                    break;
            }

            string chosen = MG_Random.RandomElement(chosenArray);

            return chosen;
        }
        #endregion Private Methods

    }
}