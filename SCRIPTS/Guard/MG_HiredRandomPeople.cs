////////////////////////////////////////////////////////////////////////////////
//
//	MG_HiredRandomPeople.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

//using GTA;
//using GTA.Native;
//using System;
//using System.Collections.Generic;

//namespace MG_Liquidator
//{
//    public class MG_HiredRandomPeople : Script
//    {
//        #region Fields
//        private bool peopleHasBeenHired = false;
//        private static bool extraActionsAvailable = true;
//        #endregion Fields

//        #region Properties      
//        public static List<Ped> HiredRandomPeople { get; set; } = new List<Ped>();
//        public static int HiredPeople_AmountCanBeSpawn { get; set; } = 0;
//        public static bool CanBeSpawned { get; set; } = false;
//        #endregion Properties

//        #region Constructor
//        public MG_HiredRandomPeople()
//        {
//            Tick += OnTick;
//        }

//        #endregion Constructor

//        #region OnTick
//        private void OnTick(object sender, EventArgs e)
//        {

//            //if (MG_Settings.INI_Observers_ON)
//            //{
//            //    if (MG_TargetAI.IsCompromised)
//            //    {
//            //        if (!peopleHasBeenHired)
//            //        {
//            //            if (CanBeSpawned)
//            //            {
//            //                HireRandomPeople();
//            //                peopleHasBeenHired = true;
//            //            }
//            //        }
//            //        else
//            //        {
//            //            if (extraActionsAvailable)
//            //                ExtraActions();
//            //        }
//            //    }
//            //    else
//            //    {
//            //        if (peopleHasBeenHired)
//            //        {
//            //            peopleHasBeenHired = false;
//            //            if (!extraActionsAvailable)
//            //                extraActionsAvailable = true;
//            //        }
//            //    }
//            //}
//            //else
//            //{
//            //    Tick -= OnTick;
//            //}
//        }
//        #endregion OnTick

//        #region Public Methods
//        public static void HireRandomPeople()
//        {

//            if (MG_Settings.INI_Observers_Max < HiredPeople_AmountCanBeSpawn)
//            {
//                HiredPeople_AmountCanBeSpawn = MG_Settings.INI_Observers_Max;
//            }

//            List<Ped> _pedsToHire = new List<Ped>();
//            Ped[] _innocentPeds = World.GetNearbyPeds(Game.Player.Character, 450);
//            for (int i = 0; i < _innocentPeds.Length; i++)
//            {
//                Ped _ped = _innocentPeds[i];
//                int _pedType = Function.Call<int>(Hash.GET_PED_TYPE, _ped);
//                if
//                (
//                    _ped.IsAlive
//                    && Function.Call<bool>(Hash.IS_PED_HUMAN, _ped, true)
//                    && !Function.Call<bool>(Hash.IS_PED_IN_ANY_HELI, _ped, true)
//                    && !MG_InnocentManager.VictimsJackedCar.Contains(_ped)
//                    && !MG_TargetBodyGuards.Bodyguards.Contains(_ped)
//                    && MG_Target.Ped != _ped
//                    && _pedType != 6//cop
//                    && _pedType != 27//swat
//                    && _pedType != 29//military
//                )
//                {
//                    _pedsToHire.Add(_innocentPeds[i]);
//                }

//            }

//            if (_pedsToHire.Count > 0)
//            {

//                //--------------------- = MG_Random.ShuffleList(_pedsToHire);
//                int _count = 0;
//                //Ped _leader = null;
//                int _group = World.AddRelationshipGroup("HIRED_TEAM_" + MG_Statistic.TotalTargetsEliminated);
//                _group = MG_TargetGroup.Group = Function.Call<int>(Hash.CREATE_GROUP, _group); ;
//                foreach (var _ped in _pedsToHire)
//                {
//                    _count++;

//                    Blip _blip = _ped.AddBlip();
//                    _blip.Color = BlipColor.Green2;
//                    HiredRandomPeople.Add(_ped);

//                    if (MG_Target.Type.Equals(TargetType.Terrorist))
//                    {
//                        MG_Bombermania.MakeSuicideBomber(_ped);
//                        return;
//                    }


