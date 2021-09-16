////////////////////////////////////////////////////////////////////////////////
//
//	MG_MissionPreset.cs
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
    public static class MG_MissionPreset
    {
        //private static List<TargetType> bannedTargetType;


        #region Public Methods

        public static void GeneratePreset()
        {
            MG_TargetFinder.HasToBeInVehicle = MG_Random.RandomB();
            //MG_TargetFinder.HasToBeInVehicle = true;///DEL!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            MG_Target.Type = MG_DifficultyRaiser.GenerateTargetType();
            //MG_Target.Type = TargetType.GangMember;///DEL!!!!!!!!!!!!!!!!!!!!!!!!!

            MG_Target.Morale = GetRandomTargetMorale();
            //MG_Target.Morale = TargetMorale.Medium;///DEL!!!!!!!!!!!!!!!!!!!!!!

            MG_Target.WeaponTraining = GenerateWeaponTraining();

            MG_Target.Accuracy = GenerateAccuracy();
            MG_Target.ArmorType = GenerateArmorType();
            MG_Target.Armor = GenerateArmor();
            //-------------MG_Target.WeaponLevel = GenerateWeaponLevel();
            //-------------if (MG_Target.WeaponLevel.Equals(WeaponLevel.None) == false) MG_Target.PrimaryWeapon = GenerateWeapon();

            MG_Target.Reaction = GenerateReaction();
            MG_TargetAI_ExtraActions.CoolDown = (GenerateCoolDown() * 1000);
            //MG_TargetAI_ExtraActions.CoolDown = 2000;///DEL!!!!!!!!!!!!!!!!!!!!!

            //MG_Message.SubTitle("MG_Target.Reaction=" + MG_Target.Reaction +" MG_TargetAI_ExtraActions.CoolDown=" + MG_TargetAI_ExtraActions.CoolDown,10000);

            MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList = GenerateExtraAction();
            //MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList = new List<TargetExtraTaskAvaiable>() { TargetExtraTaskAvaiable.CallPolice };///DEL!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            //MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList = new List<TargetExtraTaskAvaiable>() { };///DEL!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            MG_TargetBodyGuards.AmountCanBeSpawn = MG_DifficultyRaiser.GenerateNumberBodyguards();
            //MG_TargetBodyGuards.AmountCanBeSpawn = 15;///DEL!!!!!!!!!!!!!!!!!!!!!

            MG_Watchers.AmountSquadsCanBeSpawn = MG_DifficultyRaiser.GenerateNumberWatcherSquads();
            //MG_Watchers.AmountSquadsCanBeSpawn = 0;///DEL!!!!!!!!!!!!!!!!!!!!!

            MG_BackupForce.AmountSquadsCanBeSpawn = MG_DifficultyRaiser.GenerateNumberBackupForceSquad();
            //MG_BackupForce.AmountSquadsCanBeSpawn = 1;///DEL!!!!!!!!!!!!!!!!!!!!!

            MG_AssassinationMission.DestinationSetting = GenerateDestinationSetting();
        }


        #endregion Public Methods

        #region Private Methods

        private static TargetMorale GetRandomTargetMorale()
        {
            List<TargetMorale> targetBehaviours = Enum.GetValues(typeof(TargetMorale)).Cast<TargetMorale>().ToList();

            TargetType targetType = MG_Target.Type;
            switch (targetType)
            {
                case TargetType.Normal:
                    break;
                case TargetType.Assasin:
                    targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.Terrorist:
                    targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.Police:
                    targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.Cartel:
                    targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.Hacker:
                    //targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.Military:
                    targetBehaviours.Remove(TargetMorale.Low);
                    break;
                case TargetType.GangMember:
                    break;
                default:
                    break;
            }
            TargetMorale chosen = targetBehaviours[MG_Random.Random(targetBehaviours.Count)];

            return chosen;
        }

        private static WeaponTraining GenerateWeaponTraining()
        {
            WeaponTraining chosen = WeaponTraining.None;
            //List<WeaponTraining> targetWeaponTrainings = new List<WeaponTraining>()
            //{
            //    WeaponTraining.None,
            //    WeaponTraining.Selfdefence,
            //    WeaponTraining.Military,
            //    WeaponTraining.Veteran,
            //    WeaponTraining.Elite
            //};
            List<WeaponTraining> targetWeaponTrainings = Enum.GetValues(typeof(WeaponTraining)).Cast<WeaponTraining>().ToList();

            TargetType targetType = MG_Target.Type;
            switch (targetType)
            {
                case TargetType.Normal:

                    break;
                case TargetType.Assasin:
                    targetWeaponTrainings.Remove(WeaponTraining.None);
                    targetWeaponTrainings.Remove(WeaponTraining.Selfdefence);
                    break;
                case TargetType.Terrorist:
                    break;
                case TargetType.Police:
                    targetWeaponTrainings.Remove(WeaponTraining.None);
                    targetWeaponTrainings.Remove(WeaponTraining.Selfdefence);
                    break;
                case TargetType.Cartel:
                    targetWeaponTrainings.Remove(WeaponTraining.None);
                    break;
                case TargetType.Hacker:
                    break;
                case TargetType.Military:
                    targetWeaponTrainings.Remove(WeaponTraining.None);
                    targetWeaponTrainings.Remove(WeaponTraining.Selfdefence);
                    break;
                case TargetType.GangMember:
                    targetWeaponTrainings.Remove(WeaponTraining.None);
                    break;
                default:
                    break;
            }


            if (MG_Target.Morale.Equals(TargetMorale.Low))
            {
                targetWeaponTrainings.Remove(WeaponTraining.Elite);
                targetWeaponTrainings.Remove(WeaponTraining.Veteran);
                targetWeaponTrainings.Remove(WeaponTraining.Military);
            }

            if (targetWeaponTrainings.Count == 0)
            {
                return WeaponTraining.Selfdefence;
            }

            chosen = targetWeaponTrainings[MG_Random.Random(targetWeaponTrainings.Count)];


            return chosen;
        }

        private static int GenerateAccuracy()
        {
            int accuracy;
            WeaponTraining targetWeaponTraining = MG_Target.WeaponTraining;

            switch (targetWeaponTraining)
            {
                case WeaponTraining.None:
                    accuracy = MG_Random.Random(0, 15);
                    break;
                case WeaponTraining.Selfdefence:
                    accuracy = MG_Random.Random(15, 30);
                    break;
                case WeaponTraining.Military:
                    accuracy = MG_Random.Random(30, 50);
                    break;
                case WeaponTraining.Veteran:
                    accuracy = MG_Random.Random(50, 75);
                    break;
                case WeaponTraining.Elite:
                    accuracy = MG_Random.Random(75, 100);
                    break;
                default:
                    accuracy = MG_Random.Random(0, 100);
                    break;
            }
            return accuracy;
        }

        private static ArmorType GenerateArmorType()
        {
            //int armor = 0;
            //int chance = MG_Random.Random(10);
            //List<PedArmorType> pedArmorTypes = Enum.GetValues(typeof(PedArmorType)).Cast<PedArmorType>().ToList();
            List<ArmorType> pedArmorTypesChance = new List<ArmorType>();

            TargetType targetType = MG_Target.Type;
            switch (targetType)
            {
                case TargetType.Normal:
                    AddManyTimes(ArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Assasin:
                    AddManyTimes(ArmorType.None, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Terrorist:
                    AddManyTimes(ArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Police:
                    //MG_ListFunc<PedArmorType>.AddManyTimes(PedArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Cartel:
                    //MG_ListFunc<PedArmorType>.AddManyTimes(PedArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Hacker:
                    AddManyTimes(ArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                case TargetType.Military:
                    AddManyTimes(ArmorType.None, 10, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 25, pedArmorTypesChance);
                    break;
                case TargetType.GangMember:
                    AddManyTimes(ArmorType.None, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 10, pedArmorTypesChance);
                    //AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    //AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
                default:
                    AddManyTimes(ArmorType.None, 100, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Light, 50, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Medium, 25, pedArmorTypesChance);
                    AddManyTimes(ArmorType.Heavy, 5, pedArmorTypesChance);
                    break;
            }
            ArmorType chosen = pedArmorTypesChance[MG_Random.Random(pedArmorTypesChance.Count)];

            return chosen;
        }

        private static int GenerateArmor()
        {
            //None - 0
            //Light - 50
            //Medium - 100
            //Heavy - 200
            int armor;
            ArmorType armorType = MG_Target.ArmorType;

            switch (armorType)
            {
                case ArmorType.None:
                    armor = 0;
                    break;
                case ArmorType.Light:
                    armor = 50;
                    break;
                case ArmorType.Medium:
                    armor = 100;
                    break;
                case ArmorType.Heavy:
                    armor = 200;
                    break;
                default:
                    armor = MG_Random.Random(0, 200);
                    break;
            }
            return armor;
        }

        private static WeaponLevel GenerateWeaponLevel()
        {
            WeaponLevel chosen;
            List<WeaponLevel> weaponLevels;

            TargetType targetType = MG_Target.Type;
            switch (targetType)
            {
                case TargetType.Normal:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Melee_1, WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4 };
                    break;
                case TargetType.Assasin:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4, WeaponLevel.SniperRifle_5 };
                    break;
                case TargetType.Terrorist:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Melee_1, WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4, WeaponLevel.SniperRifle_5, WeaponLevel.HeavyWeapon_6 };
                    break;
                case TargetType.Police:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4 };
                    break;
                case TargetType.Cartel:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4, WeaponLevel.SniperRifle_5, WeaponLevel.HeavyWeapon_6 };
                    break;
                case TargetType.Hacker:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Melee_1, WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3 };
                    break;
                case TargetType.Military:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.AssaultRifle_4, WeaponLevel.SniperRifle_5, WeaponLevel.HeavyWeapon_6 };
                    break;
                case TargetType.GangMember:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Melee_1, WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4 };
                    break;
                default:
                    weaponLevels = new List<WeaponLevel>() { WeaponLevel.Melee_1, WeaponLevel.Handgun_2, WeaponLevel.SMG_and_Shotguns_3, WeaponLevel.AssaultRifle_4, WeaponLevel.SniperRifle_5, WeaponLevel.HeavyWeapon_6 };
                    break;
            }

            chosen = weaponLevels[MG_Random.Random(weaponLevels.Count)];
            return chosen;
        }

        private static WeaponHash GenerateWeapon()
        {
            WeaponHash chosen;
            WeaponLevel weaponLevel = MG_Target.WeaponLevel;
            WeaponHash[] weaponArray;
            switch (weaponLevel)
            {
                //case WeaponLevel.None:
                //weaponArray = DB_Weapons.AllWeapons;
                //break;
                case WeaponLevel.Melee_1:
                    weaponArray = DB_Weapons.MeleeWeapons;
                    break;
                case WeaponLevel.Handgun_2:
                    weaponArray = DB_Weapons.Handguns;
                    break;
                case WeaponLevel.SMG_and_Shotguns_3:
                    weaponArray = DB_Weapons.Shotguns;
                    break;
                case WeaponLevel.AssaultRifle_4:
                    weaponArray = DB_Weapons.AssaultRifles;
                    break;
                case WeaponLevel.SniperRifle_5:
                    weaponArray = DB_Weapons.SniperRifles;
                    break;
                case WeaponLevel.HeavyWeapon_6:
                    weaponArray = DB_Weapons.HeavyWeapons;
                    break;
                default:
                    weaponArray = DB_Weapons.AllWeapons;
                    break;
            }
            chosen = weaponArray[MG_Random.Random(weaponArray.Length)];
            return chosen;
        }

        private static List<TargetExtraTaskAvaiable> GenerateExtraAction()
        {
            List<TargetExtraTaskAvaiable> list = new List<TargetExtraTaskAvaiable>();

            TargetType targetType = MG_Target.Type;
            switch (targetType)
            {
                case TargetType.Normal:
                    list.Add(TargetExtraTaskAvaiable.CallPolice);
                    break;
                case TargetType.Assasin:
                    list.Add(TargetExtraTaskAvaiable.CallBackup);
                    break;
                case TargetType.Terrorist:
                    break;
                case TargetType.Police:
                    list.Add(TargetExtraTaskAvaiable.CallPolice);
                    break;
                case TargetType.Cartel:
                    list.Add(TargetExtraTaskAvaiable.CallBackup);
                    break;
                case TargetType.Hacker:
                    list.Add(TargetExtraTaskAvaiable.DisablePlayerVehicle);
                    list.Add(TargetExtraTaskAvaiable.ShockPlayerPhone);
                    break;
                case TargetType.Military:
                    list.Add(TargetExtraTaskAvaiable.CallBackup);
                    list.Add(TargetExtraTaskAvaiable.CallPolice);
                    break;
                case TargetType.GangMember:
                    list.Add(TargetExtraTaskAvaiable.CallBackup);
                    break;
                default:
                    break;
            }

            return list;
        }

        private static DestinationSetting GenerateDestinationSetting()
        {
            if (MG_Settings.INI_DisableOutsideMissions)
            {
                return DestinationSetting.InsideCIty;
            }

            if (MG_Random.Random() <= MG_Settings.INI_ChanceOutsideMission)
            {
                return DestinationSetting.OutsideCity;
            }          
            else
            {
                return DestinationSetting.InsideCIty;
            }

        }

        private static TargetReaction GenerateReaction()
        {
            List<TargetReaction> list;
            switch (MG_Target.WeaponTraining)
            {
                case WeaponTraining.None:
                    list = new List<TargetReaction>() { TargetReaction.Medium, TargetReaction.Low };
                    break;
                case WeaponTraining.Selfdefence:
                    list = new List<TargetReaction>() { TargetReaction.Medium, TargetReaction.Low };
                    break;
                case WeaponTraining.Military:
                    list = new List<TargetReaction>() { TargetReaction.VeryHigh, TargetReaction.High };
                    break;
                case WeaponTraining.Veteran:
                    list = new List<TargetReaction>() { TargetReaction.VeryHigh, TargetReaction.High };
                    break;
                case WeaponTraining.Elite:
                    list = new List<TargetReaction>() { TargetReaction.VeryHigh };
                    break;
                default:
                    list = new List<TargetReaction>() { TargetReaction.VeryHigh, TargetReaction.High, TargetReaction.Medium, TargetReaction.Low };
                    break;
            }
            return MG_Random.RandomElement(list);
        }

        private static int GenerateCoolDown()
        {
            switch (MG_Target.Reaction)
            {
                case TargetReaction.Low:
                    return MG_Random.Random(30, 60);
                case TargetReaction.Medium:
                    return MG_Random.Random(14, 30);
                case TargetReaction.High:
                    return MG_Random.Random(7, 14);
                case TargetReaction.VeryHigh:
                    return MG_Random.Random(3, 7);
                default:
                    return MG_Random.Random(10, 60);
            }
        }

        public static void AddManyTimes(ArmorType item, int n, List<ArmorType> list)
        {
            for (int i = 0; i < n; i++)
            {
                list.Add(item);
            }
        }
        #endregion Private Methods
    }
}
