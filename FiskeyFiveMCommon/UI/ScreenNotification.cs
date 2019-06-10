using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;

namespace CommonClient.UI
{
    public class ScreenNotification
    {
        public static int DisplayNotification(string dict, string name, string title, string subtitle, string msg)
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(msg);
            API.SetNotificationMessage(dict, name, false, 0, title, subtitle);
            return API.DrawNotification(false, false);
        }
    }
}
