using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace CommonClient.Utilities
{
    public class GetNearbyPeds
    {
        public static Ped[] GetClosestPeds(Vector3 position)
        {
            // starts at index of 2 with evens only
            List<Ped> nearbyPed = new List<Ped>();

            OutputArgument peds = new OutputArgument();

            int pedCount = Function.Call<int>(Hash.GET_PED_NEARBY_PEDS, Game.PlayerPed, peds, 0);
            
            Debug.WriteLine($"Ped count: {pedCount}");

            int[] pedArray = peds.GetResult<int[]>();

            for (int i = 0; i < pedCount; i++)
            {
                int id = i * 2 + 2;
                Ped p = new Ped(pedArray[id]);
                if (p.Exists())
                {
                    Screen.ShowNotification($"Ped {i} exists");
                    p.AttachBlip();
                    nearbyPed.Add(p);
                }
            }

            return nearbyPed.ToArray();
        }
    }
}
