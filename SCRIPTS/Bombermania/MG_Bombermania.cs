////////////////////////////////////////////////////////////////////////////////
//
//	MG_Bombermania.cs
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

namespace MG_Liquidator
{
    public class MG_Bombermania : Script
    {
        #region Fields
        private static string _bomberSound = @"scripts\HW_LIQUIDATOR2021\SOUND\Bomb.mp3";
        #endregion Fields

        #region Properties     
        public static List<Ped> Bombers { get; set; } = new List<Ped>();
        public static bool BombersSpawned { get; set; } = false;
        public static bool IsBombersAttack { get; set; } = false;

        public static int Distance_to_blow { get; set; } = 7;
        public static int SuicideVest_power_of_explosion { get; set; } = 10;
        public static int CarBomb_power_of_explosion { get; set; } = 15;


        #endregion Properties

        #region Constructor
        public MG_Bombermania()
        {
            Tick += OnTick;
        }
        #endregion Constructor

        #region OnTick
        private void OnTick(object sender, EventArgs e)
        {
            //if (MG_Main.isTargetCompromised)
            //{
            if (BombersSpawned)
            {
                if (MG_TargetAI.IsCompromised)
                {
                    CheckToBlow();
                }

            }
            // }
            // else
            //{
            //  if (MG_Main.BombersSpawned)
            //       MG_Main.BombersSpawned = false;
            //  }
        }
        #endregion OnTick

        #region Public Methods
        public static bool IsCloseToBlow(Ped target, Ped player)
        {
            float distance = Vector2.Distance(target.Position, player.Position);
            if (distance < Distance_to_blow)
            {
                return true;
            }
            return false;
        }

        public static void BOOM(Ped ped)
        {
            //MG_File.Debug(MG_Target.Type.ToString());//DEL
            //if (MG_Target.Type.Equals(TargetType.Terrorist) == false)//DEL
            //{
            //    MG_Target.Ped = null;//DEL
            //    MG_Target.Ped.CurrentVehicle.EngineHealth = 0;//DEL
            //}//DEL

            World.AddExplosion(ped.Position, ExplosionType.PlaneRocket, SuicideVest_power_of_explosion, 1);
            if (Bombers.Contains(ped))
            {
                Bombers.Remove(ped);
            }
        }

        public static void BOOM(Vehicle vehicle)
        {
            //MG_File.Debug(MG_Target.Type.ToString());//DEL
            //if (MG_Target.Type.Equals(TargetType.Terrorist) == false)//DEL
            //{
            //    MG_Target.Ped = null;//DEL
            //    MG_Target.Ped.CurrentVehicle.EngineHealth = 0;//DEL
            //}//DEL
            World.AddExplosion(vehicle.Position, ExplosionType.PlaneRocket, CarBomb_power_of_explosion, 1);
        }

        public static void MakeSuicideBomber(Ped ped)
        {
            //ped.Weapons.RemoveAll();
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, _ped, 17, false);//BF_CanDoDrivebys = 2 FALSE; 17 DONT SHOOT! 46 FIGHT TO DEATH
            //Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, _ped, 17, true);//BF_CanDoDrivebys = 2 FALSE; 17 DONT SHOOT! 46 FIGHT TO DEATH
            //Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, _ped, 65536, 1);//
            //Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, ped, true);

            Bombers.Add(ped);
            if (!BombersSpawned)
            {
                BombersSpawned = true;
            }
            //Attack(ped);
        }

        public static void DetonateEveryBomber()
        {
            foreach (var ped in MG_TargetBodyGuards.Bodyguards.ToList())
            {
                if (ped.IsAlive)
                {
                    BOOM(ped);
                    Wait(500);
                }
            }
        }

