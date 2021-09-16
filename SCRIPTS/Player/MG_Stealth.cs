////////////////////////////////////////////////////////////////////////////////
//
//	MG_Stealth.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;
using System;

namespace MG_Liquidator
{
    public static class MG_Stealth
    {
        public static bool IsYourFace_is_known { get; set; } = false;

        #region Public Methods     

        public static void CheckIfPlayerCompromised(Ped target)
        {
            Ped player = MG_Player.Ped;

            //if (MG_Target.Type.Equals(TargetType.Hacker))
            //{
            //    if (World.GetDistance(player.Position, target.Position) < 30f)
            //    {
            //        MG_TargetAI_ExtraActions.ShockPlayerPhone(target, player, false);
            //        SetCompromised(target);
            //        return;
            //    }
            //}
            float distance = World.GetDistance(player.Position, target.Position);
            bool isTargetAffectedByRagdol = target.IsRagdoll && distance < 3f; //
            bool isTargetAffectedBySurrounding = target.IsBeingStunned || target.IsInjured || target.IsInMeleeCombat || target.IsInCombat || target.IsFleeing && distance < 10f;
            bool isCrashingIntoTargetCar = player.CurrentVehicle != null && target.CurrentVehicle != null && player.CurrentVehicle.IsTouching(target.CurrentVehicle);
            bool isDamagedByPlayer = target.HasBeenDamagedBy(MG_Player.Ped);
            bool isPlayerWanted = MG_Player.Player.WantedLevel > 0 && !Function.Call<bool>(Hash.ARE_PLAYER_STARS_GREYED_OUT, MG_Player.Player) && !Function.Call<bool>(Hash.ARE_PLAYER_FLASHING_STARS_ABOUT_TO_DROP, MG_Player.Player) && distance < 20f;
            bool isPlayerRecognized = PlayerCanBeRecognized(MG_Settings.INI_CriticalDistanceToBeSpotted, target);

            string compromisedReason = "";
            if (isTargetAffectedByRagdol) compromisedReason = "affected by ragdol!";
            if (isTargetAffectedBySurrounding) compromisedReason = "Panic."; //Recognized!//"Target affected by surrounding ";
            if (isCrashingIntoTargetCar) compromisedReason = "Player aggression.";// "Crashing into car.";
            if (isDamagedByPlayer) compromisedReason = "Player aggression.";
            if (isPlayerWanted) compromisedReason = "Police chase.";
            if (isPlayerRecognized) compromisedReason = "Visible Weapon.";
            //compromisedReason = "!TEST!";//DEL!!!!!!!!!!!!!!!!!!!!!!
            //isPlayerRecognized = true;//DEL!!!!!!!!!!!!!!!!!!!!!!

            if (compromisedReason.Length > 1) MG_Message.HelpMessage("You've been spotted! Reason: " + compromisedReason);

            if (isTargetAffectedBySurrounding || isCrashingIntoTargetCar || isDamagedByPlayer || isPlayerWanted || isTargetAffectedByRagdol || isPlayerRecognized)//isTargetAffectedBySurrounding ||
            {
                SetCompromised(target);
                return;
            }
        }

        public static bool CheckIfPlayerCompromisedByWatchers(Ped target)
        {
            Ped player = MG_Player.Ped;
            float distance = World.GetDistance(player.Position, target.Position);
            //bool isTargetAffectedByRagdol = target.IsRagdoll && distance < 5f; //
            //bool isTargetAffectedBySurrounding = target.IsBeingStunned || target.IsInjured || target.IsInMeleeCombat || target.IsInCombat || target.IsFleeing && distance < 10f; ; //
            bool isCrashingIntoTargetCar = player.CurrentVehicle != null && target.CurrentVehicle != null && player.CurrentVehicle.IsTouching(target.CurrentVehicle);
            bool isDamagedByPlayer = target.HasBeenDamagedBy(MG_Player.Ped);
            bool isPlayerWanted = MG_Player.Player.WantedLevel > 0 && !Function.Call<bool>(Hash.ARE_PLAYER_STARS_GREYED_OUT, MG_Player.Player) && !Function.Call<bool>(Hash.ARE_PLAYER_FLASHING_STARS_ABOUT_TO_DROP, MG_Player.Player);
            //bool isPlayerRecognized = PlayerCanBeRecognized(MG_Settings.INI_CriticalDistanceToBeSpotted, target);

            //string compromisedReason = "";
            //if (isCrashingIntoTargetCar) compromisedReason = "Crashing into car!";
            //if (isDamagedByPlayer) compromisedReason = "Damaged by player!";
            //if (isPlayerWanted) compromisedReason = "Police chase!";


            //if (compromisedReason.Length > 1) MG_Message.HelpMessage("You've been spotted by Watch dogs! Reason: " + compromisedReason);

            if (isCrashingIntoTargetCar || isDamagedByPlayer || isPlayerWanted)//isTargetAffectedBySurrounding || isTargetAffectedByRagdol   || isPlayerRecognized
            {

                return true;
            }
            else
            {
                return false;
            }
        }

