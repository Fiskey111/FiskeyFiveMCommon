using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Drawing;

namespace CommonClient.UI
{
    public class GraphicsTexture : BaseScript
    {
        public string TextureName { get; }
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public int Alpha { get; set; }
        public bool Enabled { get; set; } = false;
        public float Rotation { get; set; } = 0f;

        private long gfx;
        public GraphicsTexture(string textureName, PointF location, SizeF size, int alpha = 255, float rotation = 0f)
        {
            TextureName = textureName;
            Location = location;
            Size = size;
            Alpha = alpha;
            Rotation = rotation;


            string fName = string.Concat(textureName, ".png");

            long txd = API.CreateRuntimeTxd(textureName);
            gfx = API.CreateRuntimeTextureFromImage(txd, textureName, fName);
            if (gfx == 0) return;
            API.CommitRuntimeTexture(gfx);
            API.SetRuntimeTexturePixel(gfx, 0, 0, 255, 255, 255, Alpha);
        }

        public void Draw()
        {
            if (!Enabled) return;
            API.DrawSprite(TextureName, TextureName, Location.X, Location.Y, Size.Width, Size.Height, Rotation, 255, 255, 255, Alpha);
        }
    }
}
