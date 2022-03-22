using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.Objects
{
    public class Animation
    {
        public string ShortName { get; }
        public string Dictionary { get; }
        public string AnimationName { get; }
        public int AnimationTime { get; }

        public Animation(string shortName, string dictionaryAndAnimation, int time = 0)
        {
            if (dictionaryAndAnimation.Split(' ').Length < 2)
                throw new ArgumentOutOfRangeException(nameof(dictionaryAndAnimation));
            var animations = dictionaryAndAnimation.Split(' ');
            ShortName = shortName;
            Dictionary = animations[0];
            AnimationName = animations[1];
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

        public async Task Play(Ped ped)
        {
            ped.Task.PlayAnimation(Dictionary, AnimationName);
            var endBreak = DateTime.Now.AddSeconds(20);
            while (API.IsEntityPlayingAnim(ped.Handle, Dictionary, AnimationName, 3))
            {
                await BaseScript.Delay(0);
                if (DateTime.Compare(DateTime.Now, endBreak) >= 0) break;
            }
        }
    }
}
