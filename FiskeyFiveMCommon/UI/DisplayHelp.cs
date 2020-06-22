using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class DisplayHelp
    {
        /// <summary>
        /// https://pastebin.com/nqNYWMSB
        /// </summary>
        /// <param name="message"></param>
        /// <param name="loop"></param>
        /// <param name="beep"></param>
        /// <param name="timer"></param>
        public static void Display(string message, bool loop, bool beep, int timer = -1, int floating = 0)
        {
            API.BeginTextCommandDisplayHelp("STRING");
            API.AddTextComponentString(message);
            API.EndTextCommandDisplayHelp(0, loop, beep, timer);
        }

        public static void CancelHelp()
        {
            API.ClearAllHelpMessages();
        }
    }
}
