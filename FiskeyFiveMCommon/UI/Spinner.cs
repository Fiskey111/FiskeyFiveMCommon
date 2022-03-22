using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using System;

namespace CommonClient.UI
{
    public class Spinner
    {
        public Spinner(ScreenPosition position, int radius = 15, int startingValue = 0)
        {
            float offsetPixels = 50f;
            float aspectRatio = Screen.AspectRatio;
            float xPosition;
            float yPosition = offsetPixels;
            float radiusHalved = radius / 2;
            var resolution = Screen.Resolution;
            switch (position)
            {
                case ScreenPosition.TopRight:
                    xPosition = resolution.Width - offsetPixels * aspectRatio - radiusHalved;
                    yPosition = offsetPixels + radiusHalved;
                    break;
                case ScreenPosition.BottomLeft:
                    xPosition = offsetPixels * aspectRatio + radiusHalved;
                    yPosition = resolution.Height - offsetPixels * aspectRatio - radiusHalved;
                    break;
                case ScreenPosition.BottomRight:
                    xPosition = resolution.Width - offsetPixels * aspectRatio - radiusHalved;
                    yPosition = resolution.Height - offsetPixels * aspectRatio - radiusHalved;
                    break;
                case ScreenPosition.Center:
                    xPosition = resolution.Width / 2 - radiusHalved;
                    yPosition = resolution.Height / 2 - radiusHalved;
                    break;
                default:
                    xPosition = offsetPixels * aspectRatio + radiusHalved;
                    yPosition = offsetPixels + radiusHalved;
                    break;
            }

            API.SendNuiMessage(JsonConvert.SerializeObject(new SpinnerJson("EVSpinnerCreate", Convert.ToInt32(xPosition), Convert.ToInt32(yPosition), radius, startingValue)));
        }

        public void UpdatePercentage(int percentage)
        {
            API.SendNuiMessage(JsonConvert.SerializeObject(new SpinnerJson("EVSpinnerProgress", 0, 0, 0, percentage)));
        }

        public void Stop()
        {
            API.SendNuiMessage(JsonConvert.SerializeObject(new SpinnerJson("EVSpinnerProgress", 0, 0, 0, -1)));
        }

        public enum ScreenPosition { TopLeft, TopRight, BottomLeft, BottomRight, Center }
    }
    public class SpinnerJson
    {
        public string dataType { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int radius { get; set; }
        public int value { get; set; }

        public SpinnerJson() { }
        public SpinnerJson(string _dataType, int _x, int _y, int _radius, int _value)
        {
            dataType = _dataType;
            x = _x;
            y = _y;
            radius = _radius;
            value = _value;
        }
    }
}
