////////////////////////////////////////////////////////////////////////////////
//
//	MG_Target.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;

public enum TargetMorale { Low, Medium, High };

public enum TargetType { Normal, Assasin, Terrorist, Police, Cartel, Hacker, Military, GangMember };

namespace MG_Liquidator
{
    class MG_Target : Script
    {
        #region Публичные Properties      
        public static Ped Ped { get; set; } = null;
        public static TargetType Type { get; set; }
        public static TargetMorale Morale { get; set; }
        public static TargetReaction Reaction { get; set; }
        public static WeaponTraining WeaponTraining { get; set; }
        //public static Blip Blip { get; set; }
        public static WeaponLevel WeaponLevel { get; set; }
        public static WeaponHash PrimaryWeapon { get; set; }
        public static int Accuracy { get; set; }
        public static ArmorType ArmorType { get; set; }
        public static int Armor { get; set; }
        public static Vehicle Vehicle { get; set; }
        public static Blip VehicleBlip { get; set; }
        public static List<Ped> FriendsInVehicle { get; set; } = new List<Ped>();
        //public static bool IsRevealed { get; set; } = false;

        public static Gender Gender { get; set; }
        #endregion Публичные Properties

        #region Public Methods 

        public static void InitTarget(Ped target)
        {

            Function.Call(Hash.CLEAR_PLAYER_HAS_DAMAGED_AT_LEAST_ONE_PED, MG_Player.Ped);//Для стелса игроку очистить последнего раненого педа.

            Ped = target;
            Gender = target.Gender;
            //Ped.Position = Ped.Position.Normalized;
            //Ped.Position = Ped.IsOccluded;
            //Ped.Position = Ped.Position.Around(1);

            target.IsPersistent = true;
            MG_TargetGroup.InitTargetGroup(target);

            if (target.IsInVehicle())
            {
                MG_GarbageCollector.AddVehicle(target.CurrentVehicle);
                SetCurrentVehicle(target.CurrentVehicle);
                InitFriendsPassengers(target);
            }
            else
            {
                target.Task.WanderAround();
            }

            //CreateBlips();
            //---Function.Call(Hash.SET_PED_AS_ENEMY, target, true);
            //Function.Call(Hash.SET_PED_COMBAT_RANGE, target, (int)CombatRange.Far);
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 46, true);  // force peds to fight
            //Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, target, (int)CombatMovement.Offensive);
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 2 | 46 | 52, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 2 | 46, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            target.DrivingStyle = DrivingStyle.Normal;

