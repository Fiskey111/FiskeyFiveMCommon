using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class Chat
    {
        public static void ChatMessage(Player target, Color color, string message)
        {
            BaseScript.TriggerEvent("chat:addMessage", new
            {
                color = new[] { color.R, color.G, color.B },
                multiline = true,
                args = new[] { target.Name, message }
            });
        }
        public static string GetChatColor(ChatColor color) => $"^{(int)color}";
        public enum ChatColor { RedOrange = 1, LightGreen = 2, LightYellow = 3, DarkBlue = 4, LightBlue = 5, Violet = 6, White = 7, BloodRed = 8, Fuchsia = 9 }
        public static string ChatBold => $"^*";
        public static string ChatUnderline => $"^_";
        public static string ChatStrikethrough => $"^~";
        public static string ChatUnderlineStrikethrough => $"^=";
        public static string ChatBoldUnderlineStrikethrough => $"^*^=";
        public static string ChatDefault => $"^r";
    }
}
