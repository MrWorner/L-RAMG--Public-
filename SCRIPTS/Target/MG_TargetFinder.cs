////////////////////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetFinder.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using System.Collections.Generic;


namespace MG_Liquidator
{
    public class MG_TargetFinder : Script
    {
        //https://alexguirre.github.io/animations-list/

        #region Fields
        private static int _tryingToFindTargetTimes = 0;//для того чтобы проигнорить "В машине или без машины искать цель", если не удается найти цель для легкого поиска.
        #endregion Fields

        #region Properties    
        public static bool IsPlayerOnPosition { get; set; } = false;
        public static bool IsNeedToStopTheCarToStartSearch { get; set; } = false;
        public static bool IsTargetFound { get; set; } = false;
        public static bool HasToBeInVehicle { get; set; }// = MG_Random.RandomB();//PRESET!
        #endregion Properties

        #region Public Methods     

        public static void CheckRoutine()
        {
            //float distance = World.GetDistance(MG_Assasin_Main.PlayerPed.Position, MG_Assasin_Main.PositionWhereTargetCanBeFound);
            Ped playerPed = Game.Player.Character;
            //Vehicle vehicle = playerPed.CurrentVehicle;
            //Vector3 playerPos;
            //Blip marker = MG_AssassinationMission.RoutineBlip;
            // Vector3 markerPos = marker.Position;
            //if (vehicle == null)
            //{
            //playerPos = playerPed.Position;
            //}
            //else
            // {
            //    playerPos = vehicle.Position;
            //}

            var playerPos = playerPed.Position;
            int distance = (int)Vector2.Distance(playerPos, MG_AssassinationMission.PositionWhereTargetCanBeFound);//OLD VARIANT

            float x = MG_AssassinationMission.PositionWhereTargetCanBeFound.X;
            float y = MG_AssassinationMission.PositionWhereTargetCanBeFound.Y;
            Vector3 position0_fix = new Vector3(x, y, 0);
            Vector3 position1_fix = new Vector3(x, y, 65);
            Vector3 position2_fix = new Vector3(x, y, 165);
            Vector3 position3_fix = new Vector3(x, y, 265);
            Vector3 position4_fix = new Vector3(x, y, 365);
            int distance0_fix = (int) World.GetDistance(playerPos, position0_fix);
            int distance1_fix = (int) World.GetDistance(playerPos, position1_fix);
            int distance2_fix = (int) World.GetDistance(playerPos, position2_fix);
            int distance3_fix = (int) World.GetDistance(playerPos, position3_fix);
            int distance4_fix = (int) World.GetDistance(playerPos, position4_fix);
            bool isOnPosition0_fix = distance0_fix < 100;
            bool isOnPosition1_fix = distance1_fix < 100;
            bool isOnPosition2_fix = distance2_fix < 100;
            bool isOnPosition3_fix = distance3_fix < 100;
            bool isOnPosition4_fix = distance4_fix < 100;
            //float distance2 = World.GetDistance(playerPed.Position, MG_AssassinationMission.PositionWhereTargetCanBeFound);//OLD VARIANT
            //float distance = World.CalculateTravelDistance(playerPos, MG_AssassinationMission.PositionWhereTargetCanBeFound);
            //float distance = World.GetDistance(playerPos, markerPos);
            //MG_Message.SubTitle("Vector2.Distance = " + distance + " VS World.GetDistance = " + distance2);

            if (MG_Test.DEBUG)
            {
                string text = "Distance= " + ((int)distance) + " | Is on position= " + ((int)distance < 50 || IsNeedToStopTheCarToStartSearch);
                text += " distance0_fix= " + distance0_fix;
                text += " distance1_fix= " + distance1_fix;
                text += " distance2_fix= " + distance2_fix;
                text += " distance3_fix= " + distance3_fix;
                text += " distance4_fix= " + distance4_fix;
                text += " 0= " + isOnPosition0_fix;
                text += " 1= " + isOnPosition1_fix;
                text += " 2= " + isOnPosition2_fix;
                text += " 3= " + isOnPosition3_fix;
                text += " 4= " + isOnPosition4_fix;
                MG_Message.SubTitle(text, 3000);
            }
            if (distance < 50 || IsNeedToStopTheCarToStartSearch || isOnPosition0_fix || isOnPosition1_fix || isOnPosition2_fix || isOnPosition3_fix || isOnPosition4_fix)
            {
                Vehicle vehicle = playerPed.CurrentVehicle;
                if (vehicle != null)
                {
                    if (vehicle.Speed > 12f)
                    {
                        if (MG_Advisor.StopTheVehicleNeedToSay)
                        {
                            MG_Advisor.StopTheVehicleNeedToSay = false;

                            MG_Message.SubTitle("~w~Stop the vehicle~w~");

                            if (MG_Advisor.DisableMessageReceivedVoice == false)
                            {
                                MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                                while (MG_Audio.IsPlaying(1) == false) Wait(100);
                                while (MG_Audio.IsPlaying(1) == true) Wait(100);
                            }
        
                            //---_StopTheVehicle
                            MG_Audio.Play(MG_Advisor._StopTheVehicle, 1, false);
                            //---_StopTheVehicle
                        }



                        MG_Message.SubTitle("~w~Stop the vehicle~w~");
                        if (!IsNeedToStopTheCarToStartSearch)
                        {
                            IsNeedToStopTheCarToStartSearch = true;
                        }
                    }
                    else
                    {
                        if (_tryingToFindTargetTimes > 0) _tryingToFindTargetTimes = 0;

                        IsPlayerOnPosition = true;
                        if (IsNeedToStopTheCarToStartSearch)
                        {
                            IsNeedToStopTheCarToStartSearch = false;
                        }
                        MG_Blip.RemoveBlip(MG_AssassinationMission.RoutineBlip);
                    }
                }
                else
                {
                    if (_tryingToFindTargetTimes > 0) _tryingToFindTargetTimes = 0;
                    IsPlayerOnPosition = true;
                    if (IsNeedToStopTheCarToStartSearch)
                    {
                        IsNeedToStopTheCarToStartSearch = false;
                    }
                    MG_Blip.RemoveBlip(MG_AssassinationMission.RoutineBlip);
                }
            }
            //else
            //{
            // UI.ShowSubtitle("test 5 _distance=" + _distance + " " + MG_Main.isNeetToStopTheCarToStartSearch);
            //}
        }