        public static void DetonateRandomCars()
        {
            Ped target = MG_Target.Ped;
            Ped player = MG_Player.Ped;

            var vehicles = World.GetNearbyVehicles(player.Position, 250);
            foreach (var vehicle in vehicles)
            {
                if (target.IsDead)
                    break;

                //float distance = Vector2.Distance(vehicle.Position, target.Position);
                if (
                    vehicle != null
                     && vehicle.IsAlive
                    //&& !vehicle.IsOnFire
                    //&& !vehicle.IsUpsideDown
                    //&& vehicle.IsDriveable
                    //&& player.CurrentVehicle != vehicle
                    && !vehicle.Model.IsTrain
                    && !vehicle.Model.IsHelicopter
                    && !vehicle.Model.IsPlane
                    && !vehicle.Model.IsBoat
                    && vehicle.Driver != MG_Player.Ped
                    && vehicle.Driver != MG_Target.Ped
                    && Vector2.Distance(target.Position, vehicle.Position) > 25f
                    //&& distance > 35
                    )
                {
                    if (MG_Random.Random() > 25)
                    {
                        BOOM(vehicle);
                        Wait(500);
                    }
                }
            }
        }

        public static void Reset()
        {
            BombersSpawned = false;
            if (Bombers.Count > 0)
            {
                foreach (var _ped in Bombers)
                {
                    _ped.CurrentBlip.Remove();
                    _ped.IsPersistent = false;
                    //----_ped.MarkAsNoLongerNeeded();
                }
                Bombers.Clear();
            }
        }
        #endregion Public Methods

        #region Private Methods
        private static void CheckToBlow()
        {

            foreach (var ped in Bombers.ToList())
            {
                bool isClosed = IsCloseToBlow(ped, MG_Player.Ped);
                bool isDead = ped.IsDead;

                if (isClosed || isDead)
                {
                    if (isDead)
                    {
                        if (MG_Random.Random() < 75)
                        {
                            Bombers.Remove(ped);
                            continue;
                        }
                    }
                    else
                    {
                        bool isClosedToTarget = IsCloseToBlow(ped, MG_Target.Ped);
                        if (isClosedToTarget) continue;//Avoid killing the target

                        //SOUND
                        MG_Audio.Play(_bomberSound, 0, false);
                        while (MG_Audio.IsPlaying(0) == false) Wait(100);
                        while (MG_Audio.IsPlaying(0) == true) Wait(100);
                    }

                    BOOM(ped);
                    if (Bombers.Count == 0) BombersSpawned = false;
                }
            }
        }

        //private static void Attack(Ped _ped)
        //{
        //    Ped _player = MG_Player.PlayerPed;
        //    Function.Call(Hash.TASK_SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, _ped, true);
        //    Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, _ped, 0, 0);
        //    Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, _ped, 46, true);
        //    // Function.Call(Hash.SET_PED_COMBAT_MOVEMENT, _ped, 3);
        //    _ped.LeaveGroup();
        //    //----Function.Call(Hash.SET_PED_RELATIONSHIP_GROUP_HASH, _ped, Function.Call<int>(Hash.GET_HASH_KEY, "cougar"));

        //    if (_ped.IsOnFoot)
        //    {
        //        //-----------------------------_ped.Task.ClearAll();
        //        //Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, _ped, Game.Player.Character, 0, 0, 0f, 30f, (-1), 10f, 1);
        //        Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, _ped, Game.Player.Character, 0, 0, 0, 50, (-1), 10, 1);
        //        //void TaskFollowToOffsetOfEntity(int /* Ped */ ped, int /* Entity */ entity, float offsetX, float offsetY, float offsetZ, float movementSpeed, int timeout, float stoppingRange, bool persistFollowing);
        //        //p6 always -1
        //        //p7 always 10.0
        //        //p8 always 1
        //    }
        //    else
        //    {
        //        //РАЗДАВИТЬ НАХРЕН ИГРОКА!
        //        Function.Call(Hash._TASK_VEHICLE_FOLLOW, _ped, _ped.CurrentVehicle, _player, 300f, DrivingStyle.AvoidTrafficExtremely.GetHashCode(), 1);
        //    }

        //}

        #endregion Private Methods

        //var position = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 5, 0));                  

        //public static void BOOM(Vector3 _pos)
        //{
        //    World.AddExplosion(_pos, ExplosionType.PlaneRocket, 20, 1);
        //}

    }

}
