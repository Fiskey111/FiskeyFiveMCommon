using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core.Native;

namespace CommonClient.Utilities
{
    public class RageGame
    {
        public static void FadeScreenOut(int time) => Function.Call(Hash.DO_SCREEN_FADE_OUT, time);
        public static void FadeScreenIn(int time) => Function.Call(Hash.DO_SCREEN_FADE_IN, time);

        public static bool IsScreenFadingOut() => Function.Call<bool>(Hash.IS_SCREEN_FADING_OUT);
        public static bool IsScreenFadingIn() => Function.Call<bool>(Hash.IS_SCREEN_FADING_IN);
    }
}
