////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetBodyGuards.cs
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

    public class MG_TargetBodyGuards : Script
    {
        #region Fields
        //private static Vehicle _lastCreatedVehicle = null;
        private static bool _areGuardsAttacking = false;
        private static int _coolDownTaskRunTo = 0;
        private static int _passengersAdded = 0;
        //private static List<Blip> _carBlips = new List<Blip>();
        private static List<Vehicle> _fullVehicles = new List<Vehicle>();
        #endregion Fields

        #region Properties
        //public static List<Vehicle> CreatedCars { get; set; } = new List<Vehicle>();

        public static List<Ped> Bodyguards { get; set; } = new List<Ped>();
        public static List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public static bool HasSpawned { get; set; } = false;
        public static int AmountCanBeSpawn { get; set; } = 0;
        #endregion Properties

        #region Constructor

        public MG_TargetBodyGuards()
        {

            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (HasSpawned)
            {
                if (MG_TargetAI.IsCompromised)
                {

                    if (!_areGuardsAttacking)
                    {
                        Attack();
                    }

                }
                else
                {
                    CheckForSuspiciousActivity();
                    DoSomethingIfStandingStill();
                }
                CheckAlives();
            }
        }
        #endregion OnTick

        #region Public Methods

        public static void SpawnBodyGuards()
        {
            if (HasSpawned == false)
            {
                if (AmountCanBeSpawn > 0)
                {
                    for (int i = 0; i < AmountCanBeSpawn; i++)
                    {
                        Ped ped = Spawn(MG_Target.Ped, MG_TargetGroup.RelationsGroup, MG_TargetGroup.PedGroup);
                        Bodyguards.Add(ped);
                    }
                }

                //--------if (MG_Target.Ped.IsInVehicle()) MakeConvoy();
                HasSpawned = true;
            }
        }

        public static Ped Spawn(Ped leader, int relationsGroup, PedGroup group)
        {
            bool istargetInVehicle = leader.IsInVehicle();
            string pedModelStr = GetPedModel();

            Ped ped;
            if (istargetInVehicle)
            {
                List<Ped> bodyGuardsAndLeader = Bodyguards.ToList();
                bodyGuardsAndLeader.Add(leader);
                bool createVehicle = true;
                Vehicle vehicleG = null;

                if (_passengersAdded < 3)//Сделано для того чтобы предотвратить спавнить бесконечно в коробке грузовиков и пикапов без сидений.
                {
                    foreach (var pedG in bodyGuardsAndLeader)
                    {
                        bool isInVehicle = pedG.IsInVehicle();
                        if (isInVehicle)
                        {
                            vehicleG = pedG.CurrentVehicle;

                            if (_fullVehicles.Contains(vehicleG))//Сделано для того чтобы предотвратить спавнить бесконечно в коробке грузовиков и пикапов без сидений.
                            {
                                continue;
                            }
                            else if (_passengersAdded < 3)
                            {
                                _fullVehicles.Add(vehicleG);//Сделано для того чтобы предотвратить спавнить бесконечно в коробке грузовиков и пикапов без сидений.
                            }

                            bool anyFreeSeats = MG_Vehicle.HasAnyFreeSeats(vehicleG);
                            if (anyFreeSeats)
                            {
                                _passengersAdded += 1;
                                createVehicle = false;
                                break;
                            }
                        }
                    }
                }

                if (createVehicle)
                {
                    string vehicleStr = GetVehicleModel();
                    vehicleG = MG_Vehicle.CreateVehicle(leader.Position, 150f, vehicleStr);
                    vehicleG.IsPersistent = true;
                    Vehicles.Add(vehicleG);
                    _passengersAdded = 0;
                }

                ped = MG_Vehicle.CreatePedInsideVehicle(vehicleG, pedModelStr);
            }
            else
            {
                Model pedModel = new Model(pedModelStr);
                pedModel.Request(500);
                ped = World.CreatePed(pedModelStr, World.GetNextPositionOnSidewalk(leader.Position.Around(15 + MG_Random.Random(25))));//NEW
                pedModel.MarkAsNoLongerNeeded();
            }

            Wait(50);
            InitGuard(ped, leader);
            if (istargetInVehicle && ped.IsInVehicle(leader.CurrentVehicle) == false)
            {
                //string ErrorText;
                if (ped.CurrentVehicle == null)
                {
                    //ЕСТЬ МОДЕЛЬ ТРАНСПОРТА, ГДЕ БОТЫ СОЗДАЛИСЬ В КОРОБКЕ ГРУЗОВИКА И НЕ СИДЕЛИ ТАМ, А ДВИГАЛИСЬ!
                    //Создадим транспорт для педа, который тупо отспавнился в коробке и не получил свое место
                    string vehicleStr = "NEMESIS";
                    Vehicle vehicleG = MG_Vehicle.CreateVehicle(leader.Position, 150f, vehicleStr);
                    vehicleG.IsPersistent = true;
                    Vehicles.Add(vehicleG);
                    _passengersAdded = 0;
                    ped.SetIntoVehicle(vehicleG, VehicleSeat.Driver);

                }

                //if (ped.CurrentVehicle.Driver == null)
                //{
                //    ErrorText = "ERROR: MG_TargetBodyGuards CODE 2";
                //    MG_Message.SMS(ErrorText, 60);
                //}
                //else
                //{
                Function.Call(Hash._TASK_VEHICLE_FOLLOW, ped.CurrentVehicle.Driver, ped.CurrentVehicle, leader, 60f, DrivingStyle.Rushed.GetHashCode(), 15);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!
                //}

            }
            return ped;
        }

        public static void Reset()
        {
            foreach (var ped in Bodyguards)
            {
                //ped.CurrentBlip.Remove();
                //ped.IsPersistent = false;
                MG_ForgottenEnemy.AddPed(ped);
            }
            Bodyguards.Clear();

            //foreach (var blip in _carBlips)
            //{
            //    if (blip != null)
            //    {
            //        blip.Remove();
            //    }
            //}

            _fullVehicles.Clear();
            _coolDownTaskRunTo = 0;
            _areGuardsAttacking = false;

            HasSpawned = false;
        }

        public static void CreateBlips()
        {

            Blip blip;
            if (Vehicles.Any())
            {
                foreach (var vehicle in Vehicles)
                {
                    if (vehicle.IsDead) continue;

                    blip = vehicle.AddBlip();
                    blip.Sprite = BlipSprite.GetawayCar;
                    blip.Color = BlipColor.Orange;
                    MG_GarbageCollector.vehicleBlips.Add(blip);
                }
            }

            if (Bodyguards.Any())
            {
                foreach (var ped in Bodyguards)
                {
                    if (ped.IsDead) continue;

                    blip = ped.AddBlip();
                    blip.Color = BlipColor.Orange;
                }
            }
        }
        #endregion Public Methods

        #region Private Methods

        private static void CheckForSuspiciousActivity()
        {
            foreach (var ped in Bodyguards)
            {
                if (ped.IsAlive)
                {
                    MG_Stealth.CheckIfPlayerCompromised(ped);
                }
            }
        }

        private static void InitGuard(Ped ped, Ped leader)
        {
            ped.IsPersistent = true;
            ped.Accuracy = MG_Random.Random(10, 35);
            Function.Call(Hash.SET_PED_PROP_INDEX, ped, 0, 0, 0, 0);//HELMET

            MG_Group.SetGroup(ped, leader, MG_TargetGroup.RelationsGroup, MG_TargetGroup.GroupID);

            Function.Call(Hash.SET_PED_COMBAT_RANGE, ped, (int)CombatRange.Far);
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 46, true);  // force peds to fight
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped, (int)CombatMovement.Offensive);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, 2 | 46, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            ped.DrivingStyle = DrivingStyle.IgnoreLights;

            ped.Weapons.Give(GetWeaponModel(), 50, true, true);
            //if (MG_Random.Random() > 50)
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Handguns), 35, true, true);
            //}
            //else if (MG_Random.Random() > 35)
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.Shotguns), 12, true, true);
            //}
            //else
            //{
            //    ped.Weapons.Give(MG_Random.RandomElement(DB_Weapons.AssaultRifles), 60, true, true);
            //}

            if (MG_Target.Type.Equals(TargetType.Terrorist))
            {
                MG_Bombermania.MakeSuicideBomber(ped);
            }

            if (MG_Target.Type.Equals(TargetType.Police))
            {
                Function.Call(Hash.SET_PED_AS_COP, ped, true);
            }
            //else
            //{
            //    Function.Call(Hash.SET_PED_AS_COP, ped, false);
            //}

            //ped.Task.FightAgainst(leader);
            //MG_WeaponManager.ProcessPed(ped, GenerateRearmLevel());
            //SetAccary(ped);
            //SetArmor(ped);
            //TargetBodyGuards.Add(ped);

            //_guard.CanSwitchWeapons = true;
            //_guard.FiringPattern = FiringPattern.SingleShot;
            //_guard.Accuracy = 25;
            //_guard.MaxDrivingSpeed = 500;
            //_guard.MaxSpeed = 500;
            //Terminator.get_Task().WarpIntoVehicle(tBike, (VehicleSeat)(-1));
            ///------------TeleportToTargetVehicle(_guard, _leader);
            //return _guard;
        }

        private static void Attack()
        {
            Ped player = MG_Player.Ped;
            Ped target = MG_Target.Ped;
            bool targetInCar = target.IsInVehicle();
            Vehicle vehicle = null;
            if (targetInCar)
            {
                vehicle = target.CurrentVehicle;
            }

            foreach (var guard in Bodyguards)
            {
                //if (isPolice)
                //{
                //    guard.MarkAsNoLongerNeeded();
                //}

                guard.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
                if (targetInCar)
                {
                    if (guard.IsInVehicle(vehicle))
                    {
                        switch (MG_Target.Morale)
                        {
                            case TargetMorale.Low:
                                guard.Task.FleeFrom(player);
                                break;
                            case TargetMorale.Medium:
                                guard.Task.FleeFrom(player);
                                break;
                            case TargetMorale.High:
                                guard.Task.FightAgainst(player);
                                break;
                            default:
                                guard.Task.FleeFrom(player);
                                break;
                        }
                    }
                    else
                    {
                        guard.Task.FightAgainst(player);
                    }
                }
                else
                {
                    guard.Task.FightAgainst(player);
                }


                //_guard.AlwaysKeepTask = true;
            }
            _areGuardsAttacking = true;
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

        private static WeaponHash GetWeaponModel()
        {
            var type = MG_Target.Type;

            WeaponHash[] chosenArray;

            switch (type)
            {
                case TargetType.Normal:
                    chosenArray = DB_Weapons.BodyGuard_Normal;
                    break;
                case TargetType.Assasin:
                    chosenArray = DB_Weapons.BodyGuard_Assasin;
                    break;
                case TargetType.Terrorist:
                    chosenArray = DB_Weapons.BodyGuard_Bomber;
                    break;
                case TargetType.Police:
                    chosenArray = DB_Weapons.BodyGuard_Police;
                    break;
                case TargetType.Cartel:
                    chosenArray = DB_Weapons.BodyGuard_Cartel;
                    break;
                case TargetType.Hacker:
                    chosenArray = DB_Weapons.BodyGuard_Hacker;
                    break;
                case TargetType.Military:
                    chosenArray = DB_Weapons.BodyGuard_Military;
                    break;
                case TargetType.GangMember:
                    chosenArray = DB_Weapons.BodyGuard_GangMember;
                    break;
                default:
                    chosenArray = DB_Weapons.BodyGuard_Normal;
                    break;
            }

            WeaponHash chosen = MG_Random.RandomElement(chosenArray);

            return chosen;
        }

        //private static void MakeConvoy()
        //{
        //    List<Ped> pedsG = MG_Target.Ped.CurrentPedGroup.ToList(false);
        //    List<Vehicle> vehicles = new List<Vehicle>();
        //    foreach (var ped in pedsG)
        //    {
        //        Vehicle vehicle = ped.CurrentVehicle;
        //        if (MG_Target.Ped.IsInVehicle(vehicle) == false)
        //        {
        //            if (vehicles.Contains(vehicle) == false)
        //            {
        //                vehicles.Add(vehicle);
        //            }
        //        }
        //    }

        //    Ped following = MG_Target.Ped;
        //    foreach (var vehicle in vehicles)
        //    {
        //        Function.Call(Hash._TASK_VEHICLE_FOLLOW, vehicle.Driver, vehicle, following, 60f, DrivingStyle.Rushed.GetHashCode(), 15);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!
        //        following = vehicle.Driver;
        //    }
        //}

        private static void CheckAlives()
        {
            foreach (var ped in Bodyguards.ToList())
            {
                if (ped.IsDead)
                {
                    ped.CurrentBlip.Remove();
                    Bodyguards.Remove(ped);
                    ped.IsPersistent = false;///NEW
                    ped.MarkAsNoLongerNeeded();///NEW
                }
            }
        }

        private static void DoSomethingIfStandingStill()
        {
            if (_coolDownTaskRunTo > 30)
            {
                _coolDownTaskRunTo = 0;
                Ped leader = MG_Target.Ped;
                //int currentPedNum = 0;
                foreach (var ped in Bodyguards.ToArray())
                {
                    //currentPedNum++;
                    //if (currentPedNum < 6)//Первые шесть по умолчанию двигаются с лидером, остальные как пни стоят! Как в ОФП ограничение.
                    //{
                    //    continue;
                    //}


                    //if (ped.IsAlive)
                    //{

                    //    if (currentPedNum == 0)
                    //    {
                    //        if (ped.CurrentPedGroup != null)
                    //        {
                    //            ped.LeaveGroup();
                    //            ped.Task.WanderAround();
                    //        }

                    //        if (leader.IsOnFoot && !leader.IsRunning && !leader.IsWalking)//ped.IsStopped &&  !ped.IsRagdoll && !ped.IsInjured &&  && !ped.IsRunning && !ped.IsWalking
                    //        {
                    //            float distance = Vector2.Distance(ped.Position, leader.Position);
                    //            if (distance > 25f)
                    //            {
                    //                //MG_Message.SubTitle("MOVE!");
                    //                leader.Task.RunTo(ped.Position.Around(2f));
                    //                //PedsAreRunningToLeader.Add(ped);
                    //            }
                    //            else if (distance > 11f)
                    //            {
                    //                leader.Task.GoTo(ped.Position.Around(2f));
                    //            }
                    //        }

                    //    }



                    //    if (ped.IsOnFoot && !ped.IsRunning && !ped.IsWalking)//ped.IsStopped &&  !ped.IsRagdoll && !ped.IsInjured &&  && !ped.IsRunning && !ped.IsWalking
                    //    {
                    //        float distance = Vector2.Distance(ped.Position, leader.Position);
                    //        if (distance > 25f)
                    //        {
                    //            //MG_Message.SubTitle("MOVE!");
                    //            ped.Task.RunTo(leader.Position.Around(5f));
                    //            //PedsAreRunningToLeader.Add(ped);
                    //        }
                    //        else if (distance > 11f)
                    //        {
                    //            ped.Task.GoTo(leader.Position.Around(5f));
                    //        }
                    //    }
                    //    currentPedNum++;
                    //}

                    if (ped.IsAlive)
                    {
                        if (ped.IsOnFoot && !ped.IsRunning && !ped.IsWalking)//ped.IsStopped &&  !ped.IsRagdoll && !ped.IsInjured &&  && !ped.IsRunning && !ped.IsWalking
                        {
                            float distance = Vector2.Distance(ped.Position, leader.Position);
                            if (distance > 25f)
                            {
                                //MG_Message.SubTitle("MOVE!");
                                ped.Task.RunTo(leader.Position.Around(5f));
                                //PedsAreRunningToLeader.Add(ped);
                            }
                            else if (distance > 11f)
                            {
                                ped.Task.GoTo(leader.Position.Around(5f));
                            }
                        }
                    }


                    //////if (ped.IsAlive)
                    //////{
                    //////    if (ped.IsOnFoot && !ped.IsRunning && !ped.IsWalking)//ped.IsStopped &&  !ped.IsRagdoll && !ped.IsInjured &&  && !ped.IsRunning && !ped.IsWalking
                    //////    {
                    //////        float randomRadius = (float)MG_Random.Random(5, 30);
                    //////        Vector3 randomPos = leader.Position.Around(randomRadius);
                    //////        //float distance = Vector2.Distance(ped.Position, leader.Position);//OLD
                    //////        float distance = Vector2.Distance(ped.Position, randomPos);
                    //////        if (distance > 25f)
                    //////        {
                    //////            //MG_Message.SubTitle("MOVE!");

                    //////            //ped.Task.RunTo(leader.Position.Around(5f));//OLD
                    //////            ped.Task.RunTo(randomPos);

                    //////        }
                    //////        else if (distance > 10f)
                    //////        {

                    //////            // ped.Task.GoTo(leader.Position.Around(5f));//OLD
                    //////            ped.Task.GoTo(randomPos);
                    //////        }
                    //////        //CheckIfPlayerCompomised(Ped _player, Ped _target)
                    //////    }
                    //////}
                }
            }
            else
            {
                _coolDownTaskRunTo++;
            }
        }
        #endregion Private Methods

    }
}



