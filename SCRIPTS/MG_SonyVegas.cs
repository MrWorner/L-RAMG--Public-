////////////////////////////////////////////////////////////////////////////////
//
//	MG_SonyVegas.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

//using GTA;
//using GTA.Native;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MG_Liquidator
//{
//    class MG_SonyVegas : Script
//    {
//        #region Fields
//        private static Ped _Michael = null;
//        private static bool _firstAttack = true;
//        private static bool _created = false;
//        private static int _group;
//        private static List<Ped> bodyguards = new List<Ped>();
//        #endregion Fields

//        #region Properties
//        #endregion Properties

//        #region Constructor
//        public MG_SonyVegas()
//        {

//            //Tick += OnTick;
//            //Game.Player.ChangeModel(PedHash.PedHashHere); 

//        }
//        #endregion Constructor

//        #region OnTick
//        private void OnTick(object sender, EventArgs e)
//        {
//            Wait(1);
//            //MichaelAI();
//        }
//        #endregion OnTick

//        #region OnKeyDown
//        #endregion OnKeyDown

//        #region Public Methods
//        public static void StarMovie()
//        {
//            BecomeTarget();
//            SpawnBodyGuards();
//            CreateMichael();
//        }
//        #endregion Public Methods

//        #region Private Methods

//        private static void CreateMichael()
//        {

//            Model pedModel = new Model("PLAYER_ZERO");
//            //Model pedModel = new Model(MG_Random.RandomElement(DB_Peds.BodyGuard_Assasin));
//            pedModel.Request(500);
//            _Michael = World.CreatePed(pedModel, World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(50)));//NEW
//            pedModel.MarkAsNoLongerNeeded();
//            _Michael.Accuracy = 0;
//            _Michael.AddBlip();
//            _Michael.Weapons.Give(GTA.Native.WeaponHash.MG, 999, true, true);
//            _Michael.IsBulletProof = true;
//            _Michael.IsOnlyDamagedByPlayer = true;

//            Wait(1000);
//            //_Michael = ped;
//            //_Michael.Position = Game.Player.Character.Position;
//            _created = true;

//            //_Michael.Task.FightAgainst(Game.Player.Character);
//            //Game.Player.Character.Task.ReactAndFlee(_Michael);
//        }

//        private static void MichaelAI()
//        {
//            if (_created)
//            {
//                float distance = World.GetDistance(_Michael.Position, Game.Player.Character.Position);
//                if (distance > 30f)
//                {
//                    _Michael.Task.RunTo(Game.Player.Character.Position);
//                    //MG_Message.SubTitle(distance.ToString() + " RUN", 5000);
//                }
//                else
//                {                  
//                    if (_firstAttack)
//                    {
//                        Function.Call(Hash.SET_PED_AS_ENEMY, _Michael, true);
//                        Function.Call(Hash.SET_PED_COMBAT_RANGE, _Michael, (int)CombatRange.Far);
//                        foreach (var body in bodyguards)
//                        {
//                            body.Task.FightAgainst(_Michael);
//                        }

//                        Wait(1200);
//                        _firstAttack = false;
//                        Game.Player.Character.Task.ReactAndFlee(_Michael);

//                        Wait(3500);
//                        MG_Ped.PlayCellphoneAnim(Game.Player.Character);
//                        var propPhone = MG_Ped.CreateAndAttachPhone(Game.Player.Character);

//                        GenerateWarningMessage("WARNING: THE TARGET IS CALLING BACKUP!", 7);
//                        MG_Target.Ped = bodyguards[0];
//                        MG_BackupForce.CreateBackup();
//                    }
//                    //MG_Message.SubTitle(distance.ToString() + " ATTACK", 5000);
//                }
//                Wait(1000);
//            }
//        }


//        //private static void MichaelAI()
//        //{
//        //    MG_Message.SubTitle(_created + "", 3000);
//        //    bool created = _created;
//        //    if (created == true)
//        //    {
//        //        //if (Object.ReferenceEquals(null, _Michael) == false)

//        //        if (World.GetDistance(_Michael.Position, MG_Target.Ped.Position) > 10f)
//        //        {
//        //            _Michael.Task.RunTo(Game.Player.Character.Position);
//        //            if (_firstAttack)
//        //            {
//        //                _firstAttack = false;
//        //                Game.Player.Character.Task.ReactAndFlee(_Michael);

//        //            }
//        //            Wait(5000);
//        //        }
//        //        else
//        //        {
//        //            _Michael.Task.FightAgainst(Game.Player.Character);
//        //            Wait(5000);
//        //        }
//        //    }
//        //}

