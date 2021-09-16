////////////////////////////////////////////////////////////////////////////////
//
//	MG_AssassinationMission.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;

//http://forum.ragezone.com/f978/grand-theft-auto-modding-guide-1100784/

namespace MG_Liquidator
{
    public class MG_AssassinationMission : Script
    {
        #region Fields       
        private static readonly BlipSprite blipSpriteTarget = BlipSprite.ExecutiveSearch;
        private static readonly BlipColor blipColorTarget = BlipColor.Red;
        #endregion Fields

        #region Properties
        public static Vector3 PositionWhereTargetCanBeFound { get; set; }
        public static bool IsJobActive { get; set; } = false;
        public static Blip RoutineBlip { get; set; }
        public static bool IsDeadOnActiveMissionActivated { get; set; } = false;
        public static DestinationSetting DestinationSetting { get; set; }
        #endregion Properties

        #region Public Methods

        public static void StartJob()
        {
            MG_Player.Player = Game.Player;
            MG_Player.Ped = Game.Player.Character;

            MG_MissionPreset.GeneratePreset();
            Ped playerPed = MG_Player.Ped;
            MG_Player.IsUsingCellphone = true;
            Prop propPhone = null;
            if (MG_iFruit.IsUsing == false)
            {

                MG_Ped.PlayCellphoneAnim(playerPed);
                MG_Ped.HideWeapon(playerPed);
                propPhone = MG_Ped.CreateAndAttachPhone(playerPed);

                //Wait(1000);
                //GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Assasin_Main.PlayerPed, "GENERIC_HI", "SPEECH_PARAMS_FORCE");
                Wait(500);
                MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                Wait(500);

                //---_AutoCall
                MG_Audio.Play(MG_Advisor._AutoCall, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                Wait(300);
                //---_AutoCall
                while (MG_Audio.IsPlaying(0) == true) Wait(100);

                if (!playerPed.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(playerPed, propPhone);
                    return;
                }
            }
            PositionWhereTargetCanBeFound = MG_Map.GetRandomMapPos(DestinationSetting);
            string streetName = MG_Map.GetStreetName(PositionWhereTargetCanBeFound);

            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: You need to get to ~o~" + streetName + "~w~. I call you back when you are there.", 6500);

            //---_GetToThatLocation1
            MG_Audio.Play(MG_Advisor._GetToThatLocation1, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(1000);
            //---_GetToThatLocation1
            while (MG_Audio.IsPlaying(0) == true) Wait(100);

            //---_GetToThatLocation2
            MG_Audio.Play(MG_Advisor._GetToThatLocation2, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(300);
            //---_GetToThatLocation2
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            Wait(1000);

            if (MG_iFruit.IsUsing == false)
            {
                if (!playerPed.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(playerPed, propPhone);
                    return;
                }

                MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);

            }
            MG_Message.SMS("GPS Coordinates of location ~o~" + streetName + "~w~ recieved!");

            RoutineBlip = MG_Blip.CreateBlip(PositionWhereTargetCanBeFound, true, blipSpriteTarget, blipColorTarget);

            if (MG_iFruit.IsUsing == false)
            {
                MG_Ped.DisableCellphoneAnim(playerPed);
                Wait(450);
                if (!playerPed.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(playerPed, propPhone);
                    return;
                }
                propPhone.Detach();
                propPhone.Delete();

                MG_Ped.ShowWeapon(playerPed);
            }
            IsJobActive = true;
            MG_Player.IsUsingCellphone = false;


        }

        public static void CancelJob()
        {
            MG_Hitman.CanceledMissionsCount++;
            if (MG_Hitman.CanceledMissionsCount > 3)
            {
                MG_Hitman.MadeMad = true;
                MG_Main.FullResetAfterEndMission();
                MG_Hitman.AngryTalk();
                return;
            }

            //http://forum.ragezone.com/f978/grand-theft-auto-modding-guide-1100784/

            MG_Player.IsUsingCellphone = true;
            Ped _character = MG_Player.Ped;

            Prop _propPhone = null;
            if (MG_iFruit.IsUsing == false)
            {
                MG_Ped.PlayCellphoneAnim(_character);
                MG_Ped.HideWeapon(_character);
                _propPhone = MG_Ped.CreateAndAttachPhone(_character);

                Wait(500);
                MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                Wait(500);

                //---_AutoCall
                MG_Audio.Play(MG_Advisor._AutoCall, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                Wait(300);
                //---_AutoCall
                while (MG_Audio.IsPlaying(0) == true) Wait(100);

                if (!_character.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(_character, _propPhone);
                    return;
                }
            }

            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: The mission rejected by your request.", 6000);
            //---_MissionRejected
            MG_Audio.Play(MG_Advisor._MissionRejected, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(300);
            //---_MissionRejected
            while (MG_Audio.IsPlaying(0) == true) Wait(100);



            if (MG_Hitman.CanceledMissionsCount > 1)
            {
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
                MG_Advisor.PlayUseless();
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
            }


            Wait(1000);

            if (MG_iFruit.IsUsing == false)
            {
                if (!_character.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(_character, _propPhone);
                    return;
                }
            }

            if (MG_Hitman.CanceledMissionsCount > 2)
            {
                GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_INSULT_HIGH", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");

            }
            else
            {
                GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_THANKS", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");
            }

            Wait(1500);

            if (MG_iFruit.IsUsing == false)
            {

                MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                Wait(500);


                if (!_character.IsAlive)
                {
                    MG_Ped.BreakPhoneCall(_character, _propPhone);
                    return;
                }
                MG_Ped.DisableCellphoneAnim(_character);
                Wait(450);

                _propPhone.Detach();
                _propPhone.Delete();

                MG_Ped.ShowWeapon(_character);
            }
            MG_Main.FullResetAfterEndMission();
            MG_Player.IsUsingCellphone = false;//new
        }

        public static void NotAvailableAnymore()
        {

            MG_Player.IsUsingCellphone = true;
            Ped _character = MG_Player.Ped;
            MG_Ped.PlayCellphoneAnim(_character);
            MG_Ped.HideWeapon(_character);
            var _propPhone = MG_Ped.CreateAndAttachPhone(_character);

            Wait(500);
            MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(500);

            //---_AutoCall
            MG_Audio.Play(MG_Advisor._AutoCall, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(300);
            //---_AutoCall
            while (MG_Audio.IsPlaying(0) == true) Wait(100);

            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, _propPhone);
                return;
            }

            MG_Audio.Play(MG_Advisor._NotAvailable, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);


            Wait(1000);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, _propPhone);
                return;
            }
            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");

