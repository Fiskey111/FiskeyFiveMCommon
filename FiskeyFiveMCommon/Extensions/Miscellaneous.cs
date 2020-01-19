using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.Extensions
{
    public static class Miscellaneous
    {
        public static void AddLog(string log)
        {
            Debug.WriteLine(log);
        }

        public static int GetPropNetworkID(this Prop prop) => Function.Call<int>(Hash.OBJ_TO_NET, prop);

        public static int GetPropFromNetworkID(this int netID) => Function.Call<int>(Hash.NET_TO_OBJ, netID);
    }
}