//        private static void BecomeTarget()
//        {
//            Game.Player.ChangeModel(DB_Peds.BodyGuard_GangMember[1]);
//            MG_Target.Type = TargetType.GangMember;
//            //MG_Target.InitTarget(Game.Player.Character);
//            Game.Player.Character.IsBulletProof = true;
//            Game.Player.Character.IsOnlyDamagedByPlayer = true;

//            //Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, Game.Player.Character, 0);
//            //Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Game.Player.Character, 0);

//            _group = Function.Call<int>(Hash.CREATE_GROUP, 0);
//            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Game.Player.Character, _group);
//            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Game.Player.Character, _group);
//            //MG_TargetBodyGuards.AmountCanBeSpawn = 15;
//            //MG_TargetBodyGuards.SpawnBodyGuards();
//            Game.Player.Character.Task.WanderAround();

//        }

//        private static void SpawnBodyGuards()
//        {
//            for (int i = 0; i < 6; i++)
//            {
//                Model pedModel = new Model(GetPedModel());
//                pedModel.Request(500);
//                var ped = World.CreatePed(pedModel, World.GetNextPositionOnSidewalk(Game.Player.Character.Position.Around(15 + MG_Random.Random(25))));//NEW
//                pedModel.MarkAsNoLongerNeeded();

//                MG_Group.SetGroup(ped, Game.Player.Character, 0, 0);
//                ped.Weapons.Give(GetWeaponModel(), 50, true, true);

//                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, _group);
//                Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped, _group);
//                bodyguards.Add(ped);
//            }
//        }

//        private static string GetPedModel()
//        {
//            var type = MG_Target.Type;

//            string[] chosenArray;

//            switch (type)
//            {
//                case TargetType.Normal:
//                    chosenArray = DB_Peds.BodyGuard_Normal;
//                    break;
//                case TargetType.Assasin:
//                    chosenArray = DB_Peds.BodyGuard_Assasin;
//                    break;
//                case TargetType.Terrorist:
//                    chosenArray = DB_Peds.BodyGuard_Bomber;
//                    break;
//                case TargetType.Police:
//                    chosenArray = DB_Peds.BodyGuard_Police;
//                    break;
//                case TargetType.Cartel:
//                    chosenArray = DB_Peds.BodyGuard_Cartel;
//                    break;
//                case TargetType.Hacker:
//                    chosenArray = DB_Peds.BodyGuard_Hacker;
//                    break;
//                case TargetType.Military:
//                    chosenArray = DB_Peds.BodyGuard_Military;
//                    break;
//                case TargetType.GangMember:
//                    chosenArray = DB_Peds.BodyGuard_GangMember;
//                    break;
//                default:
//                    chosenArray = DB_Peds.BodyGuard_Normal;
//                    break;
//            }

//            string chosen = MG_Random.RandomElement(chosenArray);

//            return chosen;
//        }

//        private static WeaponHash GetWeaponModel()
//        {
//            var type = MG_Target.Type;

//            WeaponHash[] chosenArray;

//            switch (type)
//            {
//                case TargetType.Normal:
//                    chosenArray = DB_Weapons.BodyGuard_Normal;
//                    break;
//                case TargetType.Assasin:
//                    chosenArray = DB_Weapons.BodyGuard_Assasin;
//                    break;
//                case TargetType.Terrorist:
//                    chosenArray = DB_Weapons.BodyGuard_Bomber;
//                    break;
//                case TargetType.Police:
//                    chosenArray = DB_Weapons.BodyGuard_Police;
//                    break;
//                case TargetType.Cartel:
//                    chosenArray = DB_Weapons.BodyGuard_Cartel;
//                    break;
//                case TargetType.Hacker:
//                    chosenArray = DB_Weapons.BodyGuard_Hacker;
//                    break;
//                case TargetType.Military:
//                    chosenArray = DB_Weapons.BodyGuard_Military;
//                    break;
//                case TargetType.GangMember:
//                    chosenArray = DB_Weapons.BodyGuard_GangMember;
//                    break;
//                default:
//                    chosenArray = DB_Weapons.BodyGuard_Normal;
//                    break;
//            }

//            WeaponHash chosen = MG_Random.RandomElement(chosenArray);

//            return chosen;
//        }

//        private static void GenerateWarningMessage(string text, int durationInSeconds)
//        {
//            for (int i = 0; i < durationInSeconds; i++)
//            {
//                MG_Message.SubTitle("~w~" + text);
//                Wait(250);
//                MG_Message.SubTitle("~r~" + text);
//                Wait(250);
//                MG_Message.SubTitle("~w~" + text);
//                Wait(250);
//                MG_Message.SubTitle("~r~" + text);
//                Wait(250);
//            }

//        }
//        #endregion Private Methods
//    }
//}
