////////////////////////////////////////////////////////////////////////////////////
//
//	MG_Ped.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;

public enum ArmorType { None, Light, Medium, Heavy };
//None - 0
//Light - 25-50
//Medium - 50-85
//Heavy - 85-200
public enum WeaponTraining { None, Selfdefence, Military, Veteran, Elite };
//None - 0-15
//Selfdefence - 15-25
//Military - 25-40
//Veteran - 40-65
//Elite - 65-100

public enum CombatAttributes { 
    CanUseCover = 0, 
    CanUseVehicles = 1,
    CanDoDrivebys = 2, 
    CanLeaveVehicle = 3,
    CanFightArmedPedsWhenNotArmed = 5, 
    CanTauntInVehicle = 20, 
    AlwaysFight = 46, 
    IgnoreTrafficWhenDriving = 52,
    FreezeMovement = 292, 
    PlayerCanUseFiringWeapons = 1424 
}

public enum CombatMovement
{
 
    Stationary = 0,
    Defensive = 1,
    Offensive = 2,
    Suicidal = 3,
    Disabled = 0,
    Random = -1
}

public enum CombatRange
{
    // https://docs.fivem.net/natives/?_0x3C606747B23E497B
    Near = 0,
    Medium = 1,
    Far = 2,
    Disabled = 0,
    Random = -1
}

namespace MG_Liquidator
{
    public class MG_Ped : Script
    {
        #region Public Methods

        public static void ShowWeapon(Ped ped)
        {
            if (!GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_ANY_VEHICLE, ped))
            {
                GTA.Native.Function.Call(Hash.SET_PED_CURRENT_WEAPON_VISIBLE, ped, true, true, false, false);
            }
        }

        public static void HideWeapon(Ped ped)
        {
            if (!GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_ANY_VEHICLE, ped))
            {
                GTA.Native.Function.Call(Hash.SET_PED_CURRENT_WEAPON_VISIBLE, ped, false, true, false, false);
            }
        }

        public static void PlayCellphoneAnim(Ped ped)
        {
            Function.Call(Hash.DISABLE_PLAYER_FIRING, ped, true);//31.01.2020
            ped.Task.PlayAnimation(DB_Animation.AnimDic_cellphonePlayer, DB_Animation.Anim_cellphonePlayer, 8f, 8f, -1, (AnimationFlags.UpperBodyOnly | AnimationFlags.AllowRotation | AnimationFlags.StayInEndFrame), 0f);
        }

        public static void DisableCellphoneAnim(Ped ped)
        {
            Function.Call(Hash.DISABLE_PLAYER_FIRING, ped, false);//31.01.2020
            ped.Task.ClearAnimation(DB_Animation.AnimDic_cellphonePlayer, DB_Animation.Anim_cellphonePlayer);
        }

        public static Prop CreateAndAttachPhone(Ped _ped)
        {
            Prop propPhone = World.CreateProp("prop_npc_phone", _ped.Position + _ped.ForwardVector * 4.0f, true, true);
            propPhone.SetNoCollision(_ped, true);
            AttachToPed(propPhone, _ped, _ped.GetBoneIndex(Bone.IK_R_Hand), new Vector3((propPhone.Model.GetDimensions().X), 0.02f, 0f), new Vector3(120f, 120f, 0f));
            return propPhone;
        }

        //private static void AttachToPed(Entity e1, Entity e2, int boneIndexE2, Vector3 offsetPos, Vector3 rotation, bool useSoftPinning = false, bool collisionBetweenEnts = false, bool entOneIsPed = false, int vertexIndex = 0, bool fixedRot = false)
        private static void AttachToPed(Entity e1, Entity e2, int boneIndexE2, Vector3 offsetPos, Vector3 rotation)
        {
            Function.Call(Hash.ATTACH_ENTITY_TO_ENTITY, e1, e2, boneIndexE2, offsetPos.X, offsetPos.Y, offsetPos.Z, rotation.X, rotation.Y, rotation.Z, -1f, false, false, false, 2, true);
        }

        public static void BreakPhoneCall(Ped ped, Prop propPhone)
        {
            propPhone.Detach();
            DisableCellphoneAnim(ped);
            ShowWeapon(ped);
            propPhone.SetNoCollision(ped, false);
            propPhone.MarkAsNoLongerNeeded();
        }

        public static bool IsInVehicle(Ped ped)
        {
            //GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_IN_ANY_VEHICLE, target)
            return ped.IsInVehicle();
        }

        public static void SetPedCombatAttributes(Ped ped, CombatAttributes combatAttribute, bool enabled)
        {
            //CanUseCover = 0,
            //CanUseVehicles = 1,
            //CanDoDrivebys = 2,
            //CanLeaveVehicle = 3,
            //CanFightArmedPedsWhenNotArmed = 5,
            //CanTauntInVehicle = 20,
            //AlwaysFight = 46,
            //IgnoreTrafficWhenDriving = 52,
            //FreezeMovement = 292,
            //PlayerCanUseFiringWeapons = 1424
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped, (int)combatAttribute, enabled);

        }

        public static void SetPedCombatMovement(Ped ped, int combatMovement)
        {
            //0 - Stationary(Will just stand in place)
            //1 - Defensive(Will try to find cover and very likely to blind fire)  
            //2 - Offensive(Will attempt to charge at enemy but take cover as well)
            //3 - Suicidal Offensive(Will try to flank enemy in a suicidal attack)
            Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, ped, combatMovement);
        }

        public static void SetFiringPattern(Ped ped, FiringPattern firingPattern)
        {
            //Default = 0,
            //BurstFirePumpShotGun = 12239771,
            //BurstInCover = 40051185,
            //FromGround = 577037782,
            //ChernoBarrage = 703122589,
            //BurstFireMicro = 1122960381,
            //BurstFireBursts = 1122960381,
            //AkulaBarrage = 1392378214,
            //SingleShot = 1566631136,
            //DelayFireByOneSec = 2055493265,
            //Pounder2Barrage = 2228901467,
            //BurstFireHeli = 2437838959,
            //TampaMortar = 2452873343,
            //BurstFireRifle = 2624893958,
            //BurstFirePistol = 2685983626,
            //HunterBarrage = 2905356422,
            //BurstFireMG = 3044263348,
            //FullAuto = 3337513804,
            //BurstFireSMG = 3507334638,
            //BurstFireDriveby = 3541198322,
            //BurstFire = 3607063905,
            //BurstFireTank = 3804904049
            ped.FiringPattern = firingPattern;
        }
        #endregion Public Methods

    }
}
