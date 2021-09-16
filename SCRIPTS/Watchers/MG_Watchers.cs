////////////////////////////////////////////////////////////////////////////////
//
//	MG_Watchers.cs
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

    public class MG_Watchers : Script
    {
        #region Fields
        //private static bool _areMovingToRamPlayer = false;
        private static bool _revealed = false;
        private static bool _areWatchersAttacking = false;
        private static bool _isLeaderCreated = false;
        //private static List<Blip> _carBlips = new List<Blip>();
        private static List<string[]> _squadTempate = new List<string[]>()
        {
            DB_Watchers.STEALTH_AMBULANCE_SWAT_SET,
            DB_Watchers.STEALTH_AMBULANCE2_SWAT_SET,
            DB_Watchers.STEALTH_TAXI_SWAT_SET,
            DB_Watchers.Combat_Riot_SWAT_SET,
            DB_Watchers.UNMARKED_PoliceCruiser_SWAT_SET,
            DB_Watchers.UNMARKED_PoliceCruiser_FBI_SET,
            DB_Watchers.UNMARKED_PoliceOffroad_SWAT_SET,
            DB_Watchers.UNMARKED_PoliceOffroad_FBI_SET
        };

        private static string[][] _weapons = { DB_Watchers.WEAPON_SET_ASSASULTRIFLE, DB_Watchers.WEAPON_SET_SHOTGUN, DB_Watchers.WEAPON_SET_SMG };
        private static WeaponComponent[][][] _weapons_a = { DB_Watchers.WEAPON_SET_SMG_A, DB_Watchers.WEAPON_SET_SHOTGUN_A, DB_Watchers.WEAPON_SET_ASSASULTRIFLE_A };
        #endregion Fields

        #region Properties
        public static List<Ped> Watchers { get; set; } = new List<Ped>();
        public static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public static bool HasSpawned { get; set; } = false;
        public static int AmountSquadsCanBeSpawn { get; set; } = 0;
        //public static bool Compromised { get; set; } = false;
        #endregion Properties

        #region Constructor
 
        public MG_Watchers()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (HasSpawned)
            {
                if (!_areWatchersAttacking)
                {
                    if (MG_TargetAI.IsCompromised)
                    {
                        Attack();
                        //if (_areMovingToRamPlayer == false)
                        //{
                        //    GoRamPlayer();
                        //}
                    }
                    else
                    {
                        CheckForSuspiciousActivity();
                    }
                }
                CheckAlives();
            }
        }
        #endregion OnTick

        #region Public Methods

        public static void SpawnBodyWatchers()
        {
            if (HasSpawned == false)
            {
                if (AmountSquadsCanBeSpawn > 0)
                {
                    for (int i = 0; i < AmountSquadsCanBeSpawn; i++)
                    {
                        SpawnSquad_Heavy();
                        SpawnSquad_Van();
                        SpawnSquad_Car();
                        //Ped ped = Spawn(MG_Target.Ped, MG_WatchersGroup.RelationsGroup, MG_WatchersGroup.Group);
                    }
                }
                _revealed = false;//reset
                HasSpawned = true;
            }
        }

        public static void Reset()
        {
            foreach (var ped in Watchers)
            {
                //ped.CurrentBlip.Remove();
                MG_ForgottenEnemy.AddPed(ped);
                //ped.IsPersistent = false;
            }
            Watchers.Clear();

            if (Vehicles.Any())
            {
                Vehicles.Clear();
            }
           
            //_areMovingToRamPlayer = false;
            _isLeaderCreated = false;
            _areWatchersAttacking = false;

            HasSpawned = false;
        }

        #endregion Public Methods

        #region Private Methods
   
        private static void SpawnSquad_Heavy()
        {
            string[] squadTemplate = ChooseSquadTemplate();

            bool isCarCreated = false;
            Vehicle vehicle = null;
            foreach (var item in squadTemplate)
            {
                if (isCarCreated == false)
                {
                    vehicle = MG_Vehicle.CreateVehicle(MG_Player.Ped.Position, 200f + MG_Random.Random(100), item);
                    //Blip blip = vehicle.AddBlip();
                    //blip.Sprite = BlipSprite.GetawayCar;
                    //blip.Color = BlipColor.Purple;
                    //_carBlips.Add(blip);
                    Vehicles.Add(vehicle);


                    isCarCreated = true;
                    //MG_File.Save((vehicle == null) + " " + item);
                }
                else
                {

                    Ped ped = MG_Vehicle.CreatePedInsideVehicle(vehicle, item);
                    if (_isLeaderCreated == false)
                    {
                        MG_WatchersGroup.InitGroup(ped);
                        _isLeaderCreated = true;
                    }
                    Wait(100);
                    InitGhost(ped);
                }
            }
            Function.Call(Hash._TASK_VEHICLE_FOLLOW, vehicle.Driver, vehicle, MG_Target.Ped, 20f, DrivingStyle.Normal.GetHashCode(), 100);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!               
        }

        private static void SpawnSquad_Van()
        {
            string carModel = MG_Random.RandomElement(DB_Watchers.STEALTH_VANCAR_PACK.ToList());
            string pedModel = "S_M_Y_SWAT_01";
            Vehicle vehicle = MG_Vehicle.CreateVehicle(MG_Player.Ped.Position, 200f + MG_Random.Random(100), carModel);
            //Blip blip = vehicle.AddBlip();
            //blip.Sprite = BlipSprite.GetawayCar;
            //blip.Color = BlipColor.Purple;
            Vehicles.Add(vehicle);

            for (int i = 0; i < 4; i++)
            {
                Ped ped = MG_Vehicle.CreatePedInsideVehicle(vehicle, pedModel);
                Wait(100);
                InitGhost(ped);
            }
            Function.Call(Hash._TASK_VEHICLE_FOLLOW, vehicle.Driver, vehicle, MG_Target.Ped, 20f, DrivingStyle.Normal.GetHashCode(), 100);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!               
        }

        private static void SpawnSquad_Car()
        {
            string carModel = MG_Random.RandomElement(DB_Watchers.STEALTH_VANCAR_PACK.ToList());
            string pedModel = "S_M_Y_SWAT_01";
            Vehicle vehicle = MG_Vehicle.CreateVehicle(MG_Player.Ped.Position, 200f + MG_Random.Random(100), carModel);
            //Blip blip = vehicle.AddBlip();
            //blip.Sprite = BlipSprite.GetawayCar;
            //blip.Color = BlipColor.Purple;
            Vehicles.Add(vehicle);

            for (int i = 0; i < 4; i++)
            {
                if (MG_Vehicle.HasAnyFreeSeats(vehicle) == false) break;
                Ped ped = MG_Vehicle.CreatePedInsideVehicle(vehicle, pedModel);
                Wait(100);
                InitGhost(ped);
            }
            Function.Call(Hash._TASK_VEHICLE_FOLLOW, vehicle.Driver, vehicle, MG_Target.Ped, 20f, DrivingStyle.Normal.GetHashCode(), 100);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!               
        }

        private static string[] ChooseSquadTemplate()
        {
            return MG_Random.RandomElement(_squadTempate);
        }

        private static void CheckForSuspiciousActivity()
        {
            foreach (var ped in Watchers)
            {
                if (ped.IsAlive)
                {
                    bool compromised = MG_Stealth.CheckIfPlayerCompromisedByWatchers(ped);
                    if (compromised)
                    {
                        Attack();
                    }
                }
            }
        }

        private static void InitGhost(Ped ped)
        {
            ped.IsPersistent = true;
            Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 0, 0, 0);//HELMET
            ped.Armor = 100;
            ped.Accuracy = MG_Random.Random(40, 50);
            ped.CanSufferCriticalHits = false;
            //ped.Health = 200;
            MG_Group.SetGroup(ped, MG_WatchersGroup.Leader, MG_WatchersGroup.RelationsGroup, MG_WatchersGroup.GroupID);

            //Blip blip = ped.AddBlip();
            //blip.Sprite = BlipSprite.Ghost;
            //blip.Color = BlipColor.PurpleDark;

            //Function.Call(Hash.SET_PED_AS_ENEMY, ped, true);
            Function.Call(Hash.SET_PED_COMBAT_RANGE, ped, (int)CombatRange.Far);
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);  // force peds to fight
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped, (int)CombatMovement.Offensive);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 2 | 46, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            Function.Call(Hash.SET_PED_AS_COP, ped, true);
            ped.DrivingStyle = DrivingStyle.IgnoreLights;

            Rearm(ped);

            Watchers.Add(ped);
        }

        private static void Attack()
        {
            Ped player = MG_Player.Ped;
            foreach (var ped in Watchers)
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
            _areWatchersAttacking = true;
            Reveal();
        }

        private static void CheckAlives()
        {
            foreach (var ped in Watchers.ToList())
            {
                if (ped.IsDead)
                {
                    if (ped.CurrentBlip != null)
                    {
                        ped.CurrentBlip.Remove();
                    }                  
                    Watchers.Remove(ped);
                }
            }
        }

        private static void Rearm(Ped ped)
        {
            //ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Handguns), 35, true, true);
            //if (MG_Random.Random() > 60)
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.SMG), 60, true, true);
            //}
            //else if (MG_Random.Random() > 75)
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Shotguns), 20, true, true);
            //}
            //else
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.AssaultRifles), 60, true, true);
            //}


            int chosen = MG_Random.Random(0, _weapons.Length);
            string[] chosenWeaponSet = _weapons[chosen];
            WeaponComponent[][] chosenComponentSet = _weapons_a[chosen];

            string handgun = chosenWeaponSet[0];
            WeaponComponent[] handgunAttachmentsSet = chosenComponentSet[0];
            string primaryWeapon = chosenWeaponSet[1];
            WeaponComponent[] primaryWeaponAttachmentsSet = chosenComponentSet[1];

            ped.Weapons.Give(handgun, 50, true, true);
            foreach (var attachment in handgunAttachmentsSet)
            {
                ped.Weapons.Current.SetComponent(attachment, true);
            }

            ped.Weapons.Give(primaryWeapon, 90, true, true);
            foreach (var attachment in primaryWeaponAttachmentsSet)
            {
                ped.Weapons.Current.SetComponent(attachment, true);
            }
        }

        //private static void GoRamPlayer()
        //{
        //    _areMovingToRamPlayer = true;
        //    foreach (var ped in Watchers.ToHashSet())
        //    {
        //        if (ped.IsAlive)
        //        {
        //            if (ped.IsInVehicle())
        //            {
        //                if (ped.CurrentVehicle.Driver != null)
        //                {
        //                    Function.Call(Hash._TASK_VEHICLE_FOLLOW, ped.CurrentVehicle.Driver, ped.CurrentVehicle, MG_Target.Ped, 20f, DrivingStyle.AvoidTrafficExtremely.GetHashCode(), 1);
        //                }

        //            }
        //        }
        //    }

        //}

        private static void Reveal()
        {
            if (_revealed) return;
            _revealed = true;

            foreach (var vehicle in Vehicles)
            {
                if (vehicle != null)
                {
                    if (vehicle.IsAlive)
                    {
                        Blip blip = vehicle.AddBlip();
                        blip.Sprite = BlipSprite.GetawayCar;
                        blip.Color = BlipColor.Purple;
                        MG_GarbageCollector.vehicleBlips.Add(blip);
                    }
                }
            }

            foreach (var ped in Watchers)
            {
                if (ped != null)
                {
                    if (ped.IsAlive)
                    {
                        Blip blip = ped.AddBlip();
                        blip.Sprite = BlipSprite.Ghost;
                        blip.Color = BlipColor.PurpleDark;
                    }
                }
            }
        }

        #endregion Private Methods

    }
}



