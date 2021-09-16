////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetAI.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;

public enum TargetCurrentTask { NotSet, FleeingOnFoot, FleeingOnFootInPanic, FleeingOnVehicle, FleeingOnVehicleInPanic, TryingToStealCar, FightingOnFoot, FightingOnVehicle, GivingUp, SelfDefenceOnFoot };


namespace MG_Liquidator
{

    class MG_TargetAI : Script
    {

        #region Fields
        private static string[] _sounds_IGiveUp_male = new string[]
        {
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_male_01.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_male_02.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_male_03.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_male_04.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_male_05.mp3"
        };

        private static string[] _sounds_IGiveUp_female = new string[]
        {
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_female_01.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_female_02.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_female_03.mp3",
            @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\GiveUp_female_04.mp3"
        };

        #endregion Fields

        #region Properties     
        public static bool IsCompromised { get; set; } = false;
        public static bool IsGivingUp { get; set; } = false;
        public static Vehicle VehicleTarget { get; set; } = null;
        public static TargetCurrentTask CurrentAction { get; set; } = TargetCurrentTask.NotSet;
        public static Blip VehicleTarget_blip { get; set; }
        #endregion Properties

        #region Constructor   

        public MG_TargetAI()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick   

        private void OnTick(object sender, EventArgs e)
        {
            if (MG_TargetFinder.IsTargetFound)
            {
                if (IsCompromised)
                {
                    ActAgainstPlayer();
                    //MG_Message.HelpMessage("CurrentAction= " + CurrentAction + " Morale=" + MG_Target.Morale);
                }
                else
                {
                    MG_Stealth.CheckIfPlayerCompromised(MG_Target.Ped);
                }
            }
        }
        #endregion OnTick

        #region Public Methods

        public static void ActAgainstPlayer()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;

            if (target.IsDead)
            {
                Wait(3000);
                return;
            }

            //if (MG_Target.Type.Equals(TargetType.Terrorist))
            //{
            //    if (MG_Bombermania.IsEnemyClose(target, player))
            //    {
            //        MG_Bombermania.BOOM(target);
            //        Wait(5000);
            //        return;
            //    }
            //}

            switch (CurrentAction)
            {
                case TargetCurrentTask.NotSet:
                    SetPrimaryTask(target, player);
                    break;
                case TargetCurrentTask.FleeingOnFoot:
                    FleeingOnFoot(target, player);
                    break;
                case TargetCurrentTask.FleeingOnFootInPanic:
                    FleeingOnFootInPanic(target, player);
                    break;
                case TargetCurrentTask.FleeingOnVehicle:
                    FleeingOnVehicle(target, player);
                    break;
                case TargetCurrentTask.FleeingOnVehicleInPanic:
                    FleeingOnVehicleInPanic(target, player);
                    break;
                case TargetCurrentTask.TryingToStealCar:
                    TryingToStealCar(target, player);
                    break;
                case TargetCurrentTask.FightingOnFoot:
                    FightingOnFoot(target, player);
                    break;
                case TargetCurrentTask.FightingOnVehicle:
                    FightingOnVehicle(target, player);
                    break;
                case TargetCurrentTask.GivingUp:
                    GivingUp(target, player);
                    break;
                case TargetCurrentTask.SelfDefenceOnFoot:
                    SelfDefenceOnFoot(target, player);
                    break;
                default:
                    SetPrimaryTask(target, player);
                    break;
            }
            //IsDoingNothing(target);
            //Wait(1000);
            Wait(250);
        }

        public static void Reset()
        {
            IsCompromised = false;
            IsGivingUp = false;
            CurrentAction = TargetCurrentTask.NotSet;

            if (VehicleTarget != null)
            {
                VehicleTarget.IsPersistent = false;
                VehicleTarget.MarkAsNoLongerNeeded();
                VehicleTarget = null;
            }

            if (VehicleTarget_blip != null)
            {
                VehicleTarget_blip.Remove();
            }

        }
        #endregion Public Methods

        #region Private Methods SetPrimaryTask

