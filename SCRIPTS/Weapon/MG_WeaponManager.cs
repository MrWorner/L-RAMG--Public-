////////////////////////////////////////////////////////////////////////////////
//
//	MG_WeaponManager.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Native;
using System.Collections.Generic;

namespace MG_Liquidator
{

    public enum WeaponCategory { Melee, Handgun, SMG, AssaultRifle, SniperRifle, Shotgun, HeavyWeapon, ThrownWeapon };
    public enum WeaponLevel { None, Melee_1, Handgun_2, SMG_and_Shotguns_3, AssaultRifle_4, SniperRifle_5, HeavyWeapon_6 };

    //public static class MG_WeaponManager
    //{
    //
    //    public static void ProcessPed(Ped _ped, WeaponLevel _rearmLevel)
    //    {
    //        GiveGuns(_ped, _rearmLevel);
    //        Function.Call(Hash.GET_BEST_PED_WEAPON, _ped, true);
    //        Function.Call(Hash.SET_PED_CURRENT_WEAPON_VISIBLE, _ped, true, false, true, true);
    //        _ped.CanSwitchWeapons = true;
    //    }

    //    private static void GiveGuns(Ped _ped, WeaponLevel _rearmLevel)
    //    {
    //        //List<WeaponCategory> _wepCategoryArray = new List<WeaponCategory>() { WeaponCategory.Handgun, WeaponCategory.SMG, WeaponCategory.AssaultRifle, WeaponCategory.SniperRifle, WeaponCategory.Shotgun, WeaponCategory.HeavyWeapon };//WeaponCategory.Melee WeaponCategory.ThrownWeapon

    //        List<WeaponCategory> _wepCategoryArray = new List<WeaponCategory>() { };

    //        switch (_rearmLevel)
    //        {
    //            case WeaponLevel.Melee_1:
    //                _wepCategoryArray.Add(WeaponCategory.Melee);
    //                break;
    //            case WeaponLevel.Handgun_2:
    //                _wepCategoryArray.Add(WeaponCategory.Handgun);
    //                break;
    //            case WeaponLevel.SMG_and_Shotguns_3:
    //                _wepCategoryArray.Add(WeaponCategory.SMG);
    //                if (MG_Random.Random(100 + MG_Statistic.TotalTargetsEliminated) > 70)
    //                    _wepCategoryArray.Add(WeaponCategory.Handgun);
    //                break;
    //            case WeaponLevel.AssaultRifle_4:
    //                _wepCategoryArray.Add(WeaponCategory.AssaultRifle);
    //                if (MG_Random.Random(100 + MG_Statistic.TotalTargetsEliminated) > 70)
    //                    _wepCategoryArray.Add(WeaponCategory.Handgun);
    //                break;
    //            case WeaponLevel.SniperRifle_5:
    //                _wepCategoryArray.Add(WeaponCategory.SniperRifle);
    //                if (MG_Random.Random(100 + MG_Statistic.TotalTargetsEliminated) > 70)
    //                    _wepCategoryArray.Add(WeaponCategory.SMG);
    //                break;
    //            case WeaponLevel.HeavyWeapon_6:
    //                _wepCategoryArray.Add(WeaponCategory.HeavyWeapon);
    //                if (MG_Random.Random(100 + MG_Statistic.TotalTargetsEliminated) > 70)
    //                    _wepCategoryArray.Add(WeaponCategory.SMG);
    //                break;
    //            default:
    //                break;
    //        }

    //        //int _wepCount = MG_Main.rnd.Next(1, 3 + MG_Main.TotalTargetsEliminated);
    //        //int _wepCount = MG_Func.Random(1, 3 + MG_Main.TotalTargetsEliminated);
    //        for (int i = 0; i < _wepCategoryArray.Count; i++)
    //        {
    //            WeaponCategory _chosenWepType = _wepCategoryArray[(MG_Random.Random(_wepCategoryArray.Count))];
    //            switch (_chosenWepType)
    //            {
    //                case WeaponCategory.Melee:
    //                    AddWeapon_Melee(_ped);
    //                    break;
    //                case WeaponCategory.Handgun:
    //                    AddWeapon_Handgun(_ped, MG_Random.Random(6, 24));
    //                    break;
    //                case WeaponCategory.SMG:
    //                    AddWeapon_SMG(_ped, MG_Random.Random(30, 90));
    //                    break;
    //                case WeaponCategory.AssaultRifle:
    //                    AddWeapon_AssaultRifles(_ped, MG_Random.Random(30, 90));
    //                    break;
    //                case WeaponCategory.SniperRifle:
    //                    AddWeapon_SniperRifles(_ped, MG_Random.Random(5, 20));
    //                    break;
    //                case WeaponCategory.Shotgun:
    //                    AddWeapon_Shotgun(_ped, MG_Random.Random(6, 20));
    //                    break;
    //                case WeaponCategory.HeavyWeapon:
    //                    AddWeapon_HeavyWeapons(_ped, MG_Random.Random(1, 10));
    //                    break;
    //                case WeaponCategory.ThrownWeapon:
    //                    AddWeapon_ThrownWeapons(_ped, MG_Random.Random(1, 10));
    //                    break;
    //                default:
    //                    break;
    //            }
    //            _wepCategoryArray.Remove(_chosenWepType);
    //        }
    //    }

    //    private static void AddWeapon_Melee(Ped _ped)
    //    {
    //        WeaponHash _wepName = DB_Weapons.MeleeWeapons[MG_Random.Random(DB_Weapons.MeleeWeapons.Length)];
    //        _ped.Weapons.Give(_wepName, 1, true, true);
    //    }

    //    private static void AddWeapon_Handgun(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.Handguns[MG_Random.Random(DB_Weapons.Handguns.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }

    //    private static void AddWeapon_Shotgun(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.Shotguns[MG_Random.Random(DB_Weapons.Shotguns.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }

    //    private static void AddWeapon_SMG(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.SMG[MG_Random.Random(DB_Weapons.SMG.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }

    //    private static void AddWeapon_AssaultRifles(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.AssaultRifles[MG_Random.Random(DB_Weapons.AssaultRifles.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }

    //    private static void AddWeapon_SniperRifles(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.SniperRifles[MG_Random.Random(DB_Weapons.SniperRifles.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }
    //    private static void AddWeapon_HeavyWeapons(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.HeavyWeapons[MG_Random.Random(DB_Weapons.HeavyWeapons.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }
    //    private static void AddWeapon_ThrownWeapons(Ped _ped, int Ammo)
    //    {
    //        WeaponHash _wepName = DB_Weapons.ThrownWeapons[MG_Random.Random(DB_Weapons.ThrownWeapons.Length)];
    //        _ped.Weapons.Give(_wepName, Ammo, true, true);
    //    }

    //}
}