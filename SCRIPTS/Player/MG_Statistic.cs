////////////////////////////////////////////////////////////////////////////////
//
//	MG_Statistic.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MG_Liquidator
{

    public enum MissionStatus { WIN, FAIL, CANCELLED };

    public static class MG_Statistic
    {



        #region Fields
        //private static List<string> lines = new List<string>();
        private static string _separator = " | ";
        #endregion Fields

        #region Properties
        public static int TotalTargetsEliminated { get; set; } = 0;
        public static int TotalFailedMissions { get; set; } = 0;
        #endregion Properties

        #region Constructor
        #endregion Constructor


        #region Public Methods

        public static void SaveHistory(MissionStatus status)
        {
            //SEPARATOR https://stackoverflow.com/questions/1126915/how-do-i-split-a-string-by-a-multi-character-delimiter-in-c
            // List<string> lines = new List<string>();

            string s_status = GetText(status);
            string s_success = TotalTargetsEliminated.ToString() + _separator;
            string s_targetHasToBeInVehicle = ((MG_TargetFinder.HasToBeInVehicle == true) ? "InVehicle" : "OnFoot") + _separator;
            string s_type = GetText(MG_Target.Type);
            string s_morale = GetText(MG_Target.Morale);
            string s_weaponTraining = GetText(MG_Target.WeaponTraining);
            //string s_WeaponLevel = MG_Target.WeaponLevel.ToString() + _separator;
            string s_Accuracy = MG_Target.Accuracy.ToString() + _separator;
            string s_ArmorType = GetText(MG_Target.ArmorType);
            string s_CoolDown = (MG_TargetAI_ExtraActions.CoolDown/1000 ).ToString() + "s" + _separator;
            //string s_TargetExtraTaskAvaiableList = String.Join(", ", MG_TargetAI_ExtraActions.TargetExtraTaskAvaiableList.ToArray()) + _separator;

            string s_Bodyguards = MG_TargetBodyGuards.AmountCanBeSpawn.ToString() + _separator;
            string s_WatcherSquads = MG_Watchers.AmountSquadsCanBeSpawn.ToString() + _separator;
            string s_BackupForce = MG_BackupForce.AmountSquadsCanBeSpawn.ToString() + _separator;
            string s_wantedLevel = MG_Player.Player.WantedLevel.ToString() + _separator;
            string s_destinationSetting = GetText(MG_AssassinationMission.DestinationSetting);

            string s_position = MG_AssassinationMission.PositionWhereTargetCanBeFound.ToString() + _separator; 

            string s_currentDate = DateTime.Now.ToString("h:mm tt dd.MM.yyy"); 

            string s_final = "";

            s_final += "Mission status: " + s_status;
            s_final += "Total target kills: " + s_success;
            s_final += s_targetHasToBeInVehicle;
            s_final += "Type: " + s_type;
            s_final += "Morale: " + s_morale;
            s_final += "Weapon training: " + s_weaponTraining;
            //s_final += "Weapon level: " + s_WeaponLevel;
            s_final += "Accuracy: " + s_Accuracy;
            s_final += "Armor: " + s_ArmorType;
            s_final += "Reaction time: " + s_CoolDown;
            //s_final += "Extra: " + s_TargetExtraTaskAvaiableList;
            s_final += "Bodyguards: " + s_Bodyguards;
            s_final += "Ghost squads: " + s_WatcherSquads;
            s_final += "Backup available: " + s_BackupForce;
            s_final += "Wanted level: " + s_wantedLevel;
            s_final += "Destination: " + s_destinationSetting;
            s_final += "Position: " + s_position;
            s_final += "Date: " + s_currentDate;

            int lines = MG_File.GetLineCountsInHistoryDB() + 1;
            MG_File.SaveHistory(lines + " " + s_final);
        }

        public static void TryToLoadProgressFromDB()
        {
            //if (TotalTargetsEliminated > 0) return;
   
            if (File.Exists(MG_File.HistoryFile))
            {
                string lastLine = File.ReadLines(MG_File.HistoryFile).Last();
                string result = lastLine.Split(new string[] { "Total target kills: " }, StringSplitOptions.None)[1].Split(' ')[0].Trim();

                int value;
                if (int.TryParse(result, out value))
                {
                    if (value > 0)
                    {
                        if (value > TotalTargetsEliminated)
                        {
                            TotalTargetsEliminated = value;
                        }
                    }
                }

            }
        }
        #endregion Public Methods

        #region Private Methods
        private static string GetText(MissionStatus info)
        {
            string result = _separator;
            switch (info)
            {
                case MissionStatus.WIN:
                    result += "WIN";
                    break;
                case MissionStatus.FAIL:
                    result += "FAIL";
                    break;
                case MissionStatus.CANCELLED:
                    result += "CANCELLED";
                    break;
                default:
                    result += "UNKNOWN";
                    break;
            }
            result += _separator;

            return result;
        }
        private static string GetText(TargetType info)
        {
            string result = "";
            switch (info)
            {
                case TargetType.Normal:
                    result += "Normal";
                    break;
                case TargetType.Assasin:
                    result += "Assasin";
                    break;
                case TargetType.Terrorist:
                    result += "Terrorist";
                    break;
                case TargetType.Police:
                    result += "Police";
                    break;
                case TargetType.Cartel:
                    result += "Cartel";
                    break;
                case TargetType.Hacker:
                    result += "Hacker";
                    break;
                case TargetType.Military:
                    result += "Military";
                    break;
                case TargetType.GangMember:
                    result += "GangMember";
                    break;
                default:
                    result += "UNKNOWN";
                    break;
            }
            result += _separator;

            return result;
        }
        private static string GetText(TargetMorale info)
        {
            string result = "";
            switch (info)
            {
                case TargetMorale.Low:
                    result += "Low";
                    break;
                case TargetMorale.Medium:
                    result += "Medium";
                    break;
                case TargetMorale.High:
                    result += "High";
                    break;
                default:
                    break;
            }
            result += _separator;

            return result;
        }
        private static string GetText(WeaponTraining info)
        {
            string result = "";
            switch (info)
            {
                case WeaponTraining.None:
                    result += "None";
                    break;
                case WeaponTraining.Selfdefence:
                    result += "Selfdefence";
                    break;
                case WeaponTraining.Military:
                    result += "Military";
                    break;
                case WeaponTraining.Veteran:
                    result += "Veteran";
                    break;
                case WeaponTraining.Elite:
                    result += "Elite";
                    break;
                default:
                    break;
            }
            result += _separator;

            return result;
        }
        
        private static string GetText(ArmorType info)
        {
            string result = "";
            switch (info)
            {
                case ArmorType.None:
                    result += "None";
                    break;
                case ArmorType.Light:
                    result += "Light";
                    break;
                case ArmorType.Medium:
                    result += "Medium";
                    break;
                case ArmorType.Heavy:
                    result += "Heavy";
                    break;
                default:
                    break;
            }
            result += _separator;

            return result;
        }
        
        private static string GetText(DestinationSetting info)
        {
            string result = "";
            switch (info)
            {
                case DestinationSetting.InsideCIty:
                    result += "InsideCIty";
                    break;
                case DestinationSetting.OutsideCity:
                    result += "OutsideCity";
                    break;
                case DestinationSetting.Everywhere:
                    result += "Everywhere";
                    break;
                default:
                    break;
            }
            result += _separator;

            return result;
        }


        #endregion Private Methods

    }
}
