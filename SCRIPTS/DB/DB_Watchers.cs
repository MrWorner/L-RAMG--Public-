////////////////////////////////////////////////////////////////////////////////
//
//	DB_Watchers.cs
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

namespace MG_Liquidator
{
    public static class DB_Watchers
    {
        public static string[] STEALTH_HQTRUCK_SWAT_SET { get; } =
        {
           "TERBYTE",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };
        public static string[] STEALTH_HQTRUCK2_SWAT_SET { get; } =
        {
           "BRICKADE",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] STEALTH_AMBULANCE_SWAT_SET { get; } =
        {
           "AMBULANCE",
           "S_M_M_PARAMEDIC_01",
           "S_M_M_PARAMEDIC_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] STEALTH_AMBULANCE2_SWAT_SET { get; } =
        {
           "AMBULANCE",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] STEALTH_TAXI_SWAT_SET { get; } =
        {
           "taxi",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] COMBAT_APC_SWAT_SET { get; } =
        {
           "APC",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] COMBAT_INSURGENT_SWAT_SET { get; } =
        {
           "INSURGENT",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] COMBAT_INSURGENT2_SWAT_SET { get; } =
        {
           "INSURGENT2",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };


        public static string[] COMBAT_INSURGENT3_SWAT_SET { get; } =
        {
           "INSURGENT3",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] COMBAT_menacer_SWAT_SET { get; } =
        {
           "MENACER",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] Combat_Riot_SWAT_SET { get; } =
        {
           "RIOT",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] UNMARKED_PoliceCruiser_SWAT_SET { get; } =
        {
           "POLICE4",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] UNMARKED_PoliceCruiser_FBI_SET { get; } =
        {
           "POLICE4",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01"
        };

        public static string[] UNMARKED_PoliceOffroad_SWAT_SET { get; } =
        {
           "FBI2",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01",
           "S_M_Y_SWAT_01"
        };

        public static string[] UNMARKED_PoliceOffroad_FBI_SET { get; } =
        {
           "FBI2",
           //"FBI2",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01",
           "mp_m_fibsec_01"
        };

        public static string[] STEALTH_CIVILCAR_PACK { get; } =
        {
            "PRAIRIE",
            "ASBO",
            "COGCABRIO",
            "FELON",
            "JACKAL",
            "ORACLE",
            "ORACLE2",
            "ZION",
            "DOMINATOR3",
            "ASTEROPE",
            "PRIMO",
            "PRIMO2",
            "SCHAFTER6",
            "STANIER",
            "WASHINGTON",
            "BUFFALO2",
            "BUFFALO",
            "ELEGY2",
            "SULTAN2",
            "SULTAN",
            "TORERO",
            "GRANGER",
            "SEMINOLE",
            "BALLER",
            "BALLER2",
            "BALLER3",
            "BALLER4",
            "BALLER5",
            "HUNTLEY",
            "RADI",
            "SEMINOLE2",
            "XLS2",
            "XLS"
        };

        public static string[] STEALTH_VANCAR_PACK { get; } =
        {
            "BOXVILLE2",
            "BOXVILLE3",
            "BOXVILLE4",
            "BURRITO",
            "BURRITO2",
            "BURRITO3",
            "BURRITO4",
            "PONY",
            "PONY2",
            "RUMPO",
            "RUMPO2",
            "SPEEDO",
            "SPEEDO4"
        };

        public static string[] WEAPON_SET_SMG { get; } =
        {
           "weapon_pistol50",
           "weapon_smg"
        };

        public static WeaponComponent[][] WEAPON_SET_SMG_A { get; } =
       {
           new WeaponComponent[]{ WeaponComponent.Pistol50Clip02, WeaponComponent.AtPiFlsh},
           new WeaponComponent[]{ WeaponComponent.SMGClip02,WeaponComponent.AtArFlsh, WeaponComponent.AtScopeMacro02}
        };

        public static string[] WEAPON_SET_SHOTGUN { get; } =
        {
            "weapon_revolver_mk2",
            "weapon_assaultshotgun"
        };

        public static WeaponComponent[][] WEAPON_SET_SHOTGUN_A { get; } =
        {
           new WeaponComponent[]{ WeaponComponent.RevolverMk2ClipFMJ, WeaponComponent.AtSights , WeaponComponent.AtSights, WeaponComponent.AtPiFlsh},
           new WeaponComponent[]{WeaponComponent.PumpShotgunMk2ClipHollowPoint,WeaponComponent.AtScopeSmallMk2,WeaponComponent.AtArFlsh }
        };

        public static string[] WEAPON_SET_ASSASULTRIFLE { get; } =
        {
            "weapon_pistol_mk2" ,
            "weapon_specialcarbine"
        };

        public static WeaponComponent[][] WEAPON_SET_ASSASULTRIFLE_A { get; } =
        {        
           new WeaponComponent[]{ WeaponComponent.AtPiFlsh02,WeaponComponent.PistolMk2Clip02, WeaponComponent.PistolMk2ClipFMJ, WeaponComponent.AtPiRail },
           new WeaponComponent[]{ WeaponComponent.SpecialCarbineClip02, WeaponComponent.AtArFlsh, WeaponComponent.AtScopeMedium}
        };

    }
}
