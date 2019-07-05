using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class DrawText
    {
        public static void Draw(float scale, Color color, string text, Vector2 screenPos)
        {
            API.SetTextFont(0);
            API.SetTextProportional(false);
            API.SetTextScale(scale, scale);
            API.SetTextColour(color.R, color.G, color.B, color.A);
            API.SetTextDropshadow(0, 0, 0, 0, 255);
            API.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString(text);
            API.DrawText(Convert.ToSingle(screenPos.X - (0.5 / 2)), Convert.ToSingle(screenPos.Y - (0.57 / 2)));
            Screen.ShowSubtitle($"{scale} | {text} | {screenPos.X} | {screenPos.Y}");
        }
    }
}
