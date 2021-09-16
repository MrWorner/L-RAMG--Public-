////////////////////////////////////////////////////////////////////////////////
//
//	MG_Hitman.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG_Liquidator
{
    class MG_Hitman : Script
    {
        #region Fields

        private static int _shots = 0;
        private static string _YOU_WIN_TEXT = "Ok ok, calm down! I will back later.";
        private static string _YOU_WIN_VOICE = @"scripts\HW_LIQUIDATOR2021\SOUND\Calm down.mp3";
        #endregion Fields

        #region Properties
        public static bool MadeMad { get; set; } = false;
        public static int CanceledMissionsCount { get; set; } = 0;
        public static Ped Ped { get; set; } = null;
        public static int KilledTimes { get; set; } = 0;
        public static bool PlayerKilled { get; set; } = false;
        public static bool Agent47WithRandomMask { get; set; } = true;

        public static bool IsTarget { get; set; } = false;
        public static int[] ArrayOfSuccessfulMissions { get; set; } = new int[]
        {
            146,
            266,
            376,
            496,
            598,
            666
        };


        //public static WeaponHash[] weapons { get; set; } = new WeaponHash[]
        //{
        //        //---WeaponHash.Railgun,
        //        WeaponHash.Gusenberg,
        //        WeaponHash.MarksmanRifleMk2,
        //        WeaponHash.CompactRifle,
        //        WeaponHash.HeavySniper,
        //        WeaponHash.CombatMG,
        //        //WeaponHash.CompactGrenadeLauncher,
        //        //WeaponHash.GrenadeLauncher,
        //        WeaponHash.MarksmanRifle
        //};

        public static Dictionary<string, string> TooFar_Variants = new Dictionary<string, string>()
        {
           { "You are in a good mood? Don't think so.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling2.mp3"  },
           { "Never trust anyone and rely on your instincts.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling4.mp3"  },
           { "You have been searching for me.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling6.mp3"  },
           { "You have been looking for me.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling7.mp3"  },
           { "Wrong wrong wrong...",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling10.mp3"  },
           { "There is no use of hidding.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling16.mp3"  },
           { "I hope you like it.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling14.mp3"  }
        };

        public static Dictionary<string, string> NiceWork_Variants = new Dictionary<string, string>()
        {
            { "I also see.. death. Your future is death.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling1.mp3"  },
            { "Hmmm... Quite impressive.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling3.mp3"  },
            { "Congratulations.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling5.mp3"  },
            { "Life is a straight line with a suddent end.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling8.mp3"  },
            { "Good work.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling9.mp3"  },
            { "Very good.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling11.mp3"  },
            { "Very well.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling13.mp3"  },

            { "I've said it's good to live dangerously.",@"scripts\HW_LIQUIDATOR2021\SOUND\Trolling15.mp3"  }


        };

        private static List<string> _said_GoodWork_TEXT = new List<string>();
        private static List<string> _said_GoodWork_VOICE = new List<string>();
        private static List<string> _said_FarAway_TEXT = new List<string>();
        private static List<string> _said_FarAway_VOICE = new List<string>();

        public static int[] Masks { get; set; } = new int[]
        {
            //2,//череп
            //4,//джейсон
            //9,//веселая корова
            //10,//жуткий снеговик
            //15,//джейсон скелет маска

            //19,//сова
            //21,//злой медведь
            //22,//злой бык
            23,//злой бык 2
            //24,//орел
            //25,//падальщик
            //26,//злой волк
            //28,//баллистическая маска мотокросса
            //29,//баллистическая маска череп
            //31,//веселый пингвин
            //35,//балаклава
            //39,//жуткая маска мертвого!
            //40,//жуткая маска мертвого
            //42,//жуткая маска мертвого
            //48,//весь в изоленте
            //49,//пакет на голове
            //60,//тыква
            //62,//Фрэдди крюгер)
            //63,//без кожи жуткая маска
            64,//Жесть в стиле нечто!
            //66,//Муха жуткая
            //68,//Демон
            //69,//Пугало
            //70,//жуткая маска! Типа зомбирован в глазах
            //71,//злая печенька
            //72,//злая печенька 2
            //85,//курица варенная на голове как маска
            //92,//морское чудовище
            //93,//динозавр
            //105,//черный череп хохотающий
            //106,//баллистическая маска в камо Круто
            //108,//череп норм
            //110,//череп жуткий крутой с красными глазами
            //112,//полу череп и маска
            //123,//3д измерение Норм!
            //125,//крутая баллистическая маска
            //129,//баллистический противогаз
            //130,//крутая маска противогаз
            //132,//сплинтер селл
            136,//Доктор 18 века
            //137,//похоже на Доктора 18 века
            //138,//электронный морж
            //140,//глаз инопланетянен
            //141,// инопланетянен
            //144,// гамбургер
            //155,//жуткий джокер как на карте
            //159,//жуткий джокер как на карте 2
            162//мертвая свинья
            //163,//мертвая обезьяна
            //168,//кровавый череп
            //170,//маска пугала 2
            //177,//Противогаз с красными линзами
            //180,//зловещий инопланетянин
            //183,//зловещая маска улыбка
            //184,//гиена
            //190//череп козла
        };


        #endregion Properties

        #region Constructor
        public MG_Hitman()
        {
            Tick += OnTick;

        }
        #endregion Constructor

        #region OnTick
        private void OnTick(object sender, EventArgs e)
        {
            if (PlayerKilled == false)
            {
                if (Ped != null)
                {
                    CheckNightTime();
                    if (Ped.IsAlive)
                    {
                        float distance = Vector2.Distance(Ped.Position, MG_Player.Ped.Position);
                        int _roundedDistance = Convert.ToInt32(distance);
                        if (_roundedDistance > 175)
                        {
                            Ped.Position = MG_Player.Ped.Position.Around(90f);
                            Ped.Task.FightAgainst(MG_Player.Ped);
                            Ped.AlwaysKeepTask = true;


                            if (MG_Player.Ped.IsInVehicle())
                            {

                                if (MG_Player.Ped.CurrentVehicle.FuelLevel > 0)
                                {
                                    //if (MG_Random.Random() < 50)
                                    // {
                                    MG_Player.Ped.CurrentVehicle.FuelLevel = 0;
                                    //}
                                }
                            }
                            SayFarAway();
                            Wait(4000);
                        }
                        else
                        {

                            if (MG_Player.Player.IsAiming)
                            {
                                ///---------NEW
                                if (_roundedDistance < 45)
                                {

                                    if (DB_Weapons.BannedWeaponsByHitman.Contains(MG_Player.Ped.Weapons.Current.Hash))
                                    {
                                        //Ped.AddBlip();
                                        Weapon currentWeapon = MG_Player.Ped.Weapons.Current;
                                        if (currentWeapon.Ammo > 0)
                                        {

                                            currentWeapon.Ammo = 0;
                                            //weaponProp.Detach();
                                            World.ShootBullet(MG_Player.Ped.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 0.1f), MG_Player.Ped.GetBoneCoord(Bone.SKEL_Head), Ped, WeaponHash.StunGun, 0);
                                            //weaponProp.MarkAsNoLongerNeeded();
                                            SayFarAway();
                                        }
                                    }
                                }
                                ///---------NEW

                                if (MG_Player.Player.GetTargetedEntity() == Ped)
                                {
                                    if (MG_Player.Ped.IsShooting)
                                    {
                                        _shots += 1;
                                        //MG_Message.SubTitle("_shots=" + _shots, 10000);
                                    }
                                }
                            }

                            if (_shots > KilledTimes)
                            {

                                if (Ped.IsBulletProof)
                                {
                                    Ped.IsBulletProof = false;
                                    //MG_Message.SubTitle("_shots=" + _shots + " Ped.IsBulletProof!!!!!", 10000);
                                }

                                //if (Ped.IsBeingStunned || Ped.IsRagdoll)
                                //{
                                //    Ped.IsBulletProof = true;
                                //    Ped.Health = 100;
                                //    Ped.Armor = 100;
                                //    _shots = 0;
                                //    Wait(5000);
                                //}
                            }
                        }
                    }
                    else
                    {
                        KilledTimes++;
                        if (KilledTimes > 5)//9
                        {
                            GAME_OVER_FOR_HITMAN();
                            Wait(1500);
                            Ped.MaxHealth = 250;
                            Ped.Health = 250;
                            Ped.Armor = 100;
                            Ped.IsBulletProof = true;
                            Ped.Task.ClearAllImmediately();

                            Function.Call(Hash.SET_PED_CAN_RAGDOLL, Ped, true);
                            Function.Call(Hash.SET_PED_TO_RAGDOLL, Ped, 555, 555, 0, true, true, false);//http://www.kronzky.info/fivemwiki/index.php/SetPedToRagdoll

                            Ped.Task.PlayAnimation("get_up@standard", "back");

                            Ped.DropsWeaponsOnDeath = false;
                            Script.Wait(500);
                            Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, Ped, true);
                            Function.Call(Hash.STOP_PED_SPEAKING, Ped, true);
                            Script.Wait(500);
                            Ped.IsPersistent = false;

                            Ped.Task.FleeFrom(MG_Player.Ped);
                            Ped.MarkAsNoLongerNeeded();
                            Ped = null;
                        }
                        else
                        {
                            //Ped.IsPersistent = false;
                            //Ped.CurrentBlip.Remove();
                            //bool isPlayerStunned = false;
                            //CreateHitman();
                            SayNiceWork();
                            Wait(1500);
                            Ped.MaxHealth = 250;
                            Ped.Health = 250;
                            Ped.Armor = 100;
                            Ped.Armor = 100;
                            Ped.IsBulletProof = true;

                            int Group = World.AddRelationshipGroup("HITMAN_" + KilledTimes);
                            int RelationsGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HITMAN_" + KilledTimes);
                            Group = Function.Call<int>(Hash.CREATE_GROUP, Group);
                            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, Ped, Group);
                            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, Ped, Group);

                            int copHash = Function.Call<int>(Hash.GET_HASH_KEY, "COP");
                            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, copHash);
                            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, copHash, RelationsGroup);
                            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, MG_Player.RelationsGroup);
                            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_Player.RelationsGroup, RelationsGroup);

                            Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, Ped, true);
                            Function.Call(Hash.STOP_PED_SPEAKING, Ped, true);
                            Function.Call(Hash.SET_PED_ALLOWED_TO_DUCK, Ped, false);
                            Function.Call(Hash.SET_PED_TO_LOAD_COVER, Ped, false);
                            Function.Call(Hash.SET_PED_CAN_COWER_IN_COVER, Ped, false);
                            Function.Call(Hash.SET_PED_CAN_PEEK_IN_COVER, Ped, false);
                            Function.Call(Hash.SET_PED_CAN_EVASIVE_DIVE, Ped, false);
                            Function.Call(Hash.SET_PED_AS_ENEMY, Ped, true);
                            Function.Call(Hash.SET_PED_MOVE_RATE_OVERRIDE, Ped, 5.0);
                            Function.Call(Hash.SET_PED_PATH_AVOID_FIRE, Ped, false);
                            Function.Call(Hash.SET_PED_COMBAT_RANGE, Ped, 40);

                            Ped.IsOnlyDamagedByPlayer = true;
                            Ped.CanSufferCriticalHits.Equals(true);
                            //ped.CanWrithe = false;
                            Ped.IsBulletProof = true;
                            Ped.IsMeleeProof = true;
                            Ped.Task.ClearAllImmediately();
                            Ped.IsPriorityTargetForEnemies = true;
                            //Ped.Task.FightAgainst(MG_Player.Ped);
                            Ped.AlwaysKeepTask = true;
                            Ped.CanWrithe = false;
                            Ped.DropsWeaponsOnDeath = false;
                            if (Vector2.Distance(Ped.Position, MG_Player.Ped.Position) < 15)
                            {
                                World.ShootBullet(MG_Player.Ped.GetBoneCoord(Bone.SKEL_Head) + new Vector3(0, 0, 0.1f), MG_Player.Ped.GetBoneCoord(Bone.SKEL_Head), Ped, WeaponHash.StunGun, 0);
                                //isPlayerStunned = true;
                                //Ped.Weapons.Give(WeaponHash.APPistol, 99999, true, true);
                                //Ped.Weapons.Current.SetComponent(WeaponComponent.AtPiSupp, true);
                                Ped.Weapons.Give(WeaponHash.MicroSMG, 99999, true, true);
                                Ped.Weapons.Current.SetComponent(WeaponComponent.AtArSupp02, true);
                            }
                            else// if (Vector2.Distance(Ped.Position, MG_Player.Ped.Position) > 50)
                            {
                                Ped.Weapons.Give(WeaponHash.MarksmanRifleMk2, 99999, true, true);
                                Ped.Weapons.Current.SetComponent(WeaponComponent.AtArSupp, true);//COMPONENT_AT_AR_SUPP

                            }
                            //else
                            //{
                            //    Ped.Weapons.Give(WeaponHash.MicroSMG, 99999999, true, true);
                            //    Ped.Weapons.Current.SetComponent(WeaponComponent.AtArSupp02, true);
                            //}
                            //Ped.Task.PlayAnimation("@aim_from_ground@pistol", "front");
                            //Ped.Task.PlayAnimation("get_up@standard", "front");
                            //Ped.Task.PlayAnimation("get_up@standard", "back");
                            Ped.Task.PlayAnimation("get_up@directional_sweep@combat@pistol@back", "get_up_0");
                            //Function.Call(Hash.SET_PED_CAN_RAGDOLL, Ped, true);
                            //Function.Call(Hash.SET_PED_TO_RAGDOLL, Ped, 1000, 1000, 0, 0, 0, 0);//http://www.kronzky.info/fivemwiki/index.php/SetPedToRagdoll


                            //Pe

                            Wait(1000);
                            Ped.Task.FightAgainst(MG_Player.Ped);
                            Wait(3000);
                            _shots = 0;
                        }
                    }

                    if (MG_Player.Ped.IsDead)
                    {
                        PlayerKilled = true;
                        MG_Audio.Stop(3);

                        Ped.Task.ClearAll();
                        Ped.IsPersistent = false;
                        Ped.MarkAsNoLongerNeeded();
                    }


                }
                Wait(10);
            }
        }
        #endregion OnTick

        #region OnKeyDown
        #endregion OnKeyDown

        #region Public Methods
        public static void AngryTalk()
        {
            MG_Player.IsUsingCellphone = true;
            Ped character = MG_Player.Ped;
            MG_Ped.PlayCellphoneAnim(character);
            MG_Ped.HideWeapon(character);
            var propPhone = MG_Ped.CreateAndAttachPhone(character);

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

            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }




            MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: Alirght, listen up, sucker! There is one guy who wants to say something to you!", 10000);
            MG_Audio.Play(MG_Advisor._Angry1, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }


            Wait(2000);

            //MG_Message.SubTitle("~g~" + MG_Settings.INI_AdvisorName + "~w~: ...(Talking to another person)", 10000);
            //MG_Audio.Play(MG_Advisor._Angry2, 0, false);
            //while (MG_Audio.IsPlaying(0) == false) Wait(100);
            //while (MG_Audio.IsPlaying(0) == true) Wait(100);
            //if (!_character.IsAlive)
            //{
            //    MG_Ped.BreakPhoneCall(_character, _propPhone);
            //    return;
            //}

            MG_Audio.Play(MG_Advisor._Angry3, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(500);
            MG_Message.SubTitle("~r~???~w~: Sir? Excuse me.", 10000);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }


            MG_Message.SubTitle("~r~???~w~: Now give me one good reason why I shouldn't put a bullet in your head?", 10000);
            //MG_Message.SubTitle("~r~???~w~: NOW GIVE ME ONE good REASON WHY I SHOULDN'T PUT A BULLET IN YOUR HEAD?", 10000);
            MG_Audio.Play(MG_Advisor._Angry5, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }

            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_CURSE_MED", "SPEECH_PARAMS_FORCE");
            Wait(2555);
            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");
            Wait(2500);

            MG_Message.SubTitle("~r~???~w~: Your contract is now terminated.", 10000);
            MG_Audio.Play(MG_Advisor._Angry4, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }


            MG_Message.SubTitle("~r~???~w~: So... I like to get my hands dirty!", 10000);
            MG_Audio.Play(MG_Advisor._Angry6, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }

            MG_Message.SubTitle("YOU ARE GOING TO DIE!", 10000);
            MG_Audio.Play(MG_Advisor._Angry7, 1, false);
            Wait(50);
            if (!character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(character, propPhone);
                return;
            }
            Wait(1500);
            //MG_Player.Player.CanControlCharacter = false;
            if (MG_Player.Ped.IsInVehicle() == true)
            {
                MG_Player.Ped.CurrentVehicle.FuelLevel = 0;
                Wait(1000);
            }
            MG_Ped.DisableCellphoneAnim(character);
            MG_Player.Ped.Task.ReactAndFlee(MG_Player.Ped);

            //MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            //while (MG_Audio.IsPlaying(0) == false) Wait(100
            propPhone.Detach();

            for (int i = 0; i < 25; i++)
            {
                MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                Wait(100);
                MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                Wait(100);
            }

            var hours = World.CurrentDayTime.Hours;
            //MG_Message.SubTitle("" + hours);
            //Function.Call(Hash.ADVANCE_CLOCK_TIME_TO, 0, 0, 0);

            if (hours == 0 || hours == 1 || hours == 2 || hours == 3)
            {
                for (int i = 0; i < 75; i++)
                {
                    MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                    Wait(100);
                    MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                    Wait(100);
                }
                //Wait(3000);
            }
            else //if ((hours > 3 && hours < 16))
            {
                while (hours > 0)
                {
                    hours = World.CurrentDayTime.Hours;
                    int forward;
                    if (hours < 19)
                    {
                        forward = 6;//12
                    }
                    else //if(hours < 19)
                    {
                        forward = 2;//5
                    }

                    Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                    Wait(50);
                    MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                    Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                    Wait(50);
                    MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                }
            }



            //MG_Ped.ShowWeapon(_character);

            //MG_Player.Player.CanControlCharacter = true;
            MG_Player.Ped.Task.ClearAll();

            MG_Message.SubTitle("~r~AGENT 47~w~ IS COMING FOR YOU~w~!", 10000);
            //aa.Hours
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            MG_Audio.Play(MG_Advisor._Horror, 3, true);
            while (MG_Audio.IsPlaying(3) == false) Wait(100);
            propPhone.Delete();
            CreateHitman();
            MG_Player.IsUsingCellphone = false;
        }

        public static void AngryTalkTest()
        {
            var hours = World.CurrentDayTime.Hours;
            int forward = 10;
            MG_Audio.Play(MG_Advisor._Angry4, 0, false);
            while (hours > 0)
            {
                hours = World.CurrentDayTime.Hours;

                Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                Wait(50);
                MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                Wait(50);
                MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
            }
            MG_Message.SubTitle("~r~AGENT 47~w~ IS COMING FOR YOU~w~!", 10000);
            CreateHitman();

        }

        public static void CreateHitman()
        {
            _shots = 0;
            string pedModelStr = "mp_m_freemode_01";//s_m_m_highsec_01 mp_m_freemode_01
            Model pedModel = new Model(pedModelStr);
            pedModel.Request(500);
            Ped ped = World.CreatePed(pedModelStr, MG_Player.Ped.Position.Around(125f));
            Wait(100);
            Ped = ped;
            ped.IsPersistent = true;
            //ped.AddBlip();
            //ped.Weapons.Give(weapons[KilledTimes], 999, true, false);
            //WeaponHash weapon = MG_Random.RandomElement(weapons);

            Ped.Weapons.Give(WeaponHash.MarksmanRifleMk2, 99999999, true, true);
            Ped.Weapons.Current.SetComponent(WeaponComponent.AtArSupp, true);//COMPONENT_AT_AR_SUPP


            ped.Accuracy = 100;
            ped.Armor = 100;
            ped.MaxHealth = 250;
            ped.Health = 250;
            //ped.Health = 100;
            ped.FiringPattern = FiringPattern.FullAuto;
            ped.DropsWeaponsOnDeath = false;
            Ped.CanWrithe = false;

            //0: Face\ 1: Mask\ 2: Hair\ 3: Torso\ 4: Leg\ 5: Parachute / bag\ 6: Shoes\ 7: Accessory\ 8: Undershirt\ 9: Kevlar\ 10: Badge\ 11: Torso 2
            //for (int i = 0; i < 1000; i++)
            //{
            //    Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, i, 0, 0);
            //    MG_Message.SubTitle("" + i);
            //    Wait(50);
            //}
            int tid = 0;

            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 0, 0, 0, tid); //head

            if (Agent47WithRandomMask)
            {
                int chosenMask = MG_Random.RandomElement(Masks);
                Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 1, chosenMask, 0, tid); //beard (masks)
            }

            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 2, 0, 0, tid); //hair
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 3, 52, 0, tid); //torso (variar según aux.torso y color de piel - también da guantes)
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 4, 34, 0, tid); //legs
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 5, 0, 0, tid); //hands (paracaídas)
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 6, 24, 0, tid); //foot
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 7, 0, 0, tid); //teeth
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 8, 58, 0, tid); //accesories1 (
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 9, 0, 0, tid); //accesories2 (armaduras)
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 10, 0, 0, tid); //decals
            Function.Call(Hash.SET_PED_COMPONENT_VARIATION, ped, 11, 55, 0, tid); //aux.torso (partes de arriba reales)

            int Group = World.AddRelationshipGroup("HITMAN_" + KilledTimes);
            int RelationsGroup = Function.Call<int>(Hash.GET_HASH_KEY, "HITMAN_" + KilledTimes);
            Group = Function.Call<int>(Hash.CREATE_GROUP, Group);
            Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, Group);
            Function.Call(Hash.SET_PED_AS_GROUP_LEADER, ped, Group);

            int copHash = Function.Call<int>(Hash.GET_HASH_KEY, "COP");
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, RelationsGroup, copHash);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 0, copHash, RelationsGroup);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, RelationsGroup, MG_Player.RelationsGroup);
            Function.Call(Hash.SET_RELATIONSHIP_BETWEEN_GROUPS, 5, MG_Player.RelationsGroup, RelationsGroup);

            Function.Call(Hash.DISABLE_PED_PAIN_AUDIO, ped, true);
            Function.Call(Hash.STOP_PED_SPEAKING, ped, true);
            Function.Call(Hash.SET_PED_ALLOWED_TO_DUCK, ped, false);
            Function.Call(Hash.SET_PED_TO_LOAD_COVER, ped, false);
            Function.Call(Hash.SET_PED_CAN_COWER_IN_COVER, ped, false);
            Function.Call(Hash.SET_PED_CAN_PEEK_IN_COVER, ped, false);
            Function.Call(Hash.SET_PED_CAN_EVASIVE_DIVE, ped, false);
            Function.Call(Hash.SET_PED_AS_ENEMY, ped, true);
            Function.Call(Hash.SET_PED_MOVE_RATE_OVERRIDE, ped, 5.0);
            Function.Call(Hash.SET_PED_PATH_AVOID_FIRE, ped, false);
            Function.Call(Hash.SET_PED_COMBAT_RANGE, ped, 40);

            ped.IsOnlyDamagedByPlayer = true;
            ped.CanSufferCriticalHits.Equals(true);
            //ped.CanWrithe = false;
            ped.IsBulletProof = true;
            ped.IsMeleeProof = true;
            ped.IsPriorityTargetForEnemies = true;
            ped.Task.FightAgainst(MG_Player.Ped);
            ped.AlwaysKeepTask = true;
        }

        public static void CheckToActivateHitmanAfterSuccess()
        {
            if (ArrayOfSuccessfulMissions.Contains(MG_Statistic.TotalTargetsEliminated))
            {
                IsTarget = true;
            }
        }

        public static void HitmanCall()
        {
            Ped _character = MG_Player.Ped;

            MG_Player.IsUsingCellphone = true;

            MadeMad = true;
            CanceledMissionsCount = 10;
            MG_AssassinationMission.IsJobActive = false;

            MG_Audio.Play(MG_Advisor._PhoneCall, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);

            Wait(2500);
            if (!_character.IsAlive)
            {
                return;
            }

            Prop propPhone = MG_Ped.CreateAndAttachPhone(_character);
            MG_Ped.PlayCellphoneAnim(_character);
            MG_Ped.HideWeapon(_character);

            Wait(500);
            MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);

            Wait(1000);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, propPhone);
                return;
            }


            MG_Audio.Play(MG_Advisor._Angry3, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            Wait(500);
            MG_Message.SubTitle("~r~???~w~: Sir? Excuse me.", 10000);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, propPhone);
                return;
            }


            MG_Message.SubTitle("~r~???~w~: Now give me one good reason why I shouldn't put a bullet in your head?", 10000);
            //MG_Message.SubTitle("~r~???~w~: NOW GIVE ME ONE good REASON WHY I SHOULDN'T PUT A BULLET IN YOUR HEAD?", 10000);
            MG_Audio.Play(MG_Advisor._Angry5, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, propPhone);
                return;
            }

            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_CURSE_MED", "SPEECH_PARAMS_FORCE");
            Wait(2555);
            GTA.Native.Function.Call(GTA.Native.Hash._PLAY_AMBIENT_SPEECH1, MG_Player.Ped, "GENERIC_FUCK_YOU", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL");
            Wait(2500);

            //MG_Message.SubTitle("~r~???~w~: Your contract is now terminated.", 10000);
            //MG_Audio.Play(MG_Advisor._Angry4, 0, false);
            //while (MG_Audio.IsPlaying(0) == false) Wait(100);
            //while (MG_Audio.IsPlaying(0) == true) Wait(100);
            //if (!_character.IsAlive)
            //{
            //    MG_Ped.BreakPhoneCall(_character, propPhone);
            //    return;
            //}


            MG_Message.SubTitle("~r~???~w~: So... I like to get my hands dirty!", 10000);
            MG_Audio.Play(MG_Advisor._Angry6, 0, false);
            while (MG_Audio.IsPlaying(0) == false) Wait(100);
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, propPhone);
                return;
            }

            MG_Message.SubTitle("YOU ARE GOING TO DIE!", 10000);
            MG_Audio.Play(MG_Advisor._Angry7, 1, false);
            Wait(50);
            if (!_character.IsAlive)
            {
                MG_Ped.BreakPhoneCall(_character, propPhone);
                return;
            }
            Wait(1500);
            //MG_Player.Player.CanControlCharacter = false;
            if (MG_Player.Ped.IsInVehicle() == true)
            {
                MG_Player.Ped.CurrentVehicle.FuelLevel = 0;
                Wait(1000);
            }
            MG_Ped.DisableCellphoneAnim(_character);
            MG_Player.Ped.Task.ReactAndFlee(MG_Player.Ped);

            //MG_Audio.Play(MG_Advisor._ButtonPressed, 0, false);
            //while (MG_Audio.IsPlaying(0) == false) Wait(100
            propPhone.Detach();

            for (int i = 0; i < 25; i++)
            {
                MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                Wait(100);
                MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                Wait(100);
            }

            var hours = World.CurrentDayTime.Hours;
            //MG_Message.SubTitle("" + hours);
            //Function.Call(Hash.ADVANCE_CLOCK_TIME_TO, 0, 0, 0);

            if (hours == 0 || hours == 1 || hours == 2 || hours == 3)
            {
                for (int i = 0; i < 75; i++)
                {
                    MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                    Wait(100);
                    MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                    Wait(100);
                }
                //Wait(3000);
            }
            else //if ((hours > 3 && hours < 16))
            {
                while (hours > 0)
                {
                    hours = World.CurrentDayTime.Hours;
                    int forward;
                    if (hours < 19)
                    {
                        forward = 6;//12
                    }
                    else //if(hours < 19)
                    {
                        forward = 2;//5
                    }

                    Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                    Wait(50);
                    MG_Message.SubTitle("~o~YOU ARE GOING TO DIE!");
                    Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                    Wait(50);
                    MG_Message.SubTitle("~r~YOU ARE GOING TO DIE!");
                }
            }



            //MG_Ped.ShowWeapon(_character);

            //MG_Player.Player.CanControlCharacter = true;
            MG_Player.Ped.Task.ClearAll();

            MG_Message.SubTitle("~r~AGENT 47~w~ IS COMING FOR YOU~w~!", 10000);
            //aa.Hours
            while (MG_Audio.IsPlaying(0) == true) Wait(100);
            MG_Audio.Play(MG_Advisor._Horror, 3, true);
            while (MG_Audio.IsPlaying(3) == false) Wait(100);
            propPhone.Delete();
            CreateHitman();
            MG_Player.IsUsingCellphone = false;
        }

        #endregion Public Methods

        #region Private Methods

        public static void SayNiceWork()
        {
            if (_said_FarAway_TEXT.Any() == false)
            {
                _said_FarAway_TEXT = new List<string>(NiceWork_Variants.Keys);
                _said_FarAway_VOICE = new List<string>(NiceWork_Variants.Values);
            }

            int randomNum = MG_Random.Random(0, _said_FarAway_TEXT.Count);
            string chosenText = _said_FarAway_TEXT[randomNum];
            string chosenVoice = _said_FarAway_VOICE[randomNum];

            _said_FarAway_TEXT.Remove(chosenText);
            _said_FarAway_VOICE.Remove(chosenVoice);

            if (MG_Advisor.DisableMessageReceivedVoice == false)
            {
                MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                while (MG_Audio.IsPlaying(1) == false) Wait(100);
                while (MG_Audio.IsPlaying(1) == true) Wait(100);
            }
           
            MG_Audio.Play(chosenVoice, 2, false);
            MG_Message.SubTitle("~r~Agent 47~w~: " + chosenText, 4000);
        }

        public static void SayFarAway()
        {
            if (_said_GoodWork_TEXT.Any() == false)
            {
                _said_GoodWork_TEXT = new List<string>(TooFar_Variants.Keys);
                _said_GoodWork_VOICE = new List<string>(TooFar_Variants.Values);
            }

            int randomNum = MG_Random.Random(0, _said_GoodWork_TEXT.Count);
            string chosenText = _said_GoodWork_TEXT[randomNum];
            string chosenVoice = _said_GoodWork_VOICE[randomNum];

            _said_GoodWork_TEXT.Remove(chosenText);
            _said_GoodWork_VOICE.Remove(chosenVoice);

            if (MG_Advisor.DisableMessageReceivedVoice == false)
            {
                MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                while (MG_Audio.IsPlaying(1) == false) Wait(100);
                while (MG_Audio.IsPlaying(1) == true) Wait(100);
            }
            MG_Audio.Play(chosenVoice, 2, false);
            MG_Message.SubTitle("~r~Agent 47~w~: " + chosenText, 4000);
        }




        private static void CheckNightTime()
        {
            var hours = World.CurrentDayTime.Hours;
            //MG_Message.SubTitle("" + hours);
            //Function.Call(Hash.ADVANCE_CLOCK_TIME_TO, 0, 0, 0);

            if ((hours == 0 || hours == 1 || hours == 2 || hours == 3 || hours == 4) == false)
            {
                while (hours > 0)
                {
                    hours = World.CurrentDayTime.Hours;
                    int forward;
                    if (hours < 19)
                    {
                        forward = 12;
                    }
                    else //if(hours < 19)
                    {
                        forward = 5;
                    }
                    Function.Call(Hash.ADD_TO_CLOCK_TIME, 0, forward, 0);
                    Wait(50);
                }
            }
        }

        private static void GAME_OVER_FOR_HITMAN()
        {
            MG_Audio.Stop(3);

            if (MG_Advisor.DisableMessageReceivedVoice == false)
            {
                MG_Audio.Play(MG_Advisor._MessageReceived, 1, false);
                while (MG_Audio.IsPlaying(1) == false) Wait(100);
                while (MG_Audio.IsPlaying(1) == true) Wait(100);
            }
            MG_Audio.Play(_YOU_WIN_VOICE, 2, false);
            MG_Message.SubTitle("~r~Agent 47~w~: " + _YOU_WIN_TEXT, 4000);
        }

        #endregion Private Methods

    }
}
