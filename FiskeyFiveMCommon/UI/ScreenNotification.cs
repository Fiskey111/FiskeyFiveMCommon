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
        public int Handle { get; private set; } = -1;
        public string Title { get; private set; }
        public string Subtitle { get; private set; }
        public string Text { get; private set; }

        private readonly string ImageDictionary;
        private readonly string ImageName;

        public ScreenNotification(string imgDict, string imgName, string title, string subtitle, string msg)
        {
            ImageDictionary = imgDict;
            ImageName = imgName;
            
            Title = title;
            Subtitle = subtitle;
            Text = msg;
        }

        public void Display()
        {
            API.SetNotificationTextEntry("STRING");
            API.AddTextComponentString(Text);
            API.SetNotificationMessage(ImageDictionary, ImageName, false, 0, Title, Subtitle);
            Handle = API.DrawNotification(false, false);
        }

        public void Hide()
        {
            if (Handle == -1) return;
            API.RemoveNotification(Handle);
        }
    }
}
