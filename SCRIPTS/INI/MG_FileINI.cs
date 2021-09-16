////////////////////////////////////////////////////////////////////////////////
//
//	MG_FileINI.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace MG_Liquidator
{

    public static class MG_FileINI
    {
        #region Fields       
        public static readonly string path = @"scripts\MG_Liquidator.ini";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        private static string _currentVersion = "v1.02";
        #endregion Fields

        #region Properties       
        public static void ReadInit()
        {
            bool needToCreate = false;
            if (File.Exists(path))
            {
                string currentVersion = IniReadValue("VERSION", "CurrentVersion");
                if (_currentVersion.Equals(currentVersion) == false)
                {
                    needToCreate = true;
                }
                else
                {
                    //Text("File exists...");
                    //MG_Message.SubTitle("Loading", 5000);
                    MG_Statistic.TotalTargetsEliminated = Int32.Parse(IniReadValue("CurrentSavedSession", "TotalTargetsEliminated"));

                    MG_Controls.Enabled = bool.Parse(IniReadValue("HotKey_StartCancelMission", "Enabled"));
                    MG_Controls.MainButtonToBeginMission = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), IniReadValue("HotKey_StartCancelMission", "Button"), true);

                    MG_DifficultyRaiser._LIMIT_Bodyguards = Int32.Parse(IniReadValue("DifficultySettings", "MAXIMUM_Bodyguards"));
                    MG_DifficultyRaiser._LIMIT_WatchersSquad = Int32.Parse(IniReadValue("DifficultySettings", "MAXIMUM_GhostSquadSize"));
                    MG_DifficultyRaiser._LIMIT_WatchersProbabilityPercentage = Int32.Parse(IniReadValue("DifficultySettings", "MAXIMUM_GhostSquadProbabilityToAppear"));
                    MG_DifficultyRaiser._LIMIT_BackupForceSquad = Int32.Parse(IniReadValue("DifficultySettings", "MAXIMUM_BackupForceSquads"));

                    MG_Bombermania.Distance_to_blow = Int32.Parse(IniReadValue("DifficultySettings", "SuicideBomber_Distance_to_blow"));
                    MG_Bombermania.SuicideVest_power_of_explosion = Int32.Parse(IniReadValue("DifficultySettings", "SuicideBomber_Distance_to_blow"));
                    MG_Bombermania.CarBomb_power_of_explosion = Int32.Parse(IniReadValue("DifficultySettings", "SuicideBomber_Distance_to_blow"));


                    MG_DifficultyRaiser._UNLOCK_ALL_TYPES = bool.Parse(IniReadValue("ProgressSettings", "UNLOCK_ALL_TYPES"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_GANGMEMBER = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_GANGMEMBER"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_ASSASIN = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_ASSASIN"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_POLICE = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_POLICE"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_HACKER = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_HACKER"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_CARTEL = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_CARTEL"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_TERRORIST = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_TERRORIST"));
                    MG_DifficultyRaiser._KILLS_NEEDED_TO_UNLOCK_MILITARY = Int32.Parse(IniReadValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_MILITARY"));


                    MG_TargetChecker.DISTANCE_TO_ESCAPE = Int32.Parse(IniReadValue("Target", "DISTANCE_TO_ESCAPE"));
                    MG_TargetChecker.Enable_red_cone = bool.Parse(IniReadValue("Target", "EnableRedCone"));

                    MG_Player.DisableHealthRegeneration = bool.Parse(IniReadValue("HardCoreSettings", "DisablePlayerHealthRegeneration"));

                    MG_Hitman.Agent47WithRandomMask = bool.Parse(IniReadValue("EasterEgg", "Agent47WithRandomMask"));

                    MG_Test.DEBUG = bool.Parse(IniReadValue("DEBUG", "debug"));
                    //MG_Message.SubTitle("LOADED!", 5000);

                    MG_iFruit.ContactName = IniReadValue("iFruit_CellPhone", "Contact Name");
                    MG_iFruit.Enabled = bool.Parse(IniReadValue("iFruit_CellPhone", "Enabled"));

                    MG_Settings.INI_AdvisorName = IniReadValue("Advisor", "Name");
                    MG_Advisor.DisableAdvisorVoice = bool.Parse(IniReadValue("Advisor", "Disable Advisor voice"));
                    MG_Advisor.DisableMessageReceivedVoice = bool.Parse(IniReadValue("Advisor", "Disable Message received voice"));

                    MG_GarbageCollector.Distance_DestroyedVehicle = Int32.Parse(IniReadValue("GarbageCollector", "Distance_DestroyedVehicle_remove"));
                    MG_GarbageCollector.Distance_EmptyVehicle = Int32.Parse(IniReadValue("GarbageCollector", "Distance_EmptyVehicle_remove"));
                    MG_GarbageCollector.Distance_NotEmptyVehicle = Int32.Parse(IniReadValue("GarbageCollector", "Distance_VehicleWithOccupants_remove"));

                    MG_ForgottenEnemy.Distance_to_delete_alive_ped = Int32.Parse(IniReadValue("GarbageCollector", "Distance_aliveBodyguard_remove"));
                    MG_ForgottenEnemy.Distance_to_delete_dead_ped = Int32.Parse(IniReadValue("GarbageCollector", "Distance_deadBodyguard_remove"));
                
                    MG_Settings.INI_ChanceOutsideMission = Int32.Parse(IniReadValue("Mission", "Chance of Outside of the city"));
                    MG_Settings.INI_DisableOutsideMissions = bool.Parse(IniReadValue("Mission", "Disable Outside of the city"));

 
                }
            }
            else
            {
                needToCreate = true;
            }

            if (needToCreate)
            {
                IniWriteValue("VERSION", "CurrentVersion", _currentVersion);

                IniWriteValue("iFruit_CellPhone", "Enabled", "true");
                IniWriteValue("iFruit_CellPhone", "Contact Name", "Bane");

                IniWriteValue("Advisor", "Name", "Bane");
                IniWriteValue("Advisor", "Disable Advisor voice", "false");
                IniWriteValue("Advisor", "Disable Message received voice", "false");

                IniWriteValue("HotKey_StartCancelMission", "Enabled", "true");
                IniWriteValue("HotKey_StartCancelMission", "Button", System.Windows.Forms.Keys.NumPad1.ToString());


                IniWriteValue("CurrentSavedSession", "TotalTargetsEliminated", "0");
                IniWriteValue("CurrentSavedSession", "TotalFailedObjectives", "0");


                IniWriteValue("DifficultySettings", "MAXIMUM_Bodyguards", "25");// 25 peds on 5-7 cars
                IniWriteValue("DifficultySettings", "MAXIMUM_GhostSquadSize", "3");//3*3*4 = 36 cop peds on 9 cars
                IniWriteValue("DifficultySettings", "MAXIMUM_GhostSquadProbabilityToAppear", "20");
                IniWriteValue("DifficultySettings", "MAXIMUM_BackupForceSquads", "5");//3*3*4 = 36 peds on 9 cars

                IniWriteValue("DifficultySettings", "SuicideBomber_Distance_to_blow", "7");
                IniWriteValue("DifficultySettings", "SuicideVest_power_of_explosion", "10");
                IniWriteValue("DifficultySettings", "CarBomb_power_of_explosion", "15");




                IniWriteValue("ProgressSettings", "UNLOCK_ALL_TYPES", "false");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_GANGMEMBER", "5");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_ASSASIN", "10");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_POLICE", "15");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_HACKER", "20");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_CARTEL", "25");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_TERRORIST", "30");
                IniWriteValue("ProgressSettings", "KILLS_NEEDED_TO_UNLOCK_MILITARY", "35");

                IniWriteValue("Target", "DISTANCE_TO_ESCAPE", "1650");
                IniWriteValue("Target", "EnableRedCone", "true");

                IniWriteValue("Mission", "Disable Outside of the city", "false");
                IniWriteValue("Mission", "Chance of Outside of the city", "7");

                IniWriteValue("HardCoreSettings", "DisablePlayerHealthRegeneration", "false");

                IniWriteValue("GarbageCollector", "Distance_DestroyedVehicle_remove", "100");
                IniWriteValue("GarbageCollector", "Distance_EmptyVehicle_remove", "200");
                IniWriteValue("GarbageCollector", "Distance_VehicleWithOccupants_remove", "650");

                IniWriteValue("GarbageCollector", "Distance_aliveBodyguard_remove", "300");
                IniWriteValue("GarbageCollector", "Distance_deadBodyguard_remove", "50");

                IniWriteValue("EasterEgg", "Agent47WithRandomMask", "true");

                IniWriteValue("DEBUG", "debug", "false");
            }
        }

        public static void WriteInit_Statistics()
        {
            IniWriteValue("CurrentSavedSession", "TotalTargetsEliminated", MG_Statistic.TotalTargetsEliminated.ToString());
            IniWriteValue("CurrentSavedSession", "TotalFailedObjectives", MG_Statistic.TotalFailedMissions.ToString());
        }
        #endregion Properties


        #region Constructor
        #endregion Constructor

        #region Public Methods
        #endregion Public Methods

        #region Private Methods       
        private static void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, MG_FileINI.path);
        }

        private static string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, MG_FileINI.path);
            return temp.ToString();
        }
        #endregion Private Methods
    }


}