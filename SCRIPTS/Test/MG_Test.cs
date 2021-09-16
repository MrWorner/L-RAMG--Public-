////////////////////////////////////////////////////////////////////////////////
//
//	MG_Test.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;
using iFruitAddon2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MG_Liquidator
{
    class MG_Test : Script
    {
        public static bool DEBUG { get; set; } = false;
        private bool _debug = true;
        private bool _isTestingON = false;
        private Ped _ped = null;
        private Ped _player = MG_Player.Ped;
        //private Microsoft.DirectX.AudioVideoPlayback.Audio _audio;

        #region Constructor

        public MG_Test()
        {
            if (_debug == false) return;


            KeyDown += OnKeyDown;
        }
        #endregion Constructor

        #region OnTick

        private void OnTick(object sender, EventArgs e)
        {
            Ped target = MG_Target.Ped;

            if (target == null)
            {
                UI.ShowSubtitle("Null target!");
            }
            else
            {
                string exists = " exists=" + target.Exists() + " ";
                string isHuman = " isHuman=" + target.IsHuman + " "; //GTA.Native.Function.Call<bool>(GTA.Native.Hash.IS_PED_HUMAN, ped, true)
                string IsAlive = " IsAlive=" + target.IsAlive + " ";
                UI.ShowSubtitle(exists + isHuman + IsAlive);
            }

            Wait(250);

        }
        #endregion OnTick

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {

            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=netframework-4.8
            if (e.KeyCode == Keys.Enter)
            {
                if (DEBUG)
                {
                    if (_isTestingON)
                    {
                        UI.ShowSubtitle("Debug disabled!", 300);
                        _isTestingON = false;
                        //Tick -= OnTick;
                    }
                    else
                    {
                        UI.ShowSubtitle("Debug enabled!", 300);
                        _isTestingON = true;
                        //Tick += OnTick;
                    }
                }
            }

            if (_isTestingON == false) return;

            //_player.Task.PlayAnimation("missminuteman_1ig_2", "handsup_enter", 10f, 10f, -1, ((AnimationFlags)2 | (AnimationFlags)128), 0f);//good

            //_player.Task.PlayAnimation("random@getawaydriver", "idle_2_hands_up", 1f, -1, false, 0f);
            //Script.Wait(2500);


            //  _player.Task.PlayAnimation("missminuteman_1ig_2", "handsup_base", 1f, -1, true, 0f);//good




            //_player.Task.PlayAnimation("missminuteman_1ig_2", "handsup_base", 10f, 10f, -1, ((AnimationFlags)2 | (AnimationFlags)16 | (AnimationFlags)33), 0f);//HANDS UP AND CAN MOVE!!! 5/5 можно использовать если в машине
            //_player.Task.PlayAnimation("random@arrests@busted", "idle_c", 10f, 10f, -1, ((AnimationFlags)16 | (AnimationFlags)33), 0f);//HANDS OVER HEAD AND CAN MOVE!!! 5/5

            // _player.Task.PlayAnimation("mp_arresting", "a_uncuff", 4f, -1, false, 0f);//АНИМАЦИЯ НАРУЧНИКОВ НА ТЕБЯ ТИПА для копа
            //_player.Task.PlayAnimation("mp_arresting", "idle", 10f, 10f, -1, ((AnimationFlags)16 | (AnimationFlags)33), 0f);//НАРУЧНИКИ ЗА СПИНОЙ!!! 5/5

            //MG_Player.Player.IgnoredByEveryone = true;
            //MG_Player.Player.IgnoredByPolice = true;


            if (e.KeyCode == Keys.NumPad2)
            {
                //None = 0,
                //Loop = 1,
                //StayInEndFrame = 2,
                //UpperBodyOnly = 16,
                //AllowRotation = 32,
                //CancelableWithMovement = 128,
                //RagdollOnCollision = 4194304

                //_player.Task.PlayAnimation("random@mugging5", "ig_2_guy_enter_handsup", 4f, 5000, false, 1f);
                //character.Task.PlayAnimation("missminuteman_1ig_2", "handsup_enter", 8f, 8f, -1, (AnimationFlags)2, 0f);


                //_player.Task.PlayAnimation("mp_arresting", "arrested_spin_l_0", 4f, -1, false, 0f);



                //Wait(5000);
                //MG_Message.SubTitle("CLEARED!", 2000);


            }
            else if (e.KeyCode == Keys.NumPad3)
            {
                _player.Task.ClearAnimation("missminuteman_1ig_2", "handsup_base");
            }
            else if (e.KeyCode == Keys.NumPad4)
            {

            }
            else if (e.KeyCode == Keys.NumPad5)
            {

            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                MG_TargetAI.IsCompromised = true;
            }
            else if (e.KeyCode == Keys.NumPad7)
            {
                MG_Player.DisableHUD(true);
                MG_Player.DisableRadar(true);
            }
            else if (e.KeyCode == Keys.NumPad8)
            {
                MG_Player.DisableHUD(false);
                MG_Player.DisableRadar(false);


                World.DestroyAllCameras();
                World.RenderingCamera = null;

            }
            else if (e.KeyCode == Keys.NumPad9)
            {

                //https://github.com/Bob74/iFruitAddon2/releases

                //if (File.Exists(MG_File.HistoryFile))
                //{
                //    var lastLine = File.ReadLines(MG_File.HistoryFile).Last();
                //    var result = lastLine.Split(new string[] { "Total target kills: " }, StringSplitOptions.None)[1].Split(' ')[0].Trim();

                //    MG_Message.SubTitle("result= " + Int32.Parse(result));

                //}

                //foreach (iFruitContact item in MG_iFruit.IFruit.Contacts)
                //{
                //    MG_Message.SubTitle(MG_iFruit.IFruit.Contacts.Count +  " = " + item.Name);
                //    Wait(5000);
                //}


                //bool aa = Function.Call<bool>(Hash.GET_HASH_KEY, "CAN_PHONE_BE_SEEN_ON_SCREEN");
                //MG_Message.SubTitle(aa + "");



                //Game.TimeScale = 0.85f;




                //if (MG_Target.Ped != null)
                //{
                //    MG_Target.Ped.Task.FightAgainst(MG_Player.Ped);
                //}

                // MG_Camera.Test();
                // MG_SonyVegas.StarMovie();
                //MG_Target.Ped = MG_Player.Ped;
                //MG_TargetAI_ExtraActions.CarBomb();

                //foreach (var item in DB_Weapons.BodyGuard_Police)
                //{
                //    string pedModelStr = DB_Peds.BodyGuard_Assasin[0];
                //    Model pedModel = new Model(pedModelStr);
                //    pedModel.Request(500);
                //    Ped = World.CreatePed(pedModelStr, MG_Player.Ped.Position);//NEW

                //    Ped.Weapons.Give(item, 5, true, true);
                //    Wait(1000);
                //}


                //WeaponHash.AssaultShotgun,

                //WeaponHash.AssaultSMG,

                //WeaponHash.SpecialCarbine,
                //WeaponHash.MilitaryRifle,
                //WeaponHash.CarbineRifleMk2,

                //WeaponHash.CombatMG,
                //WeaponHash.CombatMGMk2,

                //WeaponHash.MarksmanRifle,
                //WeaponHash.MarksmanRifleMk2,

                //WeaponHash.GrenadeLauncher


                // MG_Hitman.AngryTalkTest();
                //MG_Camera.Test();

                //MG_Hitman.CreateHitman();
                //MG_Player.Ped.Task.ClearAllImmediately();
                //MG_Player.Ped.Task.PlayAnimation("get_up@standard", "front");
                //MG_Player.Ped.Task.PlayAnimation("get_up@directional_sweep@combat@pistol@back", "get_up_0");

                //////////////////Генерируем историю!ПОЛНЫЙ ДЕБАГ ВСЕХ ПРЕСЕТОВ!
                //for (int i = 1; i < 138; i++)
                //{
                //    MG_MissionPreset.GeneratePreset();
                //    if (MG_Random.Random() > 15)
                //    {
                //        MG_Statistic.TotalTargetsEliminated = i;
                //        MG_Statistic.SaveHistory(MissionStatus.WIN);
                //    }
                //    else if (MG_Random.Random() > 35)
                //    {
                //        MG_Statistic.SaveHistory(MissionStatus.FAIL);
                //        i--;
                //    }
                //    else
                //    {
                //        MG_Statistic.SaveHistory(MissionStatus.CANCELLED);
                //        i--;
                //    }

                //    MG_Message.SubTitle("Generating fake history: Working on " + (i + 1) + "/138");
                //    Wait(100);
                //}

                // MG_Audio.Play(MG_Advisor._PhoneCall, 0, false);
                //MG_Message.SubTitle("Waiting for sound begin!",10000);
                //while (MG_Audio.IsPlaying(0) == false) Wait(100);
                //MG_Message.SubTitle("Waiting for sound END!", 10000);
                //while (MG_Audio.IsPlaying(0) == true) Wait(100);
                //MG_Message.SubTitle("ENDED!", 10000);


            }
        }
        #endregion OnKeyDown

        #region методы

        private static void TestPedModelsFromArray()
        {
            //if (Ped != null)
            //{
            //    Ped.Delete();
            //}

            //string pedModelStr = DB_Peds.BodyGuard_Assasin[iii];
            //Model pedModel = new Model(pedModelStr);
            //pedModel.Request(500);
            //Ped = World.CreatePed(pedModelStr, MG_Player.Ped.Position);//NEW

            //Ped.Weapons.Give(WeaponHash.APPistol, 5, true, true);

            //UI.ShowSubtitle(iii + "= " + pedModelStr, 99000);
            //iii++;
            //Ped.Task.FightAgainst(MG_Player.Ped);
            //MG_Player.Player.WantedLevel = 0;
        }

        private static void AllCarsSpawn()
        {

            //if (e.KeyCode == Keys.NumPad2)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Normal)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveProgress("NOT IN CAR!: " + item);

            //        vehicle.Delete();
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Normal", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad3)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Assasin)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveProgress("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Assasin", 99000);

            //}
            //else if (e.KeyCode == Keys.NumPad4)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Police)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveHistory("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Police", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad5)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Cartel)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveHistory("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Cartel", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad6)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Hacker)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveHistory("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Hacker", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad7)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_Military)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveHistory("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_Military ", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad8)
            //{
            //    foreach (var item in DB_Vehicles.BodyGuard_GangMember)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveHistory("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);
            //    UI.ShowSubtitle("DONE! BodyGuard_GangMember", 99000);
            //}
            //else if (e.KeyCode == Keys.NumPad9)
            //{










            //    foreach (var item in DB_Vehicles.BodyGuard_Bomber)
            //    {
            //        Model vehicleModel = new Model(item);
            //        vehicleModel.Request(500);
            //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
            //        MG_File.Debug(item);
            //        MG_Player.Ped.SetIntoVehicle(vehicle, VehicleSeat.Driver);
            //        UI.ShowSubtitle("" + item, 99000);
            //        Wait(350);
            //        if (MG_Player.Ped.IsInVehicle() == false) MG_File.SaveProgress("NOT IN CAR!: " + item);
            //        vehicle.Delete();// Wait(1000);    
            //    }
            //    Wait(1000);

            //    UI.ShowSubtitle("DONE! BodyGuard_Bomber", 99000);


            //}
        }

        private static void TeleportTargetToPlayer_9()
        {
            Ped target = MG_Target.Ped;
            Ped playerPed = MG_Player.Ped;
            if (target != null)
                if (target.Exists())
                    target.Position = playerPed.Position;

            UI.ShowSubtitle("TeleportTargetToPlayer_9()");
        }

        private static void TestPresets()
        {
            //--Кол-во телохранителей
            for (int i = 0; i < 1000; i++)
            {

                int n = MG_DifficultyRaiser.GenerateNumberBodyguards();
                int min = MG_DifficultyRaiser._min_Bodyguards;
                int max = MG_DifficultyRaiser._max_Bodyguards;
                MG_Message.SubTitle("bodyguards: " + n + " min= " + min + " max= " + max + " success=" + i);
                Wait(50);
                MG_Statistic.TotalTargetsEliminated = i;
            }

            //--Кол-во призраков
            int totalSpawnedTimes = 0;
            for (int i = 0; i < 1000; i++)
            {

                int n = MG_DifficultyRaiser.GenerateNumberWatcherSquads();
                //int min = MG_DifficultyRaiser._min_Watchers;
                int max = MG_DifficultyRaiser._max_Watchers;
                int percentage = MG_DifficultyRaiser._watchersProbabilityPercentage;
                if (n > 0) totalSpawnedTimes++;

                MG_Message.SubTitle("Watchers: " + n + " Min= " + " Max= " + max + " Percentage=" + percentage + " Total= " + totalSpawnedTimes + " Success=" + i, 60000);
                Wait(1000);
                MG_Statistic.TotalTargetsEliminated = i;
            }

            //--Кол-во подкреплений
            for (int i = 0; i < 1000; i++)
            {
                // public static int _min_Backup = 1;
                //public static int _max_Backup = 1;
                //public static int _LIMIT_Backup = 3;
                int n = MG_DifficultyRaiser.GenerateNumberBackupForceSquad();
                int min = 1;
                int max = MG_DifficultyRaiser._max_Backup;

                MG_Message.SubTitle("Backup: " + n + " Min= " + min + " Max= " + max + " Success=" + i, 60000);
                Wait(333);
                MG_Statistic.TotalTargetsEliminated = i;
            }

            //--Типы целей
            List<string> targetTypes = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                MG_Message.SubTitle("GenerateTargetType: Working " + (i + 1) + "/100");
                targetTypes.Add(MG_DifficultyRaiser.GenerateTargetType().ToString());
                MG_Statistic.TotalTargetsEliminated = i;
                Wait(50);
            }

            //--Типы целей
            List<string> targetTypes2 = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                MG_Message.SubTitle("GenerateTargetType: Working " + (i + 1) + "/100");
                targetTypes2.Add(MG_DifficultyRaiser.GenerateTargetType().ToString());
                MG_Statistic.TotalTargetsEliminated = i;
                Wait(50);
            }

            MG_Statistic.TotalTargetsEliminated = 0;



            //Генерируем историю! ПОЛНЫЙ ДЕБАГ ВСЕХ ПРЕСЕТОВ!
            for (int i = 0; i < 100; i++)
            {
                MG_MissionPreset.GeneratePreset();
                if (MG_Random.Random() > 15)
                {
                    MG_Statistic.TotalTargetsEliminated = i;
                    MG_Statistic.SaveHistory(MissionStatus.WIN);
                }
                else if (MG_Random.Random() > 35)
                {
                    MG_Statistic.SaveHistory(MissionStatus.FAIL);
                    i--;
                }
                else
                {
                    MG_Statistic.SaveHistory(MissionStatus.CANCELLED);
                    i--;
                }

                MG_Message.SubTitle("Generating fake history: Working on " + (i + 1) + "/100");
                Wait(50);
            }


        }


        #endregion методы

        #region методы (АРХИВ)

        //private static void CreatePeds()
        //{
        //    foreach (var item in DB_Peds.BodyGuard_Bomber)
        //    {
        //        string pedModelStr = item;
        //        Model pedModel = new Model(pedModelStr);
        //        pedModel.Request(500);
        //        Ped ped = World.CreatePed(pedModelStr, MG_Player.Ped.Position);//NEW
        //        pedModel.MarkAsNoLongerNeeded();
        //        UI.ShowSubtitle("" + item, 99000);
        //        ped.Task.FleeFrom(MG_Player.Ped);
        //        Wait(500);
        //        ped.Delete();
        //    }

        //    UI.ShowSubtitle("DONE!", 99000);
        //}




        //private static void CreateVehicles()
        //{
        //    foreach (var item in DB_Vehicles.BodyGuard_Bomber)
        //    {
        //        Model vehicleModel = new Model(item);
        //        vehicleModel.Request(500);
        //        Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(MG_Player.Ped.Position.Around(20f)));//NEW
        //                                                                                                                               // Wait(1000);    
        //    }

        //}

        //private static void TEST_SpawnBodyguard()
        //{
        //    MG_TargetBodyGuards.Spawn(MG_Player.PlayerPed, MG_Player.RelationsGroup, MG_Player.Group, true);
        //}


        //private static void CreatePoliceWatchers()
        //{
        //    List<string> _modelCarList = new List<string>() { "ambulance", "riot", "police4", "fbi2" };
        //    List<string> _modelCopList = new List<string>() { "S_M_Y_SWAT_01", "MP_M_FIBSEC_01" };
        //    List<VehicleSeat> _seatList = new List<VehicleSeat>() { VehicleSeat.Driver, VehicleSeat.LeftRear, VehicleSeat.RightRear, VehicleSeat.RightFront };//VehicleSeat.LeftFront,

        //    string _chosenModelVehicle = MG_Random.RandomElement(_modelCarList);
        //    string _chosenModelCop = MG_Random.RandomElement(_modelCopList);

        //    Function.Call(Hash.REQUEST_MODEL, _chosenModelVehicle);
        //    Vehicle _vehicle = World.CreateVehicle(_chosenModelVehicle, World.GetNextPositionOnStreet(Game.Player.Character.Position.Around(10f)));
        //    Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, _chosenModelVehicle);
        //    Wait(0);
        //    Function.Call(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, _vehicle);
        //    Ped _cop = null;
        //    foreach (var _seat in _seatList)
        //    {
        //        // _vehicle
        //        Function.Call(Hash.REQUEST_MODEL, _chosenModelCop);
        //        Wait(0);
        //        _cop = World.CreatePed(_chosenModelCop, World.GetNextPositionOnSidewalk(_vehicle.Position));//NEW
        //        Function.Call(Hash.SET_MODEL_AS_NO_LONGER_NEEDED, _chosenModelCop);
        //        _cop.Task.WarpIntoVehicle(_vehicle, _seat);
        //        Blip _blip = _cop.AddBlip();
        //        _blip.Color = BlipColor.BlueDark;
        //        Function.Call(Hash.SET_PED_AS_COP, _cop);
        //        //Function.Call(Hash.SET_PED_HELMET_PROP_INDEX, _cop, );
        //        //Function.Call(Hash.SET_PED_DEFAULT_COMPONENT_VARIATION, _cop);
        //        //Function.Call(Hash.SET_PED_RANDOM_PROPS, _cop);
        //        Function.Call(Hash.SET_VEHICLE_SIREN, _vehicle, true);
        //        Function.Call(Hash.SET_PED_PROP_INDEX, _cop, 0, 0, 0, 0);//HELMET

        //        Wait(0);
        //        //_cop.Task.Arrest(Game.Player.Character);
        //        //_cop.Task.GuardCurrentPosition();
        //        //_cop.Task.WanderAround(_vehicle.Position, 30f);
        //        //_cop.Task.FollowToOffsetFromEntity(Game.Player.Character, Game.Player.Character.Position, -1, 3f);
        //        _cop.Weapons.Give((WeaponHash)Function.Call<int>(Hash.GET_HASH_KEY, "WEAPON_CARBINERIFLE"), 3, true, true);
        //        //_cop.Task.FollowToOffsetFromEntity(Game.Player.Character, new Vector3(2f, -10f, 0f), -1, 5f);//---------
        //        GTA.Native.Function.Call(Hash.SET_PED_CURRENT_WEAPON_VISIBLE, _cop, true, false, false, false);
        //        //GTA.Native.Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, _cop, Game.Player.Character, 2f, (-10f), 0f,  10f, (-1), 10f, 1);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!! 

        //        //TASK_VEHICLE_ESCORT(Ped ped, Vehicle vehicle, Vehicle targetVehicle, int mode, float speed, int drivingStyle, float minDistance, int p7, float noRoadsDistance) 
        //        //_cop.AlwaysKeepTask = true;
        //        //

        //    }
        //    GTA.Native.Function.Call(Hash._TASK_VEHICLE_FOLLOW, _vehicle.Driver, _vehicle, Game.Player.Character, 60f, DrivingStyle.Rushed.GetHashCode(), 0f);//------------IT WORKS!!!!!!!!!!!!!!!!!!!!!!!!!

        //}

        //private static void TEST_AllPeds_2()
        //{        
        //    Ped[] peds = World.GetNearbyPeds(MG_Player.PlayerPed, 300);//1000
        //    List<Ped> candidates = new List<Ped>();
        //    for (int i = 0; i < peds.Length; i++)
        //    {
        //        Ped ped = peds[i];
        //        Vector3 pedPos = ped.Position;
        //        if
        //         (
        //         ped.IsHuman
        //         && ped.Exists()
        //         && ped.IsAlive
        //         && World.GetDistance(MG_Player.PlayerPed.Position, pedPos) > 0//45
        //         && World.GetDistance(MG_Player.PlayerPed.Position, pedPos) < 125//105
        //         )
        //        {
        //            candidates.Add(ped);
        //            //Function.Call(Hash.REPORT_CRIME, ped, 11,1);
        //            //Function.Call(Hash.REPORT_CRIME, MG_Player.PlayerPed, 11,1);
        //            //MG_Player.Player.CanControlCharacter = false;
        //            //MG_Player.PlayerPed.Task.ClearAll();

        //            //MG_Player.PlayerPed.Task.WanderAround();
        //            break;
        //        }
        //    }
        //    UI.ShowSubtitle("TEST_2() candidates:" + candidates.Count);
        //}


        //private static void TEST_MG_TargetPreset_8()
        //{
        //    //MG_TargetPreset.GeneratePreset();          
        //    string str_1 = "Type:" + MG_Target.Type + " ";
        //    string str_2 = "Morale:" + MG_Target.Morale + " ";
        //    string str_3 = "WeaponTraining:" + MG_Target.WeaponTraining + " ";
        //    string str_4 = "Accuracy:" + MG_Target.Accuracy + " ";
        //    string str_5 = "ArmorType:" + MG_Target.ArmorType + " ";
        //    string str_6 = "Armor:" + MG_Target.Armor + " ";
        //    string str_7 = "WeaponLevel:" + MG_Target.WeaponLevel + " ";
        //    string str_8 = "PrimaryWeapon:" + MG_Target.PrimaryWeapon + " ";

        //    UI.ShowSubtitle("Preset: " + str_1 + str_2 + str_3 + str_4 + str_5 + str_6 + str_7 + str_8, 20000);

        //}


        #endregion методы (АРХИВ)

    }
}
