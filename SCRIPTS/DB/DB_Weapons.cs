////////////////////////////////////////////////////////////////////////////////
//
//	DB_Weapons.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://wiki.altv.mp/wiki/GTA:Weapon_Models
//https://wiki.rage.mp/index.php?title=Weapons
//https://wiki.gtanet.work/index.php?title=Weapons_Models
//https://cdn.rage.mp/public/odb/index.html#25



namespace MG_Liquidator
{
    //WeaponHash.MilitaryRifle БАГНУТ!
    //WeaponHash.AssautlShotgun БАГНУТ!


    public static class DB_Weapons
    {
        //https://pinetools.com/add-text-each-line
        //https://wiki.gtanet.work/index.php?title=Weapons_Models
        //https://wiki.gtanet.work/index.php/Peds
        //https://cdn.rage.mp/public/odb/index.html#25


        public static WeaponHash[] BodyGuard_Normal { get; } =
        {
            WeaponHash.CombatPistol,
            WeaponHash.CeramicPistol,
            WeaponHash.Pistol,

            WeaponHash.SMG,
            WeaponHash.SMGMk2,
            WeaponHash.CombatPDW,

            WeaponHash.BullpupShotgun,
            WeaponHash.HeavyShotgun,

            WeaponHash.AdvancedRifle,
            WeaponHash.CarbineRifle,
            WeaponHash.CarbineRifleMk2
        };

        public static WeaponHash[] BodyGuard_Assasin { get; } =
        {
            WeaponHash.MiniSMG,
            WeaponHash.CompactRifle,
            WeaponHash.SniperRifle
        };

        public static WeaponHash[] BodyGuard_Police { get; } =
        {

            WeaponHash.Pistol,
            WeaponHash.PistolMk2,
            WeaponHash.Revolver,

            WeaponHash.SMG,
            WeaponHash.CombatPDW,
            WeaponHash.SMGMk2,

            WeaponHash.PumpShotgun,
            WeaponHash.PumpShotgunMk2,

            WeaponHash.CarbineRifle,
            WeaponHash.CarbineRifleMk2,
            WeaponHash.AssaultrifleMk2
        };

        public static WeaponHash[] BodyGuard_Cartel { get; } =
        {

            WeaponHash.SNSPistol,
            WeaponHash.SNSPistolMk2,
            WeaponHash.Revolver,

            WeaponHash.MicroSMG,
            WeaponHash.AssaultSMG,
            WeaponHash.CombatPDW,

            WeaponHash.AssaultShotgun,
            WeaponHash.HeavyShotgun,
            WeaponHash.SweeperShotgun,

            WeaponHash.AssaultRifle,
            WeaponHash.AssaultrifleMk2,
            WeaponHash.CompactRifle,
            WeaponHash.CarbineRifle,

            WeaponHash.MG,
            WeaponHash.Gusenberg
        };

        public static WeaponHash[] BodyGuard_Hacker { get; } =
        {

            WeaponHash.VintagePistol,
            WeaponHash.PistolMk2,
            WeaponHash.CeramicPistol,

            WeaponHash.MiniSMG,
            WeaponHash.SMGMk2,

            WeaponHash.SawnOffShotgun,

            WeaponHash.CompactRifle

        };

        public static WeaponHash[] BodyGuard_Military { get; } =
        {

            WeaponHash.AssaultShotgun,

            WeaponHash.AssaultSMG,

            WeaponHash.SpecialCarbine,
            WeaponHash.CarbineRifleMk2,

            WeaponHash.CombatMG,
            WeaponHash.CombatMGMk2,

            WeaponHash.MarksmanRifle,
            WeaponHash.MarksmanRifleMk2,

            WeaponHash.GrenadeLauncher
        };

        public static WeaponHash[] BodyGuard_GangMember { get; } =
        {

            WeaponHash.Knife,
            WeaponHash.Bat,
            WeaponHash.KnuckleDuster,
            WeaponHash.Bottle,

            WeaponHash.VintagePistol,
            WeaponHash.CeramicPistol,
            WeaponHash.CombatPistol,
            //WeaponHash.MarksmanPistol,
            WeaponHash.Revolver,

            WeaponHash.DoubleBarrelShotgun,
            //WeaponHash.Musket,
            WeaponHash.SawnOffShotgun,
            WeaponHash.SweeperShotgun,

            WeaponHash.MachinePistol,
            WeaponHash.MicroSMG,
            WeaponHash.MiniSMG,

            WeaponHash.AssaultRifle,

            WeaponHash.Gusenberg

        };