//                    if (HiredPeople_AmountCanBeSpawn < _count)
//                    {
//                        break;
//                    }

//                    int _chance = MG_Random.Random(20) + MG_Statistic.TotalTargetsEliminated;
//                    if (!(_chance > MG_Random.Random()))
//                    {
//                        continue;
//                    }

//                    //_ped.IsEnemy = true;
//                    _ped.IsPersistent = true;

//                    //if (_count == 0)
//                    //{
//                    //    _leader = _ped;
//                    //    SetHiredGroupLeader(_leader, _group);
//                    //}
//                    //else
//                    //{
//                    //    MG_Func.SetGroup(_ped, _leader, _group);
//                    //}

//                    MG_Group.SetGroup(_ped, MG_Target.Ped, MG_TargetGroup.RelationsGroup, MG_TargetGroup.Group);

//                    _ped.Task.FightAgainst(Game.Player.Character);
//                    _ped.AlwaysKeepTask = true;




//                    MG_WeaponManager.ProcessPed(_ped, WeaponLevel.Handgun_2);
//                }
//            }
//        }

//        /// <summary>
//        /// Полный ресет
//        /// </summary>
//        public static void Reset()
//        {
//            if (HiredRandomPeople.Count > 0)
//            {
//                foreach (var _ped in HiredRandomPeople)
//                {
//                    Blip _blip = _ped.CurrentBlip;
//                    if (_blip != null)
//                    {
//                        _blip.Remove();
//                    }
//                    _ped.IsPersistent = false;
//                    // _ped.MarkAsNoLongerNeeded();
//                }
//                HiredRandomPeople.Clear();
//            }
//        }
//        #endregion Public Methods

//        #region Private Methods
//        private static void ExtraActions()
//        {
//            int _chance = MG_Random.Random(0, 100);
//            if (_chance > 75)
//            {
//                foreach (Ped _ped in HiredRandomPeople)
//                {
//                    if (_ped.Exists() && _ped.IsAlive)
//                    {
//                        _ped.Task.FleeFrom(MG_Player.PlayerPed);
//                        bool _callCops = true;
//                        if (MG_Target.Type.Equals(TargetType.Cartel))
//                            _callCops = false;
//                        //-----------------bool _completed = MG_TargetAI_ExtraActions.TargetCallBackup(_ped, MG_Player.PlayerPed, _callCops);
//                        //------------------if (_completed)
//                        //------------------    extraActionsAvailable = false;
//                        break;
//                    }
//                }
//            }
//            else
//            {
//                Wait(MG_Random.Random(3000, 30000));
//            }
//        }
//        #endregion Private Methods

//        //private static void SetHiredGroupLeader(Ped _target, int _groupId)//, int _relationGroupId
//        //{
//        //    //MG_Assasin_Main.TargetGroup = World.AddRelationshipGroup("TARGET_TEAM_" + MG_Assasin_Main.TotalTargetsEliminated);
//        //    //MG_Assasin_Main.TargetRelationsGroup = Function.Call<int>(Hash.GET_HASH_KEY, "TARGET_TEAM_" + MG_Assasin_Main.TotalTargetsEliminated);

//        //    //Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, _relationGroupId, MG_Assasin_Main.TargetRelationsGroup);
//        //    //Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, MG_Assasin_Main.TargetRelationsGroup, _relationGroupId);
//        //    Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, _target, MG_Assasin_Main.TargetRelationsGroup);
//        //    Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, _target, _groupId);
//        //    Function.Call(Hash.SET_PED_AS_GROUP_LEADER, _target, _groupId);
//        //    Function.Call(Hash.SET_GROUP_FORMATION, _groupId, MG_Assasin_Main.rnd.Next(0, 4));//31.01.2020
//        //    Function.Call(Hash.SET_GROUP_FORMATION_SPACING, _groupId, MG_Assasin_Main.rnd.Next(2, 40), MG_Assasin_Main.rnd.Next(2, 40), MG_Assasin_Main.rnd.Next(2, 40));//31.01.2020
//        //}


//    }
//}
