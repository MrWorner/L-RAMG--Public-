////////////////////////////////////////////////////////////////////////////////
//
//	MG_Camera.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using GTA;
//using GTA.Math;
//using GTA.Native;

//namespace MG_Liquidator
//{
//    class MG_Camera : Script
//    {

//        #region Fields

//        #endregion Fields

//        #region Properties

//        #endregion Properties

//        #region Constructor

//        public MG_Camera()
//        {
//            //var p = Game.Player.Character.Position;
//            //myCamera = World.CreateCamera(p + Game.Player.Character.ForwardVector * 1, Vector3.Zero, GameplayCamera.FieldOfView);
//            //World.RenderingCamera = myCamera;
//            //myCamera.AttachTo(Game.Player.Character, new Vector3(0, 1, 1));

//            //Tick += OnTick;
//        }
//        #endregion Constructor

//        #region OnTick
//        /// <summary>
//        /// Тик
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        private void OnTick(object sender, EventArgs e)
//        {
//            //Test();
//        }
//        #endregion OnTick

//        #region Public Methods
//        public static void Test()
//        {
//            Ped playerPed = MG_Player.Ped;
//            World.DestroyAllCameras();
//            var p = Game.Player.Character.Position;
//            // Camera myCamera = World.CreateCamera(p + Game.Player.Character.ForwardVector * 1, Vector3.Zero, GameplayCamera.FieldOfView);

//            var posAround = playerPed.Position;
//            Camera myCamera = CreateCamera(true, new Vector3(posAround.X - 3, posAround.Y - 3, posAround.Z + 1));

//            //myCamera.AttachTo(Game.Player.Character, new Vector3(0, 1, 1));
//            //myCamera.AttachTo(Game.Player.Character, new Vector3(0, 0, 0.25f));


//            ////if (Camera != null) Camera.Destroy();
//            //var p = Game.Player.Character.Position;

//            ////Camera Camera = World.CreateCamera(p + Game.Player.Character.ForwardVector * 1, Vector3.Zero, GameplayCamera.FieldOfView);
//            ////Vector3 PositionToSpawnAt = p + Game.Player.Character.ForwardVector * 8f;

//            ////Camera.AttachTo(Game.Player.Character, Game.Player.Character.ForwardVector * 2f);
//            ////Camera.PointAt(Game.Player.Character.Position);
//            ////Wait(1000);







//            //Ped[] peds = World.GetNearbyPeds(playerPed, 200);//1000
//            //List<Ped> candidates = new List<Ped>();
//            //for (int i = 0; i < peds.Length; i++)
//            //{
//            //    Ped ped = peds[i];
//            //    Vector3 pedPos = ped.Position;
//            //    if
//            //     (
//            //     ped.IsHuman
//            //     && ped.Exists()
//            //     && ped.IsAlive
//            //     && !ped.IsRagdoll
//            //     && !ped.IsFalling
//            //     //&& !_ped.IsInjured
//            //     //&& !_ped.IsInCombat
//            //     //&& !_ped.IsFleeing
//            //     //---------&& ped.IsInVehicle()//TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//            //     && !ped.IsInTrain
//            //     && !ped.IsInHeli
//            //     && !ped.IsInPlane
//            //     && !ped.IsInBoat
//            //     && !ped.IsInSub//NEW
//            //                    //&& ped.Gender == Gender.Male///DEL!!!!!!!!!!!!!!!
//            //     && !ped.IsInParachuteFreeFall//NEW
//            //     && !ped.IsInPoliceVehicle//NEW
//            //     && !ped.IsOccluded//ПОХОЖЕ ИСПРАВЛЯЕТ БАГ!!!!!!!!!!!!!!!!
//            //                       //&& ped.IsIdle//NEW
//            //                       //&& ped.IsOnScreen//NEW
//            //     && World.GetDistance(playerPed.Position, pedPos) > 50//45
//            //     && World.GetDistance(playerPed.Position, pedPos) < 1000//1000
//            //     )
//            //    {
//            //        candidates.Add(ped);

//            //    }
//            //}

//            //var candidates = MG_TargetBodyGuards.Bodyguards;



//            List<Ped> candidates = new List<Ped>() {  };

//            foreach (var vehicle in MG_Watchers.Vehicles)
//            {
//                candidates.Add(vehicle.Driver);
//            }


//            int x = 2;
//            int y = 2;
//            int z = 2;

//            int n = 0;

//            List<Vector3> camPoses = new List<Vector3>()
//            {
//               new Vector3(x+n, n, z),
//               new Vector3(x+n, n, z),

//               new Vector3(x+n, y +n, z),
//               new Vector3(x+n, y +n, z),
//               new Vector3(n, y +n, z),
//               new Vector3(n, y +n, z),

//               new Vector3(n - x, y +n, z),
//               new Vector3(n - x, y +n, z),
//               new Vector3(n - x, n, z),
//               new Vector3(n - x, n, z),
//               new Vector3(n - x, n - y, z),
//               new Vector3(n - x, n - y, z),

//               new Vector3(n, n - y, z),
//               new Vector3(n, n - y, z),
//               new Vector3(n + x, n - y, z),
//               new Vector3(n + x, n - y, z),

//               new Vector3(x+n, n, z),
//               new Vector3(x+n, n, z)

//             };
//            int countCam = 0;

//            Wait(1000);
//            //BEIGN  PLAYER CAMERA
//            posAround = playerPed.Position;
//            Camera myCamera2 = CreateCamera(false, new Vector3(posAround.X + 5, posAround.Y + 5, posAround.Z + 5));

//            myCamera.InterpTo(myCamera2, 1000, true, true);// = Game.Player.Character.Position.Around(15f);

//            for (int i = 0; i < 100; i++)
//            {
//                myCamera2.PointAt(playerPed.Position);
//                myCamera.PointAt(playerPed.Position);
//                //MG_Message.SubTitle("A FOR");
//                Wait(10);
//            }


//            myCamera = myCamera2;
//            Wait(100);

//            posAround = playerPed.Position;
//            myCamera2 = CreateCamera(false, new Vector3(posAround.X + 25, posAround.Y + 25, posAround.Z + 200));

//            myCamera2.PointAt(playerPed.Position);
//            //myCamera2.AttachTo(Game.Player.Character, camPos);
//            myCamera.InterpTo(myCamera2, 1000, true, true);// = Game.Player.Character.Position.Around(15f);
//            for (int i = 0; i < 100; i++)
//            {
//                myCamera2.PointAt(playerPed.Position);
//                myCamera.PointAt(playerPed.Position);
//                //MG_Message.SubTitle("B FOR");
//                Wait(10);
//            }
//            myCamera = myCamera2;
//            //END  PLAYER CAMERA

//            //BEGIN  TARGET CAMERA
//            //Ped target = playerPed;
//            Ped target = MG_Target.Ped;

//            posAround = target.Position;
//            myCamera2 = CreateCamera(false, new Vector3(posAround.X + 25, posAround.Y + 25, posAround.Z + 10));

//            myCamera2.PointAt(target.Position);
//            //myCamera2.AttachTo(Game.Player.Character, camPos);
//            myCamera.InterpTo(myCamera2, 1000, true, true);// = Game.Player.Character.Position.Around(15f);

//            for (int i = 0; i < 100; i++)
//            {
//                myCamera2.PointAt(playerPed.Position);
//                myCamera.PointAt(playerPed.Position);
//                ///MG_Message.SubTitle("target 1 FOR");
//                Wait(10);
//            }
//            myCamera = myCamera2;

//            posAround = target.Position;
//            myCamera2 = CreateCamera(false, new Vector3(posAround.X + 2, posAround.Y + 2, posAround.Z + 3));

//            myCamera2.PointAt(target.Position);
//            //myCamera2.AttachTo(Game.Player.Character, camPos);
//            myCamera.InterpTo(myCamera2, 1000, true, true);// = Game.Player.Character.Position.Around(15f);

//            for (int i = 0; i < 200; i++)
//            {
//                myCamera2.PointAt(playerPed.Position);
//                myCamera.PointAt(playerPed.Position);
//                //--MG_Message.SubTitle("target 2 FOR");
//                Wait(10);
//            }
//            myCamera = myCamera2;
//            //END  TARGET CAMERA

//            //BEGIN WATCHER CAMERA

//            //Ped pedFirst = playerPed;
//            Ped pedFirst = candidates[0];

//            posAround = pedFirst.Position;
//            myCamera2 = CreateCamera(false, new Vector3(posAround.X + 25, posAround.Y + 25, posAround.Z + 50));

//            myCamera2.PointAt(pedFirst.Position);
//            //myCamera2.AttachTo(Game.Player.Character, camPos);
//            myCamera.InterpTo(myCamera2, 3000, true, true);// = Game.Player.Character.Position.Around(15f);

//            if (myCamera.IsInterpolating)
//            {
//                while (myCamera.IsInterpolating)
//                {
//                    Wait(1);
//                    myCamera2.PointAt(pedFirst.Position);
//                    myCamera.PointAt(pedFirst.Position);
//                }
//            }
//            else
//            {
//                for (int i = 0; i < 200; i++)
//                {
//                    myCamera2.PointAt(playerPed.Position);
//                    myCamera.PointAt(playerPed.Position);
//                    //--MG_Message.SubTitle("pedFirst FOR");
//                    Wait(10);
//                }
//            }
//            myCamera = myCamera2;

//            foreach (var ped in candidates)
//            {

//                foreach (var camPos in camPoses)
//                {
//                    posAround = ped.Position;
//                    myCamera2 = CreateCamera(false, new Vector3(posAround.X + camPos.X * countCam, posAround.Y + camPos.Y * countCam, posAround.Z + camPos.Z * countCam));
//                    myCamera2.PointAt(ped.Position);
//                    //myCamera2.AttachTo(Game.Player.Character, camPos);
//                    myCamera.InterpTo(myCamera2, 1200, false, false);// = Game.Player.Character.Position.Around(15f);
//                    //myCamera.Direction = myCamera2.Position;
//                    //Wait(1000);
//                    while (myCamera.IsInterpolating)
//                    {
//                        Wait(1);
//                        //--MG_Message.SubTitle("d=");
//                        myCamera2.PointAt(ped.Position);
//                        myCamera.PointAt(ped.Position);
//                    }

//                    myCamera = myCamera2;
//                    countCam++;
//                }
//                countCam = 0;

//            }

//            //END WATCHER CAMERA




//            //Game.Player.Character.Task.WanderAround();

//            //for (int i = 0; i < 1000; i++)
//            //{
//            //    Wait(1);
//            //    myCamera.PointAt(Game.Player.Character.Position);
//            //}
//            // Wait(2000);
//            //Game.Player.Character.Task.ClearAll();
//            ClearAllCameras();
//            //Camera.Destroy();
//            // World.DestroyAllCameras();

//            //var Position = CurrentNpc.Position + CurrentNpc.Ped.RightVector * 4f;
//            //var mid = (CurrentNpc.Position + Game.Player.Character.Position) / 2;
//            //NpcCamera.PointAt(mid);
//        }
//        #endregion Public Methods

//        #region Private Methods
//        private static void FadeOut(int sec)
//        {
//            Game.FadeScreenOut(sec);
//        }
//        private static void FadeIn(int sec)
//        {
//            Game.FadeScreenIn(sec);
//        }
//        private static Camera CreateCamera(bool activate)
//        {
//            Camera myCamera = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Direction, GameplayCamera.FieldOfView);
//            if (activate) World.RenderingCamera = myCamera;

//            return myCamera;
//        }
//        private static Camera CreateCamera(bool activate, Vector3 pos)
//        {
//            Camera myCamera = World.CreateCamera(pos, GameplayCamera.Direction, GameplayCamera.FieldOfView);
//            if (activate) World.RenderingCamera = myCamera;

//            return myCamera;
//        }

//        private static void ClearAllCameras()
//        {
//            World.DestroyAllCameras();
//            World.RenderingCamera = null;
//        }

//        //private static void GetAllCarsOfWatchers()
//        //{
//        //    foreach (var item in MG_Watchers.Vehicles)
//        //    {

//        //    }
//        //}

//        #endregion Private Methods









//    }
//}