        public static void FindTheTarget()
        {
            Ped playerPed = MG_Player.Ped;
            Ped[] peds = World.GetNearbyPeds(playerPed, 1000);//1000
            List<Ped> candidates = new List<Ped>();
            for (int i = 0; i < peds.Length; i++)
            {
                Ped ped = peds[i];
                Vector3 pedPos = ped.Position;
                if
                 (
                 ped.IsHuman
                 && ped.Exists()
                 && ped.IsAlive
                 && !ped.IsRagdoll
                 && !ped.IsFalling
                 //&& !_ped.IsInjured
                 //&& !_ped.IsInCombat
                 //&& !_ped.IsFleeing
                 //---------&& ped.IsInVehicle()//TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                 && ((ped.IsInVehicle() && HasToBeInVehicle) || (!ped.IsInVehicle() && !HasToBeInVehicle) || (_tryingToFindTargetTimes > 35))
                 && !ped.IsInTrain
                 && !ped.IsInHeli
                 && !ped.IsInPlane
                 && !ped.IsInBoat
                 && !ped.IsInSub//NEW
                                //&& ped.Gender == Gender.Female///DEL!!!!!!!!!!!!!!!
                 && !ped.IsInParachuteFreeFall//NEW
                 && !ped.IsInPoliceVehicle//NEW
                 && !ped.IsOccluded//ПОХОЖЕ ИСПРАВЛЯЕТ БАГ!!!!!!!!!!!!!!!!
                                   //&& ped.IsIdle//NEW
                                   //&& ped.IsOnScreen//NEW
                 && World.GetDistance(playerPed.Position, pedPos) > 50//45
                 && World.GetDistance(playerPed.Position, pedPos) < 1000//1000
                 )
                {
                    candidates.Add(ped);

                }
            }

            if (candidates.Count > 0)
            {
                //Ped target = MG_Random.RandomElement(candidates);
                Ped target = GetTheFarestTarget(candidates);
                MG_Target.InitTarget(target);
                Wait(50);
                MG_TargetBodyGuards.SpawnBodyGuards();
                Wait(50);
                MG_Watchers.SpawnBodyWatchers();
                ReceiveTargetInfo(target);
                IsTargetFound = true;
            }
            else
            {
                if (MG_Advisor.PleaseKeepCheckingNeedToSay)
                {
                    MG_Advisor.PleaseKeepCheckingNeedToSay = false;

                    if (MG_Advisor.DisableMessageReceivedVoice == false)
                    {
                        MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                        while (MG_Audio.IsPlaying(1) == false) Wait(100);
                        while (MG_Audio.IsPlaying(1) == true) Wait(100);
                    }
                    //---_PleaseKeepCheckingTheArea
                    MG_Audio.Play(MG_Advisor._PleaseKeepCheckingTheArea, 2, false);
                    //---_PleaseKeepCheckingTheArea
                }

                MG_Message.SubTitle("~o~Please, keep checking the area~w~ ");
                Wait(250);
                MG_Message.SubTitle("~r~Please, keep checking the area~w~ ");
                Wait(250);
                _tryingToFindTargetTimes++;
            }
        }

        public static void Reset()
        {
            IsPlayerOnPosition = false;
            IsNeedToStopTheCarToStartSearch = false;
            IsTargetFound = false;
            MG_Advisor.PleaseKeepCheckingNeedToSay = true;
            MG_Advisor.StopTheVehicleNeedToSay = true;
        }
        #endregion Public Methods

        #region Private Methods        

