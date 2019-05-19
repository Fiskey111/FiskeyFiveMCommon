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
            Function.Call(Hash._SET_NOTIFICATION_TEXT_ENTRY, "STRING");
            Function.Call(Hash._SET_NOTIFICATION_MESSAGE, dict, name, false, 0, 0, title, subtitle);
            Function.Call(Hash._ADD_TEXT_COMPONENT_ITEM_STRING, msg);
            return Function.Call<int>(Hash._DRAW_NOTIFICATION, false, false);
        }
    }
}
