using CitizenFX.Core;
using CitizenFX.Core.Native;
using System;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class Keybind
    {
        public event Pressed OnPressed;
        public event Released OnReleased;
        public event Held OnHeldIntervalElapsed;
        public string KeyName { get; private set; }
        public bool IsPressedNow { get; private set; } = false;
        public int HeldDownInterval { get; private set; } = 0;
        public int HeldDownThreshold { get; private set; } = 25;
        private DateTime _stopTime = DateTime.MinValue;
        private Task task;

        public Keybind(string command, string description, string keyName, int heldDownInterval = 0)
        {
            try
            {
                API.RegisterKeyMapping("+" + command, description, "keyboard", keyName);
                HeldDownInterval = heldDownInterval;
                KeyName = keyName;

                API.RegisterCommand("+" + command, new Action(() =>
                {
                    OnPressed?.Invoke(this);
                    IsPressedNow = true;
                    if (heldDownInterval > 0)
                    {
                        task = new Task(Process);
                        task.Start();
                    }
                    _stopTime = DateTime.Now.AddMilliseconds(HeldDownInterval);
                }), false);
                API.RegisterCommand("-" + command, new Action(() =>
                {
                    OnReleased?.Invoke(this, DateTime.Now.CompareTo(_stopTime) >= 0);
                    IsPressedNow = false;
                    task?.Dispose();
                    _stopTime = DateTime.MinValue;
                }), false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void Process()
        {
            Spinner s = new Spinner(Spinner.ScreenPosition.Center);
            var endTime = DateTime.Now.AddMilliseconds(HeldDownInterval);
            while (IsPressedNow)
            {
                await BaseScript.Delay(0);
                double percentage = Math.Abs(Convert.ToInt32(100 - (endTime - DateTime.Now).TotalMilliseconds / HeldDownInterval * 100));
                if (percentage < HeldDownThreshold) continue;
                s.UpdatePercentage(Convert.ToInt32(percentage));
                if (percentage > 100) break;
            }
            s.Stop();

            if (IsPressedNow) OnHeldIntervalElapsed?.Invoke(this);
        }

        public delegate void Pressed(Keybind bind);
        public delegate void Released(Keybind bind, bool wasHeldDown);
        public delegate void Held(Keybind bind);
    }
}
