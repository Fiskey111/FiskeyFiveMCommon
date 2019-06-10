using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace CommonServer
{
    public static class Extensions
    {
        public static void AddLog(this string text, string modification) =>
            Debug.WriteLine($"[ {modification} ] {text}");

        public static void ChatMessage(this Player player, string msg, string title, ChatColors color)
        {
            player.TriggerEvent("chat:addMessage", new
            {
                color = GetColorFromChatColor(color),
                multiline = false,
                args = new[] {title, msg}
            });
        }

        public static float ToFloat(this string value) => Convert.ToSingle(value);
        private static int[] GetColorFromChatColor(ChatColors color)
        {
            switch (color)
            {
                case ChatColors.FireRed:
                    return new[] { 255, 0, 0 };
                case ChatColors.PoliceBlue:
                    return new[] { 30, 50, 200 };
                case ChatColors.EMSGreen:
                    return new[] {25, 200, 50};
                case ChatColors.CivilianYellow:
                    return new[] {220, 220, 20};
                default:
                    return new[] {0, 0, 0};
            }
        }

        public enum ChatColors { FireRed, PoliceBlue, EMSGreen, CivilianYellow, DebugBlack }
    }
}