        private static void ReceiveTargetInfo(Ped target)
        {
            Gender gender = target.Gender;
            bool isTargetInVehicle = MG_Ped.IsInVehicle(target);
            Ped playerPed = MG_Player.Ped;

            string genderStr;
            string genderHeOrSheStr;
            bool targetByGender = (MG_Random.Random() > 89) ? true : false;
            if (targetByGender)
            {
                if (gender.Equals(Gender.Male))
                {
                    genderStr = "~o~male~w~";
                    genderHeOrSheStr = "he";
                }
                else
                {
                    genderStr = "~o~female~w~";
                    genderHeOrSheStr = "she";
                }
            }
            else
            {
                if (gender.Equals(Gender.Male))
                {
                    genderStr = "~o~man~w~";
                    genderHeOrSheStr = "he";
                }
                else
                {
                    genderStr = "~o~woman~w~";
                    genderHeOrSheStr = "she";
                }
            }

            string AddInfo_vehicle = "";
            if (isTargetInVehicle)
            {
                Vehicle vehicle = target.CurrentVehicle;
                //string vehicleName = vehicle.DisplayName;
                string vehicleName = vehicle.FriendlyName;
                VehicleColor colorVehicle = vehicle.PrimaryColor;
                AddInfo_vehicle = " and " + genderHeOrSheStr + " is in a vehicle ~o~" + vehicleName + "~w~"; //+ "~w~, color: ~o~" + colorVehicle + "~w~";
            }

            if (!playerPed.IsAlive)
            {
                //MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            MG_Player.IsUsingCellphone = true;

            MG_Audio.Play(MG_Advisor._PhoneCall, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);

            Wait(2500);
            if (!playerPed.IsAlive)
            {
                return;
            }

            Prop propPhone = MG_Ped.CreateAndAttachPhone(playerPed);
            MG_Ped.PlayCellphoneAnim(playerPed);
            MG_Ped.HideWeapon(playerPed);

            Wait(500);
            MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);

            Wait(1000);
            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            int messageDuration = 6000;
            string targetGenderVoice = MG_Advisor.TargetGenderVoice(targetByGender);
            string extraAdviceVoiceChosen = "";
            string extraAdviceVoiceTextChosen = "";
            string targetTypeVoice = "";
            string targetTypeVoiceTEXT = "";

            string finalText = "~g~" + MG_Settings.INI_AdvisorName + "~w~: Your ~r~target~w~ is a " + genderStr + AddInfo_vehicle + ".";
            if (MG_Target.Type.Equals(TargetType.Normal) == false)
            {
                messageDuration = 9000;
                int randomNum = MG_Random.Random(1, 6);

                targetTypeVoice = MG_Advisor.GetTargetTypeVoice();
                targetTypeVoiceTEXT = MG_Advisor.GetTargetTypeVoiceText();

                extraAdviceVoiceChosen = MG_Advisor.GetBeAdvicedVoice(randomNum);
                extraAdviceVoiceTextChosen = MG_Advisor.GetBeAdvicedVoiceText(randomNum);

                finalText += " " + extraAdviceVoiceTextChosen + targetTypeVoiceTEXT;

            }

            MG_Message.SubTitle(finalText, messageDuration);

            while (MG_Audio.IsPlaying(0) == true) Wait(100);

            MG_Audio.Play(targetGenderVoice, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);

            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            if (isTargetInVehicle)
            {
                string inVehicleVoice = MG_Advisor.TargetGenderInVehicleVoice();
                MG_Audio.Play(inVehicleVoice, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
            };

            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            if (MG_Target.Type.Equals(TargetType.Normal) == false)
            {
                MG_Audio.Play(extraAdviceVoiceChosen, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
                MG_Audio.Play(targetTypeVoice, 0, false);
                while (MG_Audio.IsPlaying(0) == false) Wait(100);
                while (MG_Audio.IsPlaying(0) == true) Wait(100);
            }

            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_THANKS", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");
            Wait(1000);

            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(500);

            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }

            MG_Ped.DisableCellphoneAnim(playerPed);
            Wait(500);

            propPhone.Detach();
            propPhone.Delete();
            MG_Ped.ShowWeapon(playerPed);



            if (MG_Target.Type.Equals(TargetType.Assasin) || MG_Target.Type.Equals(TargetType.Hacker) || MG_Target.Type.Equals(TargetType.Terrorist))
            {
                //SKIP
            }
            else
            {
                MG_TargetBodyGuards.CreateBlips();
                MG_Target.CreateBlips();
            }
        }

        private static Ped GetTheFarestTarget(List<Ped> candidates)
        {
            Ped chosen = null;
            Ped player = MG_Player.Ped;
            float farDistance = 0f;
            foreach (var ped in candidates)
            {
                float distance = World.GetDistance(player.Position, ped.Position);
                if (distance > farDistance)
                {
                    chosen = ped;
                    farDistance = distance;
                }
            }
            return chosen;
        }
        #endregion Private Methods

        //---TODO BALACLAVA

        //GTA.Native.Function.Call(GTA.Native.Hash.CLEAR_PLAYER_HAS_DAMAGED_AT_LEAST_ONE_PED, MG_Assasin_Main.PlayerPed);

    }
}
