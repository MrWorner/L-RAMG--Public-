////////////////////////////////////////////////////////////////////////////////
//
//	MG_Main.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////


using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

//http://www.kronzky.info/fivemwiki/index.php/Category:Script_Commands ВСЕ КОМАНДЫ!
//http://www.kronzky.info/fivemwiki/index.php/Category:Reference_Lists ВСЕ КОМАНДЫ!
//https://docs.fivem.net/natives/ ВСЕ КОМАНДЫ!
//http://gtaxscripting.blogspot.com/2013/07/tut-drawing-shapes-and-text-on-screen.html  //DRAWING SHAPES
//https://forums.gta5-mods.com/topic/15743/help-draw-texture-on-screen UI.CreateTexture(...) and UI.DrawTexture(...)

namespace MG_Liquidator
{

    public class MG_Main : Script
    {
        #region Constructor
        public MG_Main()
        {
            MG_FileINI.ReadInit();
            PlayerSetup();
            //LoadProgress();
            MG_Statistic.TryToLoadProgressFromDB();
            if (MG_Statistic.TotalTargetsEliminated > 0)
            {
                MG_Advisor.SkipTutorial = true;
            }
            MG_Controls.Init();
            MG_iFruit.Create_iFruitContact();
            MG_Advisor.Init();


            Tick += OnTick;
            //KeyDown += OnKeyDown;
            Function.Call(Hash.SET_RANDOM_EVENT_FLAG, true);

        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            if (MG_Player.IsAlive())
            {
                //--OnKeyPressed
                if (MG_Controls.CellPhoneActionPressed)
                {
                    if (MG_Controls.BlockButtonActionButton == false)
                    {
                        MG_Controls.BlockButtonActionButton = true;

                        if (MG_Player.IsUsingCellphone == false)
                        {

                            if (MG_Hitman.MadeMad)
                            {
                                //if (MG_iFruit.IsUsing == false)
                                MG_AssassinationMission.NotAvailableAnymore();
                            }
                            else
                            {

                                if (MG_AssassinationMission.IsJobActive)
                                {
                                    if (MG_Settings.INI_isCanBeCancelled)
                                    {
                                        //if (MG_iFruit.IsUsing == false)
                                        //{
                                        MG_Statistic.SaveHistory(MissionStatus.CANCELLED);
                                        MG_AssassinationMission.CancelJob();
                                        //}

                                    }
                                }
                                else
                                {
                                    //if (MG_iFruit.IsUsing == false)
                                    MG_AssassinationMission.StartJob();
                                    //------FOR FAST TEST
                                    //isJobActive = true;//DEL
                                    //isReadyToStartSearchAtPosition = true;
                                    //------FOR FAST TEST
                                }
                            }
                        }
                        MG_Controls.BlockButtonActionButton = false;
                        MG_Controls.CellPhoneActionPressed = false;
                        //MG_Message.SubTitle("DISABLED! BlockButtonActionButton=" + MG_Controls.BlockButtonActionButton);
                    }
                }
                //--OnKeyPressed


                if (MG_Statistic.TotalTargetsEliminated == 0)
                {
                    if (MG_Advisor.SkipTutorial == false)
                    {
                        MG_Advisor.StartTutorial();
                    }
                }

                if (MG_AssassinationMission.IsDeadOnActiveMissionActivated)
                {
                    MG_AssassinationMission.ShowDefeatMessage();
                }
                else
                {
                    if (MG_AssassinationMission.IsJobActive)
                    {
                        if (MG_TargetFinder.IsPlayerOnPosition)
                        {
                            if (MG_TargetFinder.IsTargetFound)
                            {
                                MG_TargetChecker.CheckTarget();
                            }
                            else
                            {
                                if (MG_Hitman.IsTarget)
                                {
                                    MG_Hitman.HitmanCall();
                                }
                                else
                                {
                                    MG_TargetFinder.FindTheTarget();
                                }
                            }
                        }
                        else
                        {
                            MG_TargetFinder.CheckRoutine();
                        }
                    }
                }

                if (MG_GarbageCollector.NeedToCleanAfterPlayerDeath)
                {
                    MG_GarbageCollector.StartCleaning();
                    MG_GarbageCollector.NeedToCleanAfterPlayerDeath = false;
                    MG_ForgottenEnemy.RemoveEveryone();
                }
                else
                {
                    MG_GarbageCollector.CheckVeryFarVehiclesAndRemove();
                }
            }
            else
            {
                if (MG_AssassinationMission.IsJobActive)
                {
                    if (MG_AssassinationMission.IsDeadOnActiveMissionActivated == false)
                    {
                        MG_Statistic.SaveHistory(MissionStatus.FAIL);
                        MG_AssassinationMission.Defeat();
                    }
                }

                if (MG_GarbageCollector.NeedToCleanAfterPlayerDeath == false)
                {
                    MG_GarbageCollector.NeedToCleanAfterPlayerDeath = true;
                }
            }
        }
        #endregion OnTick

        //#region OnKeyDown

        //private void OnKeyDown(object sender, KeyEventArgs e)
        //{
        //    //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netframework-4.8

        //    if (e.KeyCode == MainButtonToBeginMission)
        //    {
        //        if (MG_Player.IsUsingCellphone == false)
        //        {

        //            if (MG_Hitman.MadeMad)
        //            {
        //                MG_AssassinationMission.NotAvailableAnymore();

        //            }
        //            else
        //            {

        //                if (MG_AssassinationMission.IsJobActive)
        //                {
        //                    if (MG_Settings.INI_isCanBeCancelled)
        //                    {
        //                        MG_Statistic.SaveHistory(MissionStatus.CANCELLED);
        //                        MG_AssassinationMission.CancelJob();
        //                    }
        //                }
        //                else
        //                {

        //                    MG_AssassinationMission.StartJob();

        //                    //------FOR FAST TEST
        //                    //isJobActive = true;//DEL
        //                    //isReadyToStartSearchAtPosition = true;
        //                    //------FOR FAST TEST
        //                }
        //            }
        //        }
        //    }
        //}
        //#endregion OnKeyDown

        #region Public Methods

        public static void FullResetAfterEndMission()
        {
            MG_AssassinationMission.Reset();
            MG_TargetFinder.Reset();
            MG_Target.Reset();
            MG_TargetAI.Reset();
            MG_TargetAI_ExtraActions.Reset();
            MG_TargetBodyGuards.Reset();
            //MG_HiredRandomPeople.Reset();
            MG_Watchers.Reset();
            MG_Bombermania.Reset();
            MG_InnocentManager.Reset();
            MG_BackupForce.Reset();
            MG_TargetChecker.Reset();
        }
        #endregion Public Methods

        #region Private Methods
        private static void PlayerSetup()
        {
            if (MG_Player.DisableHealthRegeneration)
            {
                MG_Player.DisableHealthRecharge();
            }
        }

        //private static void LoadProgress()
        //{
        //    var progressStr = MG_File.LoadProgress();
        //    //MG_File.Debug(progressStr + " <-------");
        //    MG_Statistic.TotalTargetsEliminated = int.Parse(progressStr[0]);
        //}
        #endregion Private Methods
    }
}