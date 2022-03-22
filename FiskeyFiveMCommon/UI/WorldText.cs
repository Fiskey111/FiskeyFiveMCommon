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
    public class WorldText : BaseScript
    {

        public string ID { get; set; }
        public Vector3 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public float DistanceSquared { get; set; }
        public float Scale { get; set; }
        private static Task _task;
        public WorldText()
        {
            if (_task == null)
            {
                _task = new Task(DisplayText);
                _task.Start();
            }
        }

        public WorldText(string id, Vector3 worldPos, string text, Color color, float distanceSquared, float scale)
        {
            try
            {
                ID = id;
                Position = worldPos;
                Text = text;
                Color = color;
                DistanceSquared = distanceSquared;
                DistanceSquared = distanceSquared;
                Scale = scale;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void AddWorldText(string id, Vector3 worldPos, string text, Color color, float distanceSquared = 4f, float scale = 0.3f)
        {
            try 
            { 
                AddTextToList(new WorldText(id, worldPos, text, color, distanceSquared, scale));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void AddWorldText(WorldText text)
        {
            try
            {
                AddTextToList(text);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public static void RemoveWorldText(WorldText text)
        {
            RemoveTextFromList(text);
        }
        public static void RemoveWorldText(string id)
        {
            RemoveTextFromList(id);
        }

        private static void AddTextToList(WorldText text)
        {
            try
            {
                if (_textList.Any(t => t.ID == text.ID))
                {
                    var textValueInList = _textList.First(t => t.ID == text.ID);
                    textValueInList = text;
                }
                else
                {
                    _textList.Add(text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static void RemoveTextFromList(WorldText text)
        {
            try
            {
                if (_textList.Any(t => t.ID == text.ID))
                {
                    _textList.Remove(_textList.First(t => t.ID == text.ID));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static void RemoveTextFromList(string id)
        {
            try
            {
                if (_textList.Any(t => t.ID == id))
                {
                    _textList.Remove(_textList.First(t => t.ID == id));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private static List<WorldText> _textList = new List<WorldText>();

        public static bool DoesWorldTextExist(WorldText text)
        {
            if (_textList.Contains(text)) return true;
            if (_textList.Any(t => t.ID == text.ID)) return true;
            return false;
        }

        private static async void DisplayText()
        {
            while (true)
            {
                await Delay(0);
                foreach (var item in _textList.ToList())
                {
                    var distance = Game.PlayerPed.Position.DistanceToSquared(item.Position);
                    if (distance > item.DistanceSquared) continue;

                    DrawText(item.Position, item.Text, item.Color, item.Scale);
                }
            }
        }

        private static void DrawText(Vector3 pos, string text, Color color, float scale)
        {
            try
            {
                API.SetDrawOrigin(pos.X, pos.Y, pos.Z, 0);
                API.SetTextFont(0);
                API.SetTextProportional(false);
                API.SetTextScale(0f, scale);
                API.SetTextColour(color.R, color.G, color.B, color.A);
                API.SetTextDropshadow(0, 0, 0, 0, 255);
                API.SetTextEdge(2, 0, 0, 0, 150);
                API.SetTextDropShadow();
                API.SetTextOutline();
                API.SetTextEntry("STRING");
                API.SetTextCentre(true);
                API.AddTextComponentString(text);
                API.DrawText(0f, 0f);
                API.ClearDrawOrigin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