            Wait(1500);
            MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(500);


            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, _propPhone);
                return;
            }
            MG_Ped.DisableCellphoneAnim(_character);
            Wait(450);

            _propPhone.Detach();
            _propPhone.Delete();

            MG_Ped.ShowWeapon(_character);
            MG_Player.IsUsingCellphone = false;
        }

        public static void Defeat()
        {
            MG_Hitman.CanceledMissionsCount = 0;
            IsDeadOnActiveMissionActivated = true;
            MG_Statistic.TotalFailedMissions += 1;
        }

        public static void ShowDefeatMessage()
        {
            Wait(3000);
            IsDeadOnActiveMissionActivated = false;
            //MG_Message.SubTitle("~r~You failed your mission. ~w~Total defeats: ~o~" + MG_Statistic.TotalFailedMissions + "~w~.", 5000);


            MG_Message.SubTitle("~r~Your mission has been canceled.", 5000);

            if (MG_Advisor.DisableMessageReceivedVoice == false)
            {
                MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                while (MG_Audio.IsPlaying(1) == false) Wait(100);
                while (MG_Audio.IsPlaying(1) == true) Wait(100);
            }

            //---_YourMissionHasBeenCanceled
            MG_Audio.Play(MG_Advisor._YourMissionHasBeenCanceled, 0, false);

            if (MG_Random.Random() <= 10)
            {
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
                MG_Advisor.PlayUseless();
            }
            //---_YourMissionHasBeenCanceled

            MG_Main.FullResetAfterEndMission();
        }

        public static void Reset()
        {
            IsJobActive = false;
            MG_Player.IsUsingCellphone = false;

            if (RoutineBlip != null)
            {
                MG_Blip.RemoveBlip(RoutineBlip);
            }

        }
        #endregion Public Methods


    }
}