using CitizenFX.Core;
using CitizenFX.Core.UI;
using System;
using System.Threading.Tasks;
using NativeUI;

namespace CommonClient.UI
{
    class InteractiveNotification : BaseScript
    {
        public InteractiveNotification() { }

        private string _text;
        private double _time;
        private Control _yes;
        private Control _no;

        public InteractiveNotification(string text, double time, Control yes = Control.MpTextChatTeam, Control no = Control.PushToTalk)
        {
            _text = text;
            _time = time;
            _yes = yes;
            _no = no;
        }

        public async Task<bool> DisplayAsync()
        {
            Notification not = Screen.ShowNotification(_text);
            //Screen.ShowNotification($"Press ~h~~g~~{_yes}~~w~~s~ to accept" +
            //$"\nPress ~h~~r~~{_no}~~w~~s~ to not accept");

            bool isContinue = false;
            int timer = 0;
            BarTimerBar bar = new BarTimerBar("Time Remaining");
            int red = 0;
            int green = 255;
            while (true)
            {
                await Delay(0);

                if (Game.IsControlPressed(0, Control.MpTextChatTeam))
                {
                    isContinue = true;
                    break;
                }
                else if (Game.IsControlPressed(0, Control.PushToTalk) || timer >= _time)
                {
                    break;
                }
                timer++;
                red = Convert.ToInt32(Math.Round(timer / 2d, 0));
                green = 255 - red;
                //bar.ForegroundColor = Color.FromArgb(red, green, 0);
                bar.Percentage = 1 - Convert.ToSingle(Math.Round(timer / _time, 4));
                bar.Draw(1);
            }

            not.Hide();
            if (!isContinue)
            {
                Screen.ShowNotification($"Selection cancelled");
                return false;
            }
            return true;
        }
    }
}
