using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class Marker
    {
        public Vector3 Position { get; set; }
        public Vector3 Scale { get; set; }
        public Vector3 Rotation { get; set; }
        public Color Color { get; set; }
        public MarkerTypes Type { get; set; }
        public int Alpha { get; set; }
        public bool BobMarker { get; set; }
        public bool FaceCam { get; set; }
        public bool Rotate { get; set; }

        public Marker(Vector3 position, Color color, Vector3 scale, Vector3 rotation, int alpha = 255, MarkerTypes type = MarkerTypes.MarkerTypeUpsideDownCone, bool bobUpAndDown = false, bool faceCamera = false, bool rotate = false)
        {
            Position = position;
            Color = color;
            Scale = scale;
            Rotation = rotation;
            Type = type;
            Alpha = alpha;
            BobMarker = bobUpAndDown;
            FaceCam = faceCamera;
            Rotate = rotate;
        }

        public Marker(Vector3 position, Color color, MarkerTypes type = MarkerTypes.MarkerTypeUpsideDownCone, int alpha = 255)
        {
            Position = position;
            Color = color;
            Scale = Vector3.One;
            Rotation = Vector3.One;
            Type = type;
            Alpha = alpha;
            BobMarker = false;
            FaceCam = false;
            Rotate = false;
        }

        public void DrawMarker()
        {
            Draw();
        }

        private void Draw()
        {
            try
            {
                API.DrawMarker((int)Type, Position.X, Position.Y, Position.Z, Position.X, Position.Y, Position.Z, 
                    Rotation.X, Rotation.Y, Rotation.Z, Scale.X, Scale.Y, Scale.Z, Color.R, Color.G, Color.B, Alpha,
                    BobMarker, FaceCam, 2, Rotate, null, null, false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"COMMONCLIENT :: ERROR ||| {ex}");
            }
        }

        public enum MarkerTypes
        {
            MarkerTypeUpsideDownCone = 0,
            MarkerTypeVerticalCylinder = 1,
            MarkerTypeThickChevronUp = 2,
            MarkerTypeThinChevronUp = 3,
            MarkerTypeCheckeredFlagRect = 4,
            MarkerTypeCheckeredFlagCircle = 5,
            MarkerTypeVerticleCircle = 6,
            MarkerTypePlaneModel = 7,
            MarkerTypeLostMCDark = 8,
            MarkerTypeLostMCLight = 9,
            MarkerTypeNumber0 = 10,
            MarkerTypeNumber1 = 11,
            MarkerTypeNumber2 = 12,
            MarkerTypeNumber3 = 13,
            MarkerTypeNumber4 = 14,
            MarkerTypeNumber5 = 15,
            MarkerTypeNumber6 = 16,
            MarkerTypeNumber7 = 17,
            MarkerTypeNumber8 = 18,
            MarkerTypeNumber9 = 19,
            MarkerTypeChevronUpx1 = 20,
            MarkerTypeChevronUpx2 = 21,
            MarkerTypeChevronUpx3 = 22,
            MarkerTypeHorizontalCircleFat = 23,
            MarkerTypeReplayIcon = 24,
            MarkerTypeHorizontalCircleSkinny = 25,
            MarkerTypeHorizontalCircleSkinny_Arrow = 26,
            MarkerTypeHorizontalSplitArrowCircle = 27,
            MarkerTypeDebugSphere = 28,
            MarkerTypeDallorSign = 29,
            MarkerTypeHorizontalBars = 30,
            MarkerTypeWolfHead = 31
        };
    }
}