        public static WeaponHash[] BodyGuard_Bomber { get; } =
        {
             //WeaponHash.Knife,
              //WeaponHash.CeramicPistol,
           // WeaponHash.CombatPistol,
              WeaponHash.MicroSMG,
              WeaponHash.MiniSMG,
              WeaponHash.MicroSMG,
              WeaponHash.MiniSMG,

            WeaponHash.AssaultRifle,
            WeaponHash.AssaultrifleMk2,
            WeaponHash.AssaultRifle,
            WeaponHash.AssaultrifleMk2,

            WeaponHash.MG,

            WeaponHash.Firework,
            WeaponHash.RPG
        };













        public static readonly WeaponHash[] MeleeWeapons =
        {
            WeaponHash.Knife,
            WeaponHash.Nightstick,
            WeaponHash.Hammer,
            WeaponHash.Bat,
            WeaponHash.Crowbar,
            WeaponHash.GolfClub,
            WeaponHash.Bottle,
            WeaponHash.Dagger,
            WeaponHash.Hatchet,
            WeaponHash.KnuckleDuster,
            WeaponHash.Machete,
            //WeaponHash.Flashlight,
            WeaponHash.SwitchBlade,
            WeaponHash.PoolCue,
            WeaponHash.Wrench,
            WeaponHash.BattleAxe
        };

        public static readonly WeaponHash[] Handguns =
        {
            WeaponHash.Pistol,
            WeaponHash.PistolMk2,
            WeaponHash.CombatPistol,
            WeaponHash.Pistol50,
            WeaponHash.SNSPistol,
            WeaponHash.HeavyPistol,
            WeaponHash.VintagePistol,
            WeaponHash.MarksmanPistol,
            WeaponHash.Revolver,
            WeaponHash.APPistol,
            WeaponHash.StunGun
            //WeaponHash.FlareGun
        };

        public static readonly WeaponHash[] SMG =
        {
            WeaponHash.MicroSMG,
            WeaponHash.MachinePistol,
            WeaponHash.SMG,
            WeaponHash.SMGMk2,
            WeaponHash.AssaultSMG,
            WeaponHash.CombatPDW,
            WeaponHash.MG,
            WeaponHash.CombatMG,
            WeaponHash.CombatMGMk2,
            WeaponHash.Gusenberg,
            WeaponHash.MiniSMG
        };

        public static readonly WeaponHash[] AssaultRifles =
        {
            WeaponHash.AssaultRifle,
            WeaponHash.AssaultrifleMk2,
            WeaponHash.CarbineRifle,
            WeaponHash.CarbineRifleMk2,
            WeaponHash.AdvancedRifle,
            WeaponHash.SpecialCarbine,
            WeaponHash.BullpupRifle,
            WeaponHash.CompactRifle
        };

        public static readonly WeaponHash[] SniperRifles =
        {
            WeaponHash.SniperRifle,
            WeaponHash.HeavySniper,
            WeaponHash.HeavySniperMk2,
            WeaponHash.MarksmanRifle
        };

        public static readonly WeaponHash[] Shotguns =
        {
            WeaponHash.PumpShotgun,
            WeaponHash.SawnOffShotgun,
            WeaponHash.BullpupShotgun,
            WeaponHash.AssaultShotgun,
            WeaponHash.Musket,
            WeaponHash.HeavyShotgun,
            WeaponHash.DoubleBarrelShotgun,
            WeaponHash.SweeperShotgun
        };

        public static readonly WeaponHash[] HeavyWeapons =
        {
            WeaponHash.GrenadeLauncher,
            WeaponHash.RPG,
            WeaponHash.Minigun,
            WeaponHash.Firework,
            WeaponHash.Railgun,
            WeaponHash.HomingLauncher,
            //WeaponHash.GrenadeLauncherSmoke,
            WeaponHash.CompactGrenadeLauncher
        };

        public static readonly WeaponHash[] ThrownWeapons =
        {
            WeaponHash.Grenade,
            //WeaponHash.StickyBomb,
            //WeaponHash.ProximityMine,
            //WeaponHash.BZGas,
            WeaponHash.Molotov
            //WeaponHash.FireExtinguisher,
            //WeaponHash.PetrolCan,
            //WeaponHash.Flare,
            //WeaponHash.Ball,
            //WeaponHash.Snowball,
            //WeaponHash.SmokeGrenade,
            //WeaponHash.PipeBomb
        };


