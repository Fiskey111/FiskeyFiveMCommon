using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace CommonClient.Objects
{
    public class Animation
    {
        public string Name { get; }
        public string Dictionary { get; }
        public string AnimationName { get; }
        public int AnimationTime { get; }

        public Animation(string name, string dictionary, string animation, int time = 0)
        {
            Name = name;
            Dictionary = dictionary;
            AnimationName = animation;
            AnimationTime = time;
        }

        public void PlayAnimation(Player player, AnimationFlags flags = AnimationFlags.StayInEndFrame)
        {
            if (!player.Character.Exists())
            {
                Debug.WriteLine("No player exists");
                return;
            }

            Game.PlayerPed.Task.PlayAnimation(Dictionary, AnimationName, 8f, -1, flags);
        }

        public void PlayAnimation(Ped ped, AnimationFlags flags = AnimationFlags.StayInEndFrame)
        {
            if (!ped.Exists())
            {
                Debug.WriteLine("No ped exists");
                return;
            }

            ped.Task.PlayAnimation(Dictionary, AnimationName, 8f, -1, flags);
        }
    }
}
