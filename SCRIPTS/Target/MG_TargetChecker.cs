////////////////////////////////////////////////////////////////////////////////
//
//	MG_TargetChecker.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using System.Drawing;

namespace MG_Liquidator
{
    public class MG_TargetChecker : Script
    {
        #region Fields
        private static bool _blipsForStealthTargetCreated = false;
        #endregion Fields

        #region Properties
        public static int DISTANCE_TO_ESCAPE { get; set; } = 1650;
        public static bool Enable_red_cone { get; set; } = true;
        #endregion Properties

        #region Public Methods       
        public static void CheckTarget()
        {

            if (MG_Target.Ped.IsDead)
            {
                if (MG_Target.Type.Equals(TargetType.Terrorist))
                {
                    MG_Bombermania.DetonateEveryBomber();
                    Wait(500);
                }

                UI.ShowSubtitle("~r~Target~w~ has been eliminated!", 4000);
                if (MG_Target.Ped.CurrentBlip != null) MG_Target.Ped.CurrentBlip.Remove();
                MG_Statistic.TotalTargetsEliminated++;
                MG_Reward.Activate();
                MG_Hitman.CheckToActivateHitmanAfterSuccess();//Easter Egg
                MG_Statistic.SaveHistory(MissionStatus.WIN);
                MG_Hitman.CanceledMissionsCount = 0;
                //--------------MG_File.SaveProgress(MG_Statistic.TotalTargetsEliminated.ToString());
                MG_FileINI.WriteInit_Statistics();
                Wait(5000);

                //if (MG_Player.Player.WantedLevel > 0 && MG_Player.Player.WantedLevel != 5)
                //{
                //    MG_Player.Player.WantedLevel = MG_Player.Player.WantedLevel + 1;//MG_Assasin_Main.rnd.Next(1, 4)
                //    UI.ShowSubtitle("~r~WARNING: ~w~Your wanted level increased! " + "~w~Total targets eliminated: ~o~" + MG_Statistic.TotalTargetsEliminated + "~w~.", 6000);
                //}
                //else
                //{
                //    UI.ShowSubtitle("~w~Total targets eliminated: ~o~" + MG_Statistic.TotalTargetsEliminated + "~w~.", 5000);                 
                //}
                UI.ShowSubtitle("~w~Total targets eliminated: ~o~" + MG_Statistic.TotalTargetsEliminated + "~w~.", 5000);
                MG_Main.FullResetAfterEndMission();
            }
            else
            {
                if (MG_TargetAI.IsCompromised)
                {
                    float distance = World.GetDistance(MG_Target.Ped.Position, MG_Player.Ped.Position);
                    //MG_Message.SubTitle(distance + "/" + DISTANCE_TO_ESCAPE, 3000);
                    if (distance > DISTANCE_TO_ESCAPE)
                    {
                        MissionFailed_TargetEscaped();
                        return;
                    }
                }


                //World.DrawMarker(MarkerType.UpsideDownCone, MG_Main.Target.Position, new Vector3(0.0f, 0.0f, 5f), Vector3.Zero, new Vector3(1f, 1f, 1f), Color.DarkRed);

                if (MG_Target.Type.Equals(TargetType.Assasin) || MG_Target.Type.Equals(TargetType.Hacker) || MG_Target.Type.Equals(TargetType.Terrorist))
                {
                    if (MG_TargetAI.IsCompromised)
                    {
                        if (_blipsForStealthTargetCreated == false)
                        {
                            _blipsForStealthTargetCreated = true;
                            MG_Target.CreateBlips();
                            MG_TargetBodyGuards.CreateBlips();
                        }
                        World.DrawMarker(MarkerType.UpsideDownCone, MG_Target.Ped.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 1), GameplayCamera.Direction, GameplayCamera.Rotation, new Vector3(1, 1, 1), Color.OrangeRed);
                    }
                    else
                    {
                        MG_Stealth.ShowDistanceBetweenTwoTargets(MG_Player.Ped, MG_Target.Ped);
                    }
                }
                else
                {
                    if (Enable_red_cone)
                        World.DrawMarker(MarkerType.UpsideDownCone, MG_Target.Ped.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 1), GameplayCamera.Direction, GameplayCamera.Rotation, new Vector3(1, 1, 1), Color.OrangeRed);
                }

                //if (MG_Settings.INI_ShowTargetMarker)//&& !MG_Target.Type.Equals(TargetType.Hacker)
                // {
                //   World.DrawMarker(MarkerType.UpsideDownCone, MG_Target.Ped.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 1), GameplayCamera.Direction, GameplayCamera.Rotation, new Vector3(1, 1, 1), Color.OrangeRed);
                //}

                //if (MG_Settings.INI_ShowTargetDistance)//|| (MG_Target.Type.Equals(TargetType.Hacker) && !MG_TargetAI_ExtraActions.PhoneHasBeenHacked)
                //{

                // }

                //if (MG_Target.Blip == null)
                //{
                //    //MG_Message.SubTitle("NULL TARGET BLIP!");
                //    MG_Target.CreateBlip();
                //}

            }
        }

        public static void Reset()
        {
            _blipsForStealthTargetCreated = false;
        }
        #endregion Public Methods

        #region Private Methods
        private static void MissionFailed_TargetEscaped()
        {
            UI.ShowSubtitle("~r~Target~w~ successfuly escaped. ~r~You failed your mission!~w~", 4000);
            if (MG_Target.Ped.CurrentBlip != null) MG_Target.Ped.CurrentBlip.Remove();

            MG_Reward.ActivatePenality();
            MG_Statistic.SaveHistory(MissionStatus.FAIL);
            MG_Hitman.CanceledMissionsCount = 0;
            MG_FileINI.WriteInit_Statistics();
            Wait(5000);
            MG_Main.FullResetAfterEndMission();
        }
        #endregion Private Methods

    }
}