        public static void ShowDistanceBetweenTwoTargets(Ped ped, Ped target)
        {
            float distance = Vector2.Distance(ped.Position, target.Position);
            int _roundedDistance = Convert.ToInt32(distance);
            MG_Message.SubTitle("~w~Distance:~r~" + _roundedDistance + "m~w~.", 1000);
            // Wait(1000);
        }

        #endregion Public Methods

        #region Private Methods

        private static bool PlayerCanBeRecognized(float criticalDistance, Ped target)
        {
            Ped player = MG_Player.Ped;
            float PlayerSafeDistance = CalculateSafeDistance(target);
            //MG_Message.SubTitle(PlayerSafeDistance + " < criticalDistance=" + criticalDistance);
            if (PlayerSafeDistance < criticalDistance)
            {
                if (Function.Call<bool>(GTA.Native.Hash.HAS_ENTITY_CLEAR_LOS_TO_ENTITY_IN_FRONT, target, player))
                {
                    return true;
                }
            }
            return false;
        }

        private static float CalculateSafeDistance(Ped target)
        {
            Ped player = MG_Player.Ped;
            float playerSafeDistance = World.GetDistance(player.Position, target.Position);
            Prop propWeapon = MG_Player.Ped.Weapons.CurrentWeaponObject;
            if (GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_ANY_VEHICLE, player))
            {
                //_PlayerSafeDistance = _PlayerSafeDistance + MG_Assasin_Main.INI_MinusDistance_CarGivesAdvantages;//Если в машине игрок, то цель не должна распознать игрока на обычном расстоянии
            }
            else if (propWeapon != null)//если вдруг у игрока есть оружие в руках и он не в машине, то это слишком заметно
            {
                playerSafeDistance = playerSafeDistance - MG_Settings.INI_increaseCritDistance_WeaponInHands;

                if (MG_Player.Player.IsAiming)
                {
                    if (target.IsBeingStunned || target.IsInjured || target.IsInMeleeCombat || target.IsInCombat || target.IsFleeing)
                    {
                        //return (MG_Settings.INI_CriticalDistanceToBeSpotted - 1);
                        return (-1);
                    }

                    playerSafeDistance = playerSafeDistance - MG_Settings.INI_increaseCritDistance_AIMING;

                    if (player.IsAimingFromCover)
                    {
                        playerSafeDistance = playerSafeDistance + MG_Settings.INI_decreaseCritDistance_InVCover_AIMING;
                    }
                }
                else if (player.IsInCover())
                {
                    playerSafeDistance = playerSafeDistance + MG_Settings.INI_decreaseCritDistance_InVCover;
                }


            }

            //if (MG_Player.Ped.IsRunning)
            //{
            //    playerSafeDistance = playerSafeDistance - MG_Settings.INI_increaseCritDistance_Running;
            //}

            //if (MG_Player.Ped.IsSprinting)
            //{
            //    playerSafeDistance = playerSafeDistance - MG_Settings.INI_increaseCritDistance_Sprinting;
            //}

            //if (MG_Settings.INI_CanBeRecognizeByFace)
            //{
            //    if (IsYourFace_is_known)
            //    {
            //        playerSafeDistance = playerSafeDistance - MG_Settings.INI_CriticalDistanceToBeSpotted_KnownFace;
            //    }
            //}

            if (playerSafeDistance > MG_Settings.INI_MaxCriticalDistance)
            {
                playerSafeDistance = MG_Settings.INI_MaxCriticalDistance;
            }

            //if (MG_Assasin_Main.PlayerPed.CurrentVehicle != null && _target.CurrentVehicle != null)
            //{
            //    if (MG_Assasin_Main.PlayerPed.CurrentVehicle.IsTouching(_target.CurrentVehicle))
            //    {
            //        MG_Assasin_Main.isTargetCompromised = true;
            //        return;
            //    }
            //    _PlayerSafeDistance = _PlayerSafeDistance - 15f;
            //}

            else if (Function.Call<bool>(Hash.HAS_PLAYER_DAMAGED_AT_LEAST_ONE_PED, MG_Player.Ped))
            {
                playerSafeDistance = playerSafeDistance - MG_Settings.INI_increaseCritDistance_DammagedRandomPed;
                Function.Call(Hash.CLEAR_PLAYER_HAS_DAMAGED_AT_LEAST_ONE_PED, MG_Player.Ped);
            }

            return playerSafeDistance;
        }

        private static void SetCompromised(Ped target)
        {
            //_target.Task.ClearAllImmediately();//NEW 31.01.2020
            MG_TargetAI.IsCompromised = true;
            TargetMorale morale = MG_Target.Morale;

            //----Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 2, false);//BF_CanDoDrivebys = 2 FALSE; 17 DONT SHOOT! 46 FIGHT TO DEATH
            //Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, _guard, true);
            switch (morale)
            {
                case TargetMorale.Low:
                    Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, target, true);
                    Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, target, 0, true);
                    break;
                case TargetMorale.Medium:
                    Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, target, true);
                    break;
                case TargetMorale.High:
                    break;
                default:
                    break;
            }
            target.DrivingStyle = DrivingStyle.AvoidTrafficExtremely;
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_TargetGroup.RelationsGroup, MG_Player.Ped.RelationshipGroup);//5 - HATE
        }
        #endregion Private Methods

    }
}
