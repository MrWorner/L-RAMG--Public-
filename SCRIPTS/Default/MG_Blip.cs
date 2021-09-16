////////////////////////////////////////////////////////////////////////////////
//
//	MG_Blip.cs
//	Author: HarryWorner
//  GitHub: https://github.com/MrWorner	
//
/////////////////////////////////////////////////////////////////////////////////

using GTA;
using GTA.Math;

//https://wiki.gtanet.work/index.php?title=Blips
//https://docs.fivem.net/docs/game-references/blips/#blips
//https://wiki.rage.mp/index.php?title=Blips

namespace MG_Liquidator
{
    public static class MG_Blip
    {
        #region Public Methods

        public static Blip CreateBlip(Vector3 pos, bool showRoutine, BlipSprite sprite, BlipColor color)
        {
            Blip blip = World.CreateBlip(pos);
            blip.ShowRoute = showRoutine;
            blip.Sprite = sprite;
            blip.Color = color;
            return blip;
        }

        public static Blip CreateBlip(Ped ped, bool showRoutine, BlipSprite sprite, BlipColor color)
        {
            Blip blip = ped.AddBlip();
            blip.ShowRoute = showRoutine;
            blip.Sprite = sprite;
            blip.Color = color;
            return blip;
        }

        public static Blip CreateBlip(Vehicle vehicle, bool showRoutine, BlipSprite sprite, BlipColor color)
        {
            Blip blip = vehicle.AddBlip();
            blip.ShowRoute = showRoutine;
            blip.Sprite = sprite;
            blip.Color = color;
            return blip;
        }

        public static void RemoveBlip(Blip blip)
        {
            if (blip.Exists())
            {
                blip.ShowRoute = false;
                blip.Remove();
            }           
        }
        #endregion Public Methods
    }
}