        private static void SetPrimaryTask(Ped target, Ped player)
        {

            bool isTargetInVehicle = target.IsInVehicle();

            switch (MG_Target.Morale)
            {
                case TargetMorale.Low:
                    if (isTargetInVehicle)
                    {
                        Begin_DriveAwayInPanic(target, player);
                    }
                    else
                    {
                        Begin_PanicFlee(target, player);
                    }
                    break;
                case TargetMorale.Medium:
                    if (isTargetInVehicle)
                    {
                        Begin_DriveAway(target, player);
                    }
                    else
                    {
                        Begin_Flee(target, player);
                    }
                    break;
                case TargetMorale.High:
                    if (isTargetInVehicle)
                    {
                        Begin_FightOnVehicle(target, player);
                    }
                    else
                    {
                        Begin_FightOnFoot(target, player);
                    }
                    break;
                default:
                    if (isTargetInVehicle)
                    {
                        Begin_FightOnVehicle(target, player);
                    }
                    else
                    {
                        Begin_FightOnFoot(target, player);
                    }
                    break;
            }
        }
        #endregion Private Methods SetPrimaryTask

        #region Private Methods Начальные действия

        private static void Begin_PanicFlee(Ped target, Ped player)
        {
            target.Task.ReactAndFlee(player);
            CurrentAction = TargetCurrentTask.FleeingOnFootInPanic;
        }

        private static void Begin_Flee(Ped target, Ped player)
        {
            target.Task.FleeFrom(player);
            CurrentAction = TargetCurrentTask.FleeingOnFoot;
        }

        private static void Begin_DriveAway(Ped target, Ped player)
        {

            Vehicle targetVehicle = target.CurrentVehicle;
            MG_Target.Vehicle = targetVehicle;
            Ped[] crews = targetVehicle.Occupants;
            foreach (var crew in crews)
            {
                if (crew.IsDead) continue;
                crew.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;//NEW
                crew.MaxDrivingSpeed = 250f;
                crew.Task.CruiseWithVehicle(targetVehicle, 250f);
                crew.Task.FleeFrom(player);
            }
            CurrentAction = TargetCurrentTask.FleeingOnVehicle;
        }

        private static void Begin_DriveAwayInPanic(Ped target, Ped player)
        {

            Vehicle targetVehicle = target.CurrentVehicle;
            MG_Target.Vehicle = targetVehicle;
            Ped[] crews = targetVehicle.Occupants;
            foreach (var crew in crews)
            {
                if (crew.IsDead) continue;
                crew.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;//NEW
                crew.MaxDrivingSpeed = 250f;
                crew.Task.CruiseWithVehicle(targetVehicle, 250f);
                crew.Task.ReactAndFlee(player);
            }
            CurrentAction = TargetCurrentTask.FleeingOnVehicleInPanic;
        }

        private static void Begin_FightOnFoot(Ped target, Ped player)
        {
            target.Task.FightAgainst(player);
            CurrentAction = TargetCurrentTask.FightingOnFoot;
        }

        private static void Begin_SelfDefenceOnFoot(Ped target, Ped player)
        {
            target.Task.FightAgainst(player);
            CurrentAction = TargetCurrentTask.SelfDefenceOnFoot;
        }

        private static void Begin_FightOnVehicle(Ped target, Ped player)
        {
            Ped[] crews = target.CurrentVehicle.Occupants;
            foreach (var crew in crews)
            {
                if (crew.IsDead) continue;
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, crew, 2 | 46, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH
                crew.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;//NEW
                crew.MaxDrivingSpeed = 250f;
                crew.Task.FightAgainst(player);
            }

            CurrentAction = TargetCurrentTask.FightingOnVehicle;
            //MG_Message.SMS("FightOnVehicle");
        }
        #endregion Private Methods Начальные действия

        #region Private Methods Продолжения действий

