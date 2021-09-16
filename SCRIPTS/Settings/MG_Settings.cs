////////////////////////////////////////////////////////////////////////////////
//
//	MG_Settings.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

namespace MG_Liquidator
{
    public static class MG_Settings
    {
        //------INI SETTINGS
        public static bool INI_isCanBeCancelled { get; set; } = true;
        public static bool INI_ShowTargetBlip { get; set; } = true;
        public static bool INI_ShowTargetDistance { get; set; } = false;
        public static string INI_AdvisorName { get; set; } = "Bane";
        public static bool INI_ShowTargetMarker { get; set; } = true;

        //------Mission Preset
        public static bool INI_DisableOutsideMissions { get; set; } = false;
        public static int INI_ChanceOutsideMission { get; set; } = 7;

        //------CalculateSafeDistance
        public static float INI_CriticalDistanceToBeSpotted { get; set; } = 0f;
        public static float INI_increaseCritDistance_WeaponInHands { get; set; } = 20f;//25
        public static float INI_increaseCritDistance_AIMING { get; set; } = 10f;//5
        public static float INI_increaseCritDistance_DammagedRandomPed { get; set; } = 20f;//
        public static float INI_CriticalDistanceToBeSpotted_KnownFace { get; set; } = 15f;
        public static float INI_MaxCriticalDistance { get; set; } = 90f;
        public static float INI_increaseCritDistance_Running { get; set; } = 3f;//
        public static float INI_increaseCritDistance_Sprinting { get; set; } = 15f;//
        public static float INI_decreaseCritDistance_InVCover { get; set; } = 17f;//20
        public static float INI_decreaseCritDistance_InVCover_AIMING { get; set; } = 7f;//10
        public static bool INI_CanBeRecognizeByFace { get; set; } = true;

        //------BodyGuards
        //public static bool INI_BodyGuards_ON { get; set; } = true;
        //public static int INI_BodyGuards_Max { get; set; } = 10;

        //------Observers
        //public static bool INI_Observers_ON { get; set; } = true;
        //public static int INI_Observers_Max { get; set; } = 10;

        //-------Police watchers
        //public static bool INI_Watchers_ON { get; set; } = true;
        //public static int INI_Watchers_Max { get; set; } = 4;

        //-------Gang watchers
        //private static bool gangWatchers_ON = true;
        //private static int gangWatchers_Max = 4;
        //public static bool INI_GangWatchers_ON { get => gangWatchers_ON; set => gangWatchers_ON = value; }
        //public static int INI_GangWatchers_Max { get => gangWatchers_Max; set => gangWatchers_Max = value; }

        //-------MG_DifficultManager
        //public static bool INI_RaiseDifficultBySuccess { get; set; } = true;


        //---------------------------------------------------------------
    }
}
