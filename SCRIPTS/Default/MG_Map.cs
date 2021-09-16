////////////////////////////////////////////////////////////////////////////////
//
//	MG_Map.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;

namespace MG_Liquidator
{

    public enum DestinationSetting { InsideCIty, OutsideCity, Everywhere };


    public static class MG_Map
    {
        #region Public Methods

        public static Vector3 GetRandomMapPos(DestinationSetting destinationSetting)
        {
            Vector3 loc = new Vector3(0, 0, 220);
            int x = MG_Random.Random(-1305, 1100);
            int y;

            if (destinationSetting.Equals(DestinationSetting.Everywhere))
            {
                y = MG_Random.Random(-2251, 7000);
            }
            else if (destinationSetting.Equals(DestinationSetting.OutsideCity))
            {
                y = MG_Random.Random(473, 7000);
            }
            else// if (destinationSetting.Equals(DestinationSetting.InsideCIty))
            {
                y = MG_Random.Random(-2251, 473);
            };
            loc.X = x;
            loc.Y = y;
            return loc;
            //return MG_Player.Ped.Position;///DEL TEST!!!!!!!!!!!!!!!!
        }

        public static string GetStreetName(Vector3 pos)
        {
            Vector2 pos2D = new Vector2(pos.X, pos.Y);
            string streetName = World.GetStreetName(pos2D);
            return streetName;
        }
        #endregion Public Methods
    }
}