        private static void FleeingOnFoot(Ped target, Ped player)
        {


            if (IsPlayerTooClose(target, player))
            {
                Begin_SelfDefenceOnFoot(target, player);
                return;
            }

            //EXPERIMENTAL
            if (target.IsInCombat)
            {
                ResetFleeing();
                //return;
            };
            //EXPERIMENTAL


            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                //CurrentAction = TargetCurrentTask.FleeingOnVehicle;
                Begin_DriveAway(target, player);
            }
            else
            {
                //WIP искать машину
                var vehicle = MG_Vehicle.FindSafestVehicleForTarget(target, player);
                if (vehicle != null)
                {
                    MG_Target.ResetCurrentVehicle();
                    MG_Target.SetCurrentVehicle(vehicle);
                    //target.Task.RunTo(vehicle.Position);
                    MG_InnocentManager.All_GetOut(vehicle);
                    target.Task.EnterVehicle(vehicle, VehicleSeat.Driver, 999999, 2f);
                    CurrentAction = TargetCurrentTask.TryingToStealCar;
                    //Wait(2000);//2500
                    //return;
                }
            }
            //MG_Message.SMS("FightingOnVehicle!!!");
            //Wait(1000);//2500
        }

        private static void TryingToStealCar(Ped target, Ped player)
        {
            if (IsPlayerTooClose(target, player))
            {
                Begin_SelfDefenceOnFoot(target, player);
                return;
            }

            //EXPERIMENTAL
            if (target.IsInCombat)
            {
                ResetFleeing();
                //return;
            };
            //EXPERIMENTAL

            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                Begin_DriveAway(target, player);
            }
            else
            {
                if (IsClosestVehicleIsCompromised(target, player, MG_Target.Vehicle))
                {
                    Begin_Flee(target, player);
                    MG_Target.ResetCurrentVehicle();
                    return;
                }

            }
            //MG_Message.SMS("FightingOnVehicle!!!");
            // Wait(1000);//2500
            //CurrentAction = TargetCurrentTask.GivingUp;
        }

        private static void FleeingOnFootInPanic(Ped target, Ped player)
        {
            //EXPERIMENTAL
            if (target.IsInCombat)
            {
                ResetFleeing();
                //return;
            };
            //EXPERIMENTAL

            bool isPlayerInVehicle = player.IsInVehicle();
            if (isPlayerInVehicle) return;

            Prop propWeapon = player.Weapons.CurrentWeaponObject;
            if (propWeapon == null) return;

            float distance = World.GetDistance(player.Position, target.Position);
            if (distance > 4) return;

            if (target.IsRagdoll) return;
            if (target.IsGettingUp) return;
            if (target.IsBeingStunned) return;

            SaySurrender();

            target.Task.PlayAnimation("random@mugging3", "handsup_standing_base", 8f, 8f, -1, (AnimationFlags.AllowRotation | AnimationFlags.StayInEndFrame), 0f);
            CurrentAction = TargetCurrentTask.GivingUp;
            IsGivingUp = true;
        }

        private static void FleeingOnVehicle(Ped target, Ped player)
        {
            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                //WIP Проверка целостности машины (возможно и не требуется!)

                //EXPERIMENTAL
                if (target.IsInCombat)
                {
                    ResetFleeing();
                    //return;
                };
                //EXPERIMENTAL

            }
            else
            {
                if (target.IsRagdoll) return;
                if (target.IsGettingUp) return;
                if (target.IsBeingJacked) return;
                if (target.IsBeingStunned) return;

                //---Begin_Flee( target,  player);//<----- ВОЗМОЖНО НЕ НУЖЕН, БУДЕТ И ТАК УБЕГАТЬ!
                CurrentAction = TargetCurrentTask.FleeingOnFoot;
            }
            //MG_Message.SMS("FightingOnVehicle!!!");
        }

        private static void FleeingOnVehicleInPanic(Ped target, Ped player)
        {
            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                //WIP Проверка целостности машины (возможно и не требуется!)

                //EXPERIMENTAL
                if (target.IsInCombat)
                {
                    ResetFleeing();
                    //return;
                };
                //EXPERIMENTAL
            }
            else
            {
                if (target.IsRagdoll) return;
                if (target.IsGettingUp) return;
                if (target.IsBeingJacked) return;
                if (target.IsBeingStunned) return;

                //Begin_PanicFlee(target, player);//<----- ВОЗМОЖНО НЕ НУЖЕН, БУДЕТ И ТАК УБЕГАТЬ!
                CurrentAction = TargetCurrentTask.FleeingOnFootInPanic;
            }
            //MG_Message.SMS("FightingOnVehicle!!!");
        }

        private static void GivingUp(Ped target, Ped player)
        {
            float distance = World.GetDistance(player.Position, target.Position);

            if (target.IsRagdoll || target.IsGettingUp || distance > 16 || target.IsBeingStunned)
            {
                target.Task.ClearAnimation("random@mugging3", "handsup_standing_base");//нужен ли?
                IsGivingUp = false;
                CurrentAction = TargetCurrentTask.FleeingOnFootInPanic;
                return;
            }
        }

        private static void FightingOnFoot(Ped target, Ped player)
        {
            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                Begin_DriveAway(target, player);
                return;
                //CurrentAction = TargetCurrentTask.FightingOnVehicle;
            }
            else
            {
                //WIP Если игрок реально далеко уже:
                //заставить вернуться к машине. А то если далеко от машины, то ему уже пофиг будет на нее
                //если с машиной проблемы, то найти новую

            }
            //MG_Message.SMS("FightingOnFoot!!!");
        }

        private static void FightingOnVehicle(Ped target, Ped player)
        {
            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                //WIP Проверка целостности машины (возможно и не требуется!)
            }
            else
            {
                CurrentAction = TargetCurrentTask.FightingOnFoot;
            }
            //MG_Message.SMS("FightingOnVehicle!!!");

        }

        private static void SelfDefenceOnFoot(Ped target, Ped player)
        {
            if (IsPlayerFarAway(target, player) == false)
            {
                return;
            }

            bool IstargetInVehicle = target.IsInVehicle();
            if (IstargetInVehicle)
            {
                Begin_DriveAway(target, player);

            }
            else
            {
                Begin_Flee(target, player);
            }
            //MG_Message.SMS("FightingOnVehicle!!!");
        }
        #endregion Private Methods Продолжения действий

        #region Private Methods

        private static void SaySurrender()
        {
            string chosenWords;
            switch (MG_Target.Gender)
            {
                case Gender.Male:
                    chosenWords = MG_Random.RandomElement(_sounds_IGiveUp_male);
                    MG_Audio.Play(chosenWords, 0, false);
                    break;
                case Gender.Female:
                    chosenWords = MG_Random.RandomElement(_sounds_IGiveUp_female);
                    MG_Audio.Play(chosenWords, 0, false);
                    break;
                default:
                    break;
            }

        }


        private static bool IsClosestVehicleIsCompromised(Ped target, Ped player, Vehicle vehicle)
        {
            //if (_player.IsSittingInVehicle(_vehicleTarget) || (_vehicleTarget.Driver != null && _vehicleTarget.Driver != _target))
            //if (_vehicle.IsAlive && !_vehicle.IsOnFire && !_vehicle.IsUpsideDown && _vehicle.IsDriveable && _player.CurrentVehicle != _vehicle)//DEL LAST && _vehicle.PassengerCount > 0 && _vehicle.Driver != null
            //{
            float distance = World.GetDistance(vehicle.Position, target.Position);
            float distancePlayer = World.GetDistance(vehicle.Position, player.Position);
            if (distance > distancePlayer)
            {
                return true;
            }
            //}
            return false;
        }

        private static bool IsPlayerTooClose(Ped target, Ped player)
        {
            float distance = World.GetDistance(target.Position, player.Position);
            if (distance < 15)
                return true;
            else
                return false;
        }

        private static bool IsPlayerFarAway(Ped target, Ped player)
        {
            float distance = World.GetDistance(target.Position, player.Position);
            if (distance > 30)
                return true;
            else
                return false;
        }


        private static void ResetFleeing()
        {
            //---MG_Target.Ped.Task.FleeFrom(MG_Player.Ped);
            //---MG_Target.Ped.AlwaysKeepTask = true;

            //CurrentAction = TargetCurrentTask.NotSet;
        }


        //private static void IsDoingNothing(Ped target)
        //{
        //    bool IstargetInVehicle = target.IsInVehicle();
        //    if (IstargetInVehicle)
        //    {


        //    }
        //    else
        //    {

        //    }

        //    //if (target.Task)
        //    //{
        //        //count += 1;
        //        MG_Message.SubTitle("IsInCombatAgainst= " + target.IsInCombatAgainst(MG_Player.PlayerPed) + " IsWalking=" + target.IsWalking + " IsWalking=" + target.IsRunning);
        //        //CurrentAction = TargetCurrentTask.NotSet;//RESET!
        //    //}

        //}
        #endregion Private Methods

    }
}