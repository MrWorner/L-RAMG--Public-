////////////////////////////////////////////////////////////////////////////////
//
//	MG_Vehicle.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;
using GTA.Native;
using System.Collections.Generic;
using System.Linq;

namespace MG_Liquidator
{
    public static class MG_Vehicle
    {
        #region Public Methods

        public static Vehicle FindSafestVehicleForTarget(Ped target, Ped player)
        {
            var vehicles = World.GetNearbyVehicles(target.Position, 100);
            float mindistance = 9999f;
            Vehicle chosenVehice = null;
            foreach (var vehicle in vehicles)
            {
                //if (_player.IsSittingInVehicle(_vehicleTarget) || (_vehicleTarget.Driver != null && _vehicleTarget.Driver != _target))
                if (
                    vehicle.IsAlive
                    && !vehicle.IsOnFire
                    && !vehicle.IsUpsideDown
                    && vehicle.IsDriveable
                    //&& player.CurrentVehicle != vehicle
                    && !vehicle.Model.IsTrain
                    && !vehicle.Model.IsHelicopter
                    && !vehicle.Model.IsPlane
                    && !vehicle.Model.IsBoat

                    )//DEL LAST && _vehicle.PassengerCount > 0 && _vehicle.Driver != null
                {
                    float distance = Vector2.Distance(vehicle.Position, target.Position);
                    float distancePlayer = Vector2.Distance(vehicle.Position, player.Position);//NEW
                    if (mindistance > distance && distancePlayer > distance)
                    {
                        chosenVehice = vehicle;
                        mindistance = distance;
                    }
                }

            }
            return chosenVehice;
        }

        public static Vehicle CreateVehicle(Vector3 position, float positionAround, string vehicleModelStr)
        {
            Model vehicleModel = new Model(vehicleModelStr);
            vehicleModel.Request(500);
            Vehicle vehicle = World.CreateVehicle(vehicleModel, World.GetNextPositionOnStreet(position.Around(positionAround)));//NEW
            Function.Call(Hash.SET_VEHICLE_ON_GROUND_PROPERLY, vehicle);
            vehicleModel.MarkAsNoLongerNeeded();
            MG_GarbageCollector.AddVehicle(vehicle);
            return vehicle;
        }

        public static bool HasAnyFreeSeats(Vehicle vehicle)
        {
            return Function.Call<bool>(Hash._IS_ANY_VEHICLE_SEAT_EMPTY, vehicle);
        }

        public static List<VehicleSeat> GetFreeSeats(Vehicle vehicle)
        {
            List<VehicleSeat> result = new List<VehicleSeat>();
            if (HasAnyFreeSeats(vehicle))
            {
                List<VehicleSeat> seatList = new List<VehicleSeat>() { VehicleSeat.Driver, VehicleSeat.LeftRear, VehicleSeat.RightRear, VehicleSeat.RightFront };//VehicleSeat.LeftFront,
                foreach (var seat in seatList)
                {
                    if (vehicle.IsSeatFree(seat))
                        result.Add(seat);
                }
            }

            return result;
        }

        public static Ped CreatePedInsideVehicle(Vehicle vehicle, string modelStr)
        {
            Model pedModel = new Model(modelStr);
            pedModel.Request(500);
            Ped ped;
            List<VehicleSeat> seatList = GetFreeSeats(vehicle);
            if (seatList.Any())
            {
                VehicleSeat seat = seatList[0];
                ped = vehicle.CreatePedOnSeat(seat, pedModel);//NEW
            }
            else
            {
                
                ped = vehicle.CreatePedOnSeat(VehicleSeat.Any, pedModel);//NEW
            }
            pedModel.MarkAsNoLongerNeeded();
            MG_GarbageCollector.AddVehicle(vehicle);
            return ped;
        }

        #endregion Public Methods
    }
}
