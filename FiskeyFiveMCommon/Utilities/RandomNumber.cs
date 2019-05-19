using System;

namespace CommonClient.Utilities
{
    public class RandomNumber
    {
        private static Random _rnd = new Random();

        public static Random RandomValue = _rnd;

        public static float RandomFloat(float min, float max) => Convert.ToSingle(_rnd.NextDouble() * (max - min) + min);

        public static int RandomInt() => _rnd.Next();
        public static int RandomInt(int max) => _rnd.Next(max);
        public static int RandomInt(int min, int max) => _rnd.Next(min, max);
    }
}
