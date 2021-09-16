////////////////////////////////////////////////////////////////////////////////
//
//	MG_Audio.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WMPLib;

namespace MG_Liquidator
{
    public class MG_Audio : Script
    {
        private static System.Media.SoundPlayer MUSICdb = new System.Media.SoundPlayer();
        private static int _waitingTime = 0;
        private static int _maximum_attempts = 40;
        private static readonly int _MAXIMUM_ATTEMPTS_Number = 40;

        public static string _file_wav = ".\\Scripts\\HW_MUSIC\\XCOM_FAIL.wav";
        public static string _file_ogg = ".\\Scripts\\HW_MUSIC\\15_Dunes_Peace.ogg";
        public static string _file_mp3 = ".\\Scripts\\HW_MUSIC\\Robocop.mp3";

        public static WMPLib.WindowsMediaPlayer _wplayer = new WMPLib.WindowsMediaPlayer();
        public static WMPLib.WindowsMediaPlayer _wplayer2 = new WMPLib.WindowsMediaPlayer();
        public static WMPLib.WindowsMediaPlayer _wplayer3 = new WMPLib.WindowsMediaPlayer();
        public static WMPLib.WindowsMediaPlayer _wplayer4 = new WMPLib.WindowsMediaPlayer();

        public static void Play(string file, int channelNum, bool bLoop)
        {
            WMPLib.WindowsMediaPlayer wplayer;
            switch (channelNum)
            {
                case 0:
                    wplayer = _wplayer;
                    break;
                case 1:
                    wplayer = _wplayer2;
                    break;
                case 2:
                    wplayer = _wplayer3;
                    break;
                case 3:
                    wplayer = _wplayer4;
                    break;
                default:
                    wplayer = _wplayer;
                    break;
            }

            bool switchedAndPlayed = false;
            bool errorOccured = false;
            _waitingTime = 0;

            while (switchedAndPlayed == false)
            {
                try
                {
                    if (errorOccured) wplayer = new WMPLib.WindowsMediaPlayer();

                    wplayer.URL = file;
                    wplayer.settings.setMode("loop", bLoop);
                    //wplayer.settings.volume = _volume;

                    wplayer.controls.play();

                    switchedAndPlayed = true;
                    if (_waitingTime != 0) _waitingTime = 0;
                    if (_maximum_attempts != _MAXIMUM_ATTEMPTS_Number) _maximum_attempts = _MAXIMUM_ATTEMPTS_Number; //RESET IF SOUND WORKS 
                }
                catch (System.Runtime.InteropServices.COMException e)
                {
                    if (errorOccured == false) errorOccured = true;
                    if (_waitingTime > 4)
                    {   ///CANNOT PLAY SOUND! NO SOUND WILL BE PLAYED + LESS WAITING TIME 
                        switchedAndPlayed = true;
                        _maximum_attempts = 4;
                        _waitingTime = 0;
                        if (MG_Test.DEBUG)
                        {
                            MG_Message.HelpMessage("CANNOT PLAY SOUND!", 3000);
                        }

                        return;
                    }

                    Wait(100);
                    _waitingTime++;
                }
            }
        }

        public static bool IsPlaying(int channelNum)
        {
            WMPLib.WindowsMediaPlayer wplayer;
            switch (channelNum)
            {
                case 0:
                    wplayer = _wplayer;
                    break;
                case 1:
                    wplayer = _wplayer2;
                    break;
                case 2:
                    wplayer = _wplayer3;
                    break;
                case 3:
                    wplayer = _wplayer4;
                    break;
                default:
                    wplayer = _wplayer;
                    break;
            }

            try
            {
                if (wplayer.playState.Equals(WMPPlayState.wmppsPlaying))
                {
                    return true;
                }
                _waitingTime++;
                //MG_Message.SubTitle("_waitingTime=" + _waitingTime);
                if (_waitingTime > _maximum_attempts)
                {
                    _waitingTime = 0;
                    return true;
                }

                return false;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {

                //--MG_Message.SMS("IsPlaying ERROR");
                return true;
                //if ((e.ErrorCode & 0xFFFF) == 0x10a)
                //{   // Excel is busy
                //    //...release the com object 
                //}
                //else
                //{   // Re-throw!
                //    throw e;
                //}
            }

            //if (wplayer.playState.Equals(WMPPlayState.wmppsPlaying))
            //{
            //    return true;
            //}
            //return false;
        }

        internal static void Stop(int channelNum)
        {
            WMPLib.WindowsMediaPlayer wplayer;
            switch (channelNum)
            {
                case 0:
                    wplayer = _wplayer;
                    break;
                case 1:
                    wplayer = _wplayer2;
                    break;
                case 2:
                    wplayer = _wplayer3;
                    break;
                case 3:
                    wplayer = _wplayer4;
                    break;
                default:
                    wplayer = _wplayer;
                    break;
            }

            try
            {
                wplayer.controls.stop();
            }
            catch (System.Runtime.InteropServices.COMException e)
            {
                wplayer = new WMPLib.WindowsMediaPlayer();
            }

        }
    }
}
