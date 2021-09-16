////////////////////////////////////////////////////////////////////////////////
//
//	MG_Advisor.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    public class MG_Advisor : Script
    {

        #region Fields

        #endregion Fields

        #region Properties
        public static bool DisableAdvisorVoice { get; set; } = false;
        public static bool DisableMessageReceivedVoice { get; set; } = false;

        private static readonly string _emptyVoiceLines = @"scripts\HW_LIQUIDATOR2021\SOUND\Empty.mp3";

        private static string _Hello1 { get; set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Hello1.mp3";
        private static string _Hello2 { get; set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Hello2.mp3";
        private static string _Hello3 { get; set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Hello3.mp3";
        private static string _Hello4 { get; set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Hello4.mp3";
        private static string _Hello5 { get; set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Hello5.mp3";

        public static string _PhoneCall { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\PhoneCall.mp3";
        public static string _ButtonPressed { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\ButtonPressed.mp3";
        public static string _AutoCall { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\AutoCall.mp3";

        public static string _Angry1 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry1.mp3";
        public static string _Angry2 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry2.mp3";
        public static string _Angry3 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry3.mp3";
        public static string _Angry4 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry4.mp3";
        public static string _Angry5 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry5.mp3";
        public static string _Angry6 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry6_B.mp3";
        public static string _Angry7 { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Angry7.mp3";

        public static string _MessageReceived { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MessageReceived.mp3";
        public static string _MissionRejected { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MissionRejected.mp3";
        public static string _YourMissionHasBeenCanceled { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\YourMissionHasBeenCanceled.mp3";


        public static string _GetToThatLocation1 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\GetToThatLocation1.mp3";
        public static string _GetToThatLocation2 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\GetToThatLocation2.mp3";

        public static string _PleaseKeepCheckingTheArea { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\PleaseKeepCheckingTheArea.mp3";
        public static string _StopTheVehicle { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\StopTheVehicle.mp3";

        public static string _TargetFemaleInVehicle { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\TargetFemaleInVehicle.mp3";

        public static string _TargetIsFelame { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\TargetIsFelame.mp3";
        public static string _TargetIsMale { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\TargetIsMale.mp3";
        public static string _TargetIsWoman { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\YourTargetIsWoman.mp3";
        public static string _TargetIsMan { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\YourTargetIsMan.mp3";

        public static string _TargetMaleInVehicle { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\TargetMaleInVehicle.mp3";

        public static string _Useless1 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Useless1.mp3";
        public static string _Useless2 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Useless2.mp3";
        public static string _Useless3 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Useless3.mp3";
        public static string _Useless4 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Useless4.mp3";
        public static string _Useless5 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\Useless5.mp3";

        public static string _BeAdvised1 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\BeAdvised1.mp3";
        public static string _BeAdvised2 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\BeAdvised2.mp3";
        public static string _BeAdvised3 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\BeAdvised3.mp3";
        public static string _BeAdvised4 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\BeAdvised4.mp3";
        public static string _BeAdvised5 { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\BeAdvised5.mp3";

        public static string _FemaleAssassin { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleAssassin.mp3";
        public static string _FemaleCartel { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleCartel.mp3";
        public static string _FemaleGang { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleGang.mp3";
        public static string _FemaleHacker { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleHacker.mp3";
        public static string _FemaleMilitary { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleMilitary.mp3";
        public static string _FemalePolice { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemalePolice.mp3";
        public static string _FemaleTerrorist { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\FemaleTerrorist.mp3";

        public static string _MaleAssassin { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleAssassin.mp3";
        public static string _MaleCartel { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleCartel.mp3";
        public static string _MaleGang { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleGang.mp3";
        public static string _MaleHacker { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleHacker.mp3";
        public static string _MaleMilitary { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleMilitary.mp3";
        public static string _MalePolice { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MalePolice.mp3";
        public static string _MaleTerrorist { get; private set; } = @"scripts\HW_LIQUIDATOR2021\SOUND\MaleTerrorist.mp3";

        public static string _Horror { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\HorrorMusic.mp3";
        public static string _NotAvailable { get; } = @"scripts\HW_LIQUIDATOR2021\SOUND\NotAvailable.mp3";

        public static bool SkipTutorial { get; set; } = false;
        public static bool PleaseKeepCheckingNeedToSay { get; set; } = true;
        public static bool StopTheVehicleNeedToSay { get; set; } = true;
        #endregion Properties

        #region Public Methods
        public static void Init()
        {
            if (DisableAdvisorVoice)
            {
                DisableAdvisorVoiceLines();
            }
            if (DisableMessageReceivedVoice)
            {
                DisableMessageReceived();
            }
        }

        public static void StartTutorial()
        {
            SkipTutorial = true;
            MG_Player.IsUsingCellphone = true;
            Wait(5000);

            MG_Audio.Play(_PhoneCall, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(2500);

            Ped playerPed = MG_Player.Ped;
            MG_Ped.PlayCellphoneAnim(playerPed);
            MG_Ped.HideWeapon(playerPed);
            var propPhone = MG_Ped.CreateAndAttachPhone(playerPed);

            Wait(500);
            MG_Audio.Play(_ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(1000);


            //---_Hello1
            MG_Audio.Play(_Hello1, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: Hello, contractor. If you want to start the first assassination mission, please use your Numpad buttons and press 'Num one' (by default).", 15000);
            //---_Hello1

            Wait(1000);

            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            Wait(1000);

            //---_Hello2
            MG_Audio.Play(_Hello2, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: If your mission is active, you also can cancel it. Just press the same button during your active mission.", 15000);
            //---_Hello2

            Wait(1000);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            Wait(1000);

            //---_Hello3
            MG_Audio.Play(_Hello3, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: But don't do it often. It will be ~r~very unprofessional~w~.", 15000);
            //---_Hello3

            Wait(1000);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            Wait(1000);

            ////---_Hello4
            //MG_Audio.Play(_Hello4, 0, false);
            //while (MG_Audio.IsPlaying(0) == false) Wait(10);
            //Wait(300);
            //MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: Other than that, there is one person here who is ready to beat the shit out of you for doing that.", 15000);
            ////---_Hello4

            //Wait(1000);
            //while (MG_Audio.IsPlaying(0) == true) Wait(10);
            //Wait(1000);

            //---_Hello5
            MG_Audio.Play(_Hello5, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: You got it? So, good luck.", 6000);
            //---_Hello5

            Wait(1000);

            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            MG_Audio.Play(_ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(1000);

            MG_Ped.DisableCellphoneAnim(playerPed);
            //Wait(3000);
            if (!playerPed.IsAlive)
            {
                MG_Ped.BreakPhoneCall(playerPed, propPhone);
                return;
            }
            propPhone.Detach();
            propPhone.Delete();

            MG_Ped.ShowWeapon(playerPed);
            MG_Player.IsUsingCellphone = false;
        }

        public static void PlayUseless()
        {
            string[] array = new string[] { _Useless1, _Useless2, _Useless3, _Useless4, _Useless5 };
            MG_Audio.Play(MG_Random.RandomElement(array), 0, false);
        }

        public static string GetTargetTypeVoice()
        {
            TargetType type = MG_Target.Type;
            Gender gender = MG_Target.Gender;

            switch (gender)
            {
                case Gender.Male:
                    switch (type)
                    {
                        //case TargetType.Normal:
                        //    break;
                        case TargetType.Assasin:
                            return _MaleAssassin;
                        case TargetType.Terrorist:
                            return _MaleTerrorist;
                        case TargetType.Police:
                            return _MalePolice;
                        case TargetType.Cartel:
                            return _MaleCartel;
                        case TargetType.Hacker:
                            return _MaleHacker;
                        case TargetType.Military:
                            return _MaleMilitary;
                        case TargetType.GangMember:
                            return _MaleGang;
                        default:
                            return _MaleGang;

                    }

                case Gender.Female:
                    switch (type)
                    {
                        //case TargetType.Normal:
                        //    break;
                        case TargetType.Assasin:
                            return _FemaleAssassin;
                        case TargetType.Terrorist:
                            return _FemaleTerrorist;
                        case TargetType.Police:
                            return _FemalePolice;
                        case TargetType.Cartel:
                            return _FemaleCartel;
                        case TargetType.Hacker:
                            return _FemaleHacker;
                        case TargetType.Military:
                            return _FemaleMilitary;
                        case TargetType.GangMember:
                            return _FemaleGang;
                        default:
                            return _FemaleGang;
                    }
                default:
                    return _MaleGang;
            }


        }

        public static string GetTargetTypeVoiceText()
        {
            TargetType type = MG_Target.Type;
            Gender gender = MG_Target.Gender;

            switch (gender)
            {
                case Gender.Male:
                    switch (type)
                    {
                        //case TargetType.Normal:
                        //    break;
                        case TargetType.Assasin:
                            return "He is an ~r~assassin~w~.";
                        case TargetType.Terrorist:
                            return "He works for a ~r~terrorist organization~w~.";
                        case TargetType.Police:
                            return "He works for ~r~the police~w~.";
                        case TargetType.Cartel:
                            return "He works for a ~r~drug cartel~w~.";
                        case TargetType.Hacker:
                            return "He is a ~r~cyber criminal~w~.";
                        case TargetType.Military:
                            return "He works for the ~r~military~w~.";
                        case TargetType.GangMember:
                            return "He is a ~r~gang member~w~.";
                        default:
                            return "~r~(ERROR MALE)~w~";

                    }

                case Gender.Female:
                    switch (type)
                    {
                        //case TargetType.Normal:
                        //    break;
                        case TargetType.Assasin:
                            return "She is an ~r~assassin~w~.";
                        case TargetType.Terrorist:
                            return "She works for a ~r~terrorist organization~w~.";
                        case TargetType.Police:
                            return "She works for ~r~the police~w~.";
                        case TargetType.Cartel:
                            return "She works for a ~r~drug cartel~w~.";
                        case TargetType.Hacker:
                            return "She is a ~r~cyber criminal~w~.";
                        case TargetType.Military:
                            return "She works for the ~r~military~w~.";
                        case TargetType.GangMember:
                            return "She is a ~r~gang member~w~.";
                        default:
                            return "~r~(ERROR FEMALE)~w~";
                    }
                default:
                    return "~r~(ERROR GENDER)~w~";
            }


        }

        public static string GetBeAdvicedVoice(int num)
        {
            switch (num)
            {
                //case TargetType.Normal:
                //    break;
                case 1:
                    return _BeAdvised1;
                case 2:
                    return _BeAdvised2;
                case 3:
                    return _BeAdvised3;
                case 4:
                    return _BeAdvised4;
                case 5:
                    return _BeAdvised5;
                default:
                    return _BeAdvised1;

            }
        }

        public static string GetBeAdvicedVoiceText(int num)
        {
            switch (num)
            {
                //case TargetType.Normal:
                //    break;
                case 1:
                    return "Be advised! ";
                case 2:
                    return "Be aware! ";
                case 3:
                    return "Stay alert! ";
                case 4:
                    return "Listen carefully! ";
                case 5:
                    return "Listen to me carefully! ";
                default:
                    return "";

            }
        }

        public static string TargetGenderVoice(bool genderName)
        {
            Gender gender = MG_Target.Gender;

            if (genderName)
            {
                switch (gender)
                {
                    case Gender.Male:
                        return _TargetIsMale;
                    case Gender.Female:
                        return _TargetIsFelame;
                    default:
                        return _TargetIsMale;
                }
            }
            else
            {
                switch (gender)
                {
                    case Gender.Male:
                        return _TargetIsMan;
                    case Gender.Female:
                        return _TargetIsWoman;
                    default:
                        return _TargetIsMan;
                }
            }

        }

        public static string TargetGenderInVehicleVoice()
        {
            Gender gender = MG_Target.Gender;

            switch (gender)
            {
                case Gender.Male:
                    return _TargetMaleInVehicle;
                case Gender.Female:
                    return _TargetFemaleInVehicle;
                default:
                    return _TargetMaleInVehicle;
            }
        }

        #endregion Public Methods

        #region Private Methods
        private static void DisableAdvisorVoiceLines()
        {
            _Hello1 = _emptyVoiceLines;
            _Hello2 = _emptyVoiceLines;
            _Hello3 = _emptyVoiceLines;
            _Hello4 = _emptyVoiceLines;
            _Hello5 = _emptyVoiceLines;
            _MissionRejected = _emptyVoiceLines;
            _YourMissionHasBeenCanceled = _emptyVoiceLines;
            _GetToThatLocation1 = _emptyVoiceLines;
            _GetToThatLocation2 = _emptyVoiceLines;
            _PleaseKeepCheckingTheArea = _emptyVoiceLines;
            _StopTheVehicle = _emptyVoiceLines;
            _TargetFemaleInVehicle = _emptyVoiceLines;
            _TargetIsFelame = _emptyVoiceLines;
            _TargetIsMale = _emptyVoiceLines;
            _TargetIsWoman = _emptyVoiceLines;
            _TargetIsMan = _emptyVoiceLines;
            _TargetMaleInVehicle = _emptyVoiceLines;
            _Useless1 = _emptyVoiceLines;
            _Useless2 = _emptyVoiceLines;
            _Useless3 = _emptyVoiceLines;
            _Useless4 = _emptyVoiceLines;
            _Useless5 = _emptyVoiceLines;
            _BeAdvised1 = _emptyVoiceLines;
            _BeAdvised2 = _emptyVoiceLines;
            _BeAdvised3 = _emptyVoiceLines;
            _BeAdvised4 = _emptyVoiceLines;
            _BeAdvised5 = _emptyVoiceLines;
            _FemaleAssassin = _emptyVoiceLines;
            _FemaleCartel = _emptyVoiceLines;
            _FemaleGang = _emptyVoiceLines;
            _FemaleHacker = _emptyVoiceLines;
            _FemaleMilitary = _emptyVoiceLines;
            _FemalePolice = _emptyVoiceLines;
            _FemaleTerrorist = _emptyVoiceLines;
            _MaleAssassin = _emptyVoiceLines;
            _MaleCartel = _emptyVoiceLines;
            _MaleGang = _emptyVoiceLines;
            _MaleHacker = _emptyVoiceLines;
            _MaleMilitary = _emptyVoiceLines;
            _MalePolice = _emptyVoiceLines;
            _MaleTerrorist = _emptyVoiceLines;
        }
        private static void DisableMessageReceived()
        {
            _MessageReceived = _emptyVoiceLines;
        }
        #endregion Private Methods
    }
}
