////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetAI_ExtraActions.cs
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
    public enum TargetReaction { Low, Medium, High, VeryHigh };
    public enum TargetExtraTaskAvaiable { CallPolice, CallBackup, ShockPlayerPhone, DisablePlayerVehicle, CarBomb };


    class MG_TargetAI_ExtraActions : Script
    {

        #region Fields
        private static string _sound_911 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Part2\911.mp3";
        #endregion Fields

        #region Properties       
        public static List<TargetExtraTaskAvaiable> TargetExtraTaskAvaiableList { get; set; } = new List<TargetExtraTaskAvaiable>();
        public static TargetExtraTaskAvaiable ChosenTask { get; set; }// = TargetExtraTaskAvaiable.None;
        public static int CoolDown { get; set; } = 5000;
        #endregion Properties

        #region Constructor

        public MG_TargetAI_ExtraActions()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (MG_TargetFinder.IsTargetFound)
            {
                if (MG_TargetAI.IsCompromised)
                {
                    ExtraActAgainstPlayer();
                }
            }
        }
        #endregion OnTick

        #region Public Methods
  
        public static void ExtraActAgainstPlayer()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;
            //Ped player = MG_Player.PlayerPed;

            if (target.IsDead)
            {
                Wait(3000);
                return;
            }

            if (TargetExtraTaskAvaiableList.Any() == false)
            {
                Wait(5000);
                return;
            }

            Wait(CoolDown);
            if (target == null) return;
            if (target.IsDead) return;
            if (player.IsDead) return;
            if (MG_AssassinationMission.IsJobActive == false) return;

            ChosenTask = ChooseRandomExtraTask();
            switch (ChosenTask)
            {
                // case TargetExtraTaskAvaiable.None:
                //До сюда никогда не доберется
                //  break;
                case TargetExtraTaskAvaiable.CallPolice:
                    CallPolice();
                    break;
                case TargetExtraTaskAvaiable.CallBackup:
                    CallBackup();
                    break;
                case TargetExtraTaskAvaiable.ShockPlayerPhone:
                    ShockPlayerPhone();
                    break;
                case TargetExtraTaskAvaiable.DisablePlayerVehicle:
                    DisablePlayerVehicle();
                    break;
                case TargetExtraTaskAvaiable.CarBomb:
                    CarBomb();
                    break;
                default:
                    CallPolice();
                    break;
            }
        }

        public static void Reset()
        {
            TargetExtraTaskAvaiableList.Clear();
        }
        #endregion Public Methods

        #region Private Methods Действия

        public static void CallPolice()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;


            while (IsAbleToUseExtraTask() == false) Wait(1000);
            if (target.IsDead) return;
            if (player.IsDead) return;
            MG_Ped.PlayCellphoneAnim(target);
            //MG_Ped.HideWeapon(target);
            var propPhone = MG_Ped.CreateAndAttachPhone(target);

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                MG_Ped.BreakPhoneCall(target, propPhone);
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER
            Wait(1500);
            MG_Audio.Play(_sound_911, 1, false);
            GenerateWarningMessage("WARNING: THE TARGET IS CALLING THE POLICE!", 7);


            while (IsAbleToUseExtraTask() == false) Wait(1000);
            
            MG_Ped.BreakPhoneCall(target, propPhone);

            if (player.IsDead) return;
            if (target.IsDead) return;

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            MG_Player.UpWantedLevel();

            TargetExtraTaskAvaiableList.Remove(ChosenTask);
            // ChosenTask = TargetExtraTaskAvaiable.None;
        }

        public static void CallBackup()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;


            while (IsAbleToUseExtraTask() == false) Wait(1000);
            if (target.IsDead) return;
            if (player.IsDead) return;
            MG_Ped.PlayCellphoneAnim(target);
            //MG_Ped.HideWeapon(target);
            var propPhone = MG_Ped.CreateAndAttachPhone(target);

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                MG_Ped.BreakPhoneCall(target, propPhone);
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            GenerateWarningMessage("WARNING: THE TARGET IS CALLING BACKUP!", 7);

            while (IsAbleToUseExtraTask() == false) Wait(1000);
           
            MG_Ped.BreakPhoneCall(target, propPhone);

            if (player.IsDead) return;
            if (target.IsDead) return;

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            MG_BackupForce.CreateBackup();

            TargetExtraTaskAvaiableList.Remove(ChosenTask);
            //  ChosenTask = TargetExtraTaskAvaiable.None;           
        }

        public static void ShockPlayerPhone()
        {

            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;

            while (IsAbleToUseExtraTask() == false) Wait(1000);
            if (target.IsDead) return;
            if (player.IsDead) return;

            MG_Ped.PlayCellphoneAnim(target);
            //MG_Ped.HideWeapon(target);
            var propPhone = MG_Ped.CreateAndAttachPhone(target);

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                MG_Ped.BreakPhoneCall(target, propPhone);
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            GenerateWarningMessage("WARNING: THE TARGET IS HACKING YOUR PHONE!", 7);
            while (IsAbleToUseExtraTask() == false) Wait(1000);

            MG_Ped.BreakPhoneCall(target, propPhone);
            if (player.IsDead) return;
            if (target.IsDead) return;

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            while (player.IsInVehicle() && player.IsAlive) Wait(1500);

            World.ShootBullet(player.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 0.1f), player.GetBoneCoord(Bone.SKEL_Head), target, WeaponHash.StunGun, 0);

            if (target.IsDead) return;
            TargetExtraTaskAvaiableList.Remove(ChosenTask);
            // ChosenTask = TargetExtraTaskAvaiable.None;
        }

        public static void DisablePlayerVehicle()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;

            while (!player.IsInVehicle() && player.IsAlive) Wait(1500);
            Vehicle vehicle = player.CurrentVehicle;
            while (IsAbleToUseExtraTask() == false) Wait(1000);
            if (target.IsDead) return;
            if (player.IsDead) return;

            MG_Ped.PlayCellphoneAnim(target);
            //MG_Ped.HideWeapon(target);
            var propPhone = MG_Ped.CreateAndAttachPhone(target);

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                MG_Ped.BreakPhoneCall(target, propPhone);
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            GenerateWarningMessage("WARNING: THE TARGET IS HACKING YOUR ELECTRONIC DEVICES IN YOUR CAR!", 7);
            while (IsAbleToUseExtraTask() == false) Wait(1000);
           
            MG_Ped.BreakPhoneCall(target, propPhone);
            if (player.IsDead) return;
            if (target.IsDead) return;

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            if (vehicle != null)
            {
                Function.Call(Hash.SET_VEHICLE_ENGINE_HEALTH, vehicle, -4000);
                //Function.Call(Hash.SET_VEHICLE_ENGINE_HEALTH, vehicle, 650);
                Function.Call(Hash.SET_VEHICLE_ENGINE_ON, vehicle, false, true, false);
                Function.Call(Hash.SET_VEHICLE_PETROL_TANK_HEALTH, vehicle, 0);
                Function.Call(Hash.SET_VEHICLE_HANDBRAKE, vehicle, true);
            }

            if (target.IsDead) return;
            TargetExtraTaskAvaiableList.Remove(ChosenTask);
            // ChosenTask = TargetExtraTaskAvaiable.None;
        }

        public static void CarBomb()
        {
            MG_Message.SubTitle("A 1");
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;

            //while (!player.IsInVehicle() && player.IsAlive) Wait(1500);
            while (IsAbleToUseExtraTask() == false) Wait(1000);
            if (target.IsDead) return;
            if (player.IsDead) return;
            MG_Message.SubTitle("A 2");
            MG_Ped.PlayCellphoneAnim(target);
            //MG_Ped.HideWeapon(target);
            var propPhone = MG_Ped.CreateAndAttachPhone(target);

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                MG_Ped.BreakPhoneCall(target, propPhone);
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            GenerateWarningMessage("WARNING: THE TARGET IS PREPARING TO REMOTLY DETONATE HIDDEN EXPLOSIVES!", 7);
            while (IsAbleToUseExtraTask() == false) Wait(1000);
               
            MG_Ped.BreakPhoneCall(target, propPhone);
            if (player.IsDead) return;
            if (target.IsDead) return;

            //IF SURRENDER
            if (MG_TargetAI.IsGivingUp)
            {
                TargetExtraTaskAvaiableList.Clear();
                return;
            }
            //IF SURRENDER

            MG_Bombermania.DetonateRandomCars();

            if (target.IsDead) return;
            TargetExtraTaskAvaiableList.Remove(ChosenTask);
            // ChosenTask = TargetExtraTaskAvaiable.None;

            TargetExtraTaskAvaiableList.Remove(ChosenTask);
        }
        #endregion Private Methods Действия

        #region Private Methods
   
        private static TargetExtraTaskAvaiable ChooseRandomExtraTask()
        {
            return MG_Random.RandomElement(TargetExtraTaskAvaiableList);
        }

        private static bool IsAbleToUseExtraTask()
        {
            Ped target = MG_Target.Ped;
            if (target == null) return true;
            if (target.IsDead) return true;

            if (
                 //target.IsBeingStunned
                 //|| target.IsInMeleeCombat
                 target.IsRagdoll
                //|| MG_TargetAI.CurrentAction.Equals(TargetCurrentTask.GivingUp)
                )
            {
                return false;
            }
            return true;
        }

        private static void GenerateWarningMessage(string text, int durationInSeconds)
        {
            Ped target = MG_Target.Ped;
            for (int i = 0; i < durationInSeconds; i++)
            {
                if (target.IsDead) break;
                if (MG_TargetAI.IsGivingUp) break;              
                MG_Message.SubTitle("~w~" + text);
                Wait(250);

                if (target.IsDead) break;
                if (MG_TargetAI.IsGivingUp) break;
                MG_Message.SubTitle("~r~" + text);
                Wait(250);

                if (target.IsDead) break;
                if (MG_TargetAI.IsGivingUp) break;
                MG_Message.SubTitle("~w~" + text);
                Wait(250);

                if (target.IsDead) break;
                if (MG_TargetAI.IsGivingUp) break;
                MG_Message.SubTitle("~r~" + text);
                Wait(250);
            }

        }

        #endregion Private Methods


    }
}