        public static readonly WeaponHash[] BannedWeaponsByHitman =
       {
            WeaponHash.Grenade,
            WeaponHash.StickyBomb,
            WeaponHash.ProximityMine,
            //WeaponHash.BZGas,
            WeaponHash.Molotov,
            //WeaponHash.FireExtinguisher,
            //WeaponHash.PetrolCan,
            //WeaponHash.Flare,
            //WeaponHash.Ball,
            //WeaponHash.Snowball,
            //WeaponHash.SmokeGrenade,
            WeaponHash.PipeBomb,
            WeaponHash.GrenadeLauncher,
            WeaponHash.RPG,
            WeaponHash.Minigun,
            //WeaponHash.Firework,
            WeaponHash.Railgun,
            //WeaponHash.HomingLauncher,
            //WeaponHash.GrenadeLauncherSmoke,
            WeaponHash.CompactGrenadeLauncher
        };

        //https://wiki.gtanet.work/index.php/Peds
        //public static readonly string[] PedBodyGuardModel =
        //{
        //   ""
        //};

        /*
        public static readonly WeaponHash[]  =
        {
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.,
            WeaponHash.
        };
    }
    */

        public static readonly WeaponHash[] AllWeapons =
  {
            WeaponHash.Knife,
            WeaponHash.Nightstick,
            WeaponHash.Hammer,
            WeaponHash.Bat,
            WeaponHash.Crowbar,
            WeaponHash.GolfClub,
            WeaponHash.Bottle,
            WeaponHash.Dagger,
            WeaponHash.Hatchet,
            WeaponHash.KnuckleDuster,
            WeaponHash.Machete,
            WeaponHash.Flashlight,
            WeaponHash.SwitchBlade,
            WeaponHash.PoolCue,
            WeaponHash.Wrench,
            WeaponHash.BattleAxe,
        //Handguns
            WeaponHash.Pistol,
            WeaponHash.PistolMk2,
            WeaponHash.CombatPistol,
            WeaponHash.Pistol50,
            WeaponHash.SNSPistol,
            WeaponHash.HeavyPistol,
            WeaponHash.VintagePistol,
            WeaponHash.MarksmanPistol,
            WeaponHash.Revolver,
            WeaponHash.APPistol,
            WeaponHash.StunGun,
            WeaponHash.FlareGun,
        //SMG
            WeaponHash.MicroSMG,
            WeaponHash.MachinePistol,
            WeaponHash.SMG,
            WeaponHash.SMGMk2,
            WeaponHash.AssaultSMG,
            WeaponHash.CombatPDW,
            WeaponHash.MG,
            WeaponHash.CombatMG,
            WeaponHash.CombatMGMk2,
            WeaponHash.Gusenberg,
            WeaponHash.MiniSMG,
        //AssaultRifles
            WeaponHash.AssaultRifle,
            WeaponHash.AssaultrifleMk2,
            WeaponHash.CarbineRifle,
            WeaponHash.CarbineRifleMk2,
            WeaponHash.AdvancedRifle,
            WeaponHash.SpecialCarbine,
            WeaponHash.BullpupRifle,
            WeaponHash.CompactRifle,
        //SniperRifles
            WeaponHash.SniperRifle,
            WeaponHash.HeavySniper,
            WeaponHash.HeavySniperMk2,
            WeaponHash.MarksmanRifle,
        //Shotguns
            WeaponHash.PumpShotgun,
            WeaponHash.SawnOffShotgun,
            WeaponHash.BullpupShotgun,
            WeaponHash.AssaultShotgun,
            WeaponHash.Musket,
            WeaponHash.HeavyShotgun,
            WeaponHash.DoubleBarrelShotgun,
            WeaponHash.SweeperShotgun,
        //HeavyWeapons
            WeaponHash.GrenadeLauncher,
            WeaponHash.RPG,
            WeaponHash.Minigun,
            WeaponHash.Firework,
            WeaponHash.Railgun,
            WeaponHash.HomingLauncher,
            WeaponHash.GrenadeLauncherSmoke,
            WeaponHash.CompactGrenadeLauncher,
        //ThrownWeapons
            WeaponHash.Grenade,
            WeaponHash.StickyBomb,
            WeaponHash.ProximityMine,
            WeaponHash.BZGas,
            WeaponHash.Molotov,
            WeaponHash.FireExtinguisher,
            WeaponHash.PetrolCan,
            WeaponHash.Flare,
            WeaponHash.Ball,
            WeaponHash.Snowball,
            WeaponHash.SmokeGrenade,
            WeaponHash.PipeBomb
        };
    }
}
