using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.Objects
{
    public class AnimationDictionary : BaseScript
    {
        public string Dictionary { get; private set; }
        public bool IsLoaded { get; private set; }

        internal int WaitTime = 1000;

        public AnimationDictionary() { }

        public AnimationDictionary(string dict, int waitTime = 1000)
        {
            Dictionary = dict;
            WaitTime = waitTime;
        }

        public void LoadAndWait()
        {
            Task t = new Task(Process);
            t.Start();
        }

        private async void Process()
        {
            API.RequestAnimDict(Dictionary);
            int time = 0;
            while (!API.HasAnimDictLoaded(Dictionary))
            {
                await Delay(0001);
                time++;
                if (time >= WaitTime)
                {
                    IsLoaded = false;
                    break;
                }
                IsLoaded = true;
            }
        }
    }
}
