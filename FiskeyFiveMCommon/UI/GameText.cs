using System.Drawing;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;

namespace CommonClient.UI
{
    public class GameText : BaseScript
    {
        public string Text { get; set; }
        public Font TextFont { get; }
        public float Scale { get; set; } = 1.0f;
        public Color Color { get; }
        public float WrapX1 { get; }
        public float WrapX2 { get; }
        public Color DropshadowColor { get; } = Color.Empty;
        public int DropshadowDistance { get; } = 0;
        public Justification Justify { get; }
        public Point Position { get; }
        public SizeF Offset { get; }

        private float _x, _y;
        private Task _fiber;

        public GameText() { }

        public GameText(string text, Font font, Color color, Point position, SizeF offset, Justification justification = Justification.Left, float wrap1 = 0f, float wrap2 = 1.0f, float scale = 1.0f)
        {
            Text = text;
            TextFont = font;
            Color = color;
            Position = position;
            Offset = offset;
            WrapX1 = wrap1;
            WrapX2 = wrap2;
            Justify = justification;
            Scale = scale;
        }

        public GameText(string text, Font font, Color color, Point position, SizeF offset, Color dropColor, int dropDistance, Justification justification = Justification.Left, float wrap1 = 0f, float wrap2 = 1.0f, float scale = 1.0f)
        {
            Text = text;
            TextFont = font;
            Color = color;
            Position = position;
            Offset = offset;
            WrapX1 = wrap1;
            WrapX2 = wrap2;
            Justify = justification;
            Scale = scale;

            DropshadowDistance = dropDistance;
            DropshadowColor = dropColor;
        }

        public void Draw()
        {
            _x = (Position.X + Offset.Width) / Screen.Resolution.Width;
            _y = (Position.Y + Offset.Height) / Screen.Resolution.Height;

            _fiber = new Task(Process);
            _fiber.Start();
        }

        public void Stop()
        {
            _fiber?.Dispose();
        }

        private async void Process()
        {
            while (true)
            {
                await Delay(0);

                API.SetTextFont((int)TextFont);
                API.SetTextScale(Scale, Scale);
                API.SetTextColour(Color.R, Color.G, Color.B, Color.A);
                API.SetTextCentre(false);
                API.SetTextJustification((int)Justify);
                API.SetTextDropshadow(DropshadowDistance, DropshadowColor.R, DropshadowColor.G,
                    DropshadowColor.B, DropshadowColor.A);
                API.SetTextEntry("STRING");
                API.AddTextComponentString(Text);
                API.DrawText(_x, _y);
            }
        }

        // From SHDN https://github.com/crosire/scripthookvdotnet/blob/0868cd7ae29a9ef6ed1c375396827f144182b1d5/source/scripting/Font.cs
        public enum Font
        {
            ChaletLondon,
            HouseScript,
            Monospace,
            ChaletComprimeCologne = 4,
            Pricedown = 7
        }

        public enum Justification
        {
            Center,
            Left,
            Right
        }
    }
}