            ///Type = TargetType.Terrorist;//DEL!!!!!!!!!!!
            ///Morale = TargetMorale.Low;//DEL!!!!!!!!!!!
            ///MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList.Clear();//DEL!!!!!!!!!!!
            ///MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList.Add(TargetExtraTaskAvaiable.CallBackup);//DEL!!!!!!!!!!!
            Ped.Accuracy = Accuracy;
            Ped.Armor = Armor;
            if (Morale.Equals(TargetMorale.High))
            {
                Ped.Weapons.Give(GetWeaponModel(), 50, true, true);
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, target, (int)CombatMovement.Offensive);
                Function.Call(Hash.SET_PED_COMBAT_RANGE, target, (int)CombatRange.Far);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 2 | 46 | 52, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,

            }
            else if(Morale.Equals(TargetMorale.Medium))
            {
                Ped.Weapons.Give(GetWeaponModel(), 50, true, true);
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, target, (int)CombatMovement.Defensive);
                Function.Call(Hash.SET_PED_COMBAT_RANGE, target, (int)CombatRange.Near);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 2 | 52, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
            }
            else//Morale.Equals(TargetMorale.Low)
            {
                Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, target, (int)CombatMovement.Disabled);
                Function.Call(Hash.SET_PED_COMBAT_RANGE, target, (int)CombatRange.Disabled);
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 52, true);//BF_CanDoDrivebys = 2 TRUE; 17 DONT SHOOT! 46 FIGHT TO DEATH | IgnoreTrafficWhenDriving = 52,
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, target, 5, false);//BF_CanFightArmedPedsWhenNotArmed = 5,  
                // 
            }
            //--------------------------------------------MG_Watchers.Watchers_AmountCanBeSpawn = Convert.ToInt32(Math.Round((MG_Statistic.TotalTargetsEliminated * 1.0f), MidpointRounding.AwayFromZero));
            //--------------------------------------------MG_HiredRandomPeople.HiredPeople_AmountCanBeSpawn = Convert.ToInt32(Math.Round((MG_Statistic.TotalTargetsEliminated * 1.0f), MidpointRounding.AwayFromZero));
            //--------------------------------------------MG_TargetBodyGuards.BodyGuards_AmountCanBeSpawn = Convert.ToInt32(Math.Round((MG_Statistic.TotalTargetsEliminated * 1.0f), MidpointRounding.AwayFromZero));
        }

        public static void SetCurrentVehicle(Vehicle vehicle)
        {
            ResetCurrentVehicle();
            vehicle.IsPersistent = true;
            Vehicle = vehicle;
            //VehicleBlip = MG_Blip.CreateBlip(Vehicle, false, BlipSprite.GetawayCar, BlipColor.Pink);
        }

        public static void ResetCurrentVehicle()
        {
            if (VehicleBlip != null)
            {
                VehicleBlip.Remove();
            }

            if (Vehicle != null)
            {
                Vehicle.IsPersistent = false;
                //Vehicle.MarkAsNoLongerNeeded();
            }       
        }

        public static void Reset()
        {
            if (Ped != null)
            {             
                Ped.AlwaysKeepTask = false;
                Ped.IsPersistent = false;
                //Ped.MarkAsNoLongerNeeded();
                Ped = null;

            }

            ResetCurrentVehicle();

            if (FriendsInVehicle.Any())
            {
                foreach (var ped in FriendsInVehicle)
                {
                    //ped.CurrentBlip.Remove();
                    //ped.IsPersistent = false;
                    MG_ForgottenEnemy.AddPed(ped);
                }
                FriendsInVehicle.Clear();
            }
         

            //if (FriendsInVehicle.Count > 0)
            //{
            //    foreach (var friend in FriendsInVehicle)
            //    {
            //        if (friend.CurrentBlip != null)
            //        {
            //            friend.CurrentBlip.Remove();
            //        }
            //        friend.IsPersistent = false;
            //        //friend.MarkAsNoLongerNeeded();

            //    }
            //}

            //IsRevealed = false;

        }

        public static void CreateBlips()
        {
            if (Vehicle != null)
            {
                if (Vehicle.IsAlive)
                {
                    Blip blipCar = MG_Blip.CreateBlip(Vehicle, false, BlipSprite.GetawayCar, BlipColor.Red);
                    MG_GarbageCollector.vehicleBlips.Add(blipCar);
                }
                    

            }

            Blip blip;
            if (FriendsInVehicle.Any())
            {
                foreach (var ped in FriendsInVehicle)
                {
                    if (ped.IsDead) continue;
                    blip = ped.AddBlip();
                    blip.Color = BlipColor.Orange;
                }
            }

            if (Ped.IsAlive)
            {
                blip = Ped.AddBlip();
                blip.IsFriendly = false;
            }

        }



        #endregion Public Methods

        #region Private Methods    

        private static void InitFriendsPassengers(Ped target)
        {
            Ped[] peds = target.CurrentVehicle.Occupants;
            FriendsInVehicle = new List<Ped>();
            foreach (var ped in peds)
            {
                if (ped.Equals(target)) continue;
                ped.IsPersistent = true;
                //MG_Group.SetGroup(ped, target, MG_TargetGroup.RelationsGroup, MG_TargetGroup._groupID);
                //
                MG_TargetGroup.PedGroup.Add(ped, false);///EXPERIMENTAL НА ВСЯКИЙ СЛУЧАЙ, ЧТОБЫ СВОИХ НЕ УБИВАЛ

                FriendsInVehicle.Add(ped);

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


            }

            if (Type.Equals(TargetType.Terrorist))
            {
                foreach (var ped in peds)
                {
                    MG_Bombermania.MakeSuicideBomber(ped);
                }
            }
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
        #endregion Private Methods
    }
}