using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CommonClient.Utilities;

namespace CommonClient.Extensions
{
    public static class Vector
    {
        public static void SetOnFire(this Vector3 position) => Function.Call(
            Hash.ADD_EXPLOSION, position.X, position.Y, position.Z, (int)ExplosionType.Molotov1,
                1f, false, true, 0f);

        public static Vector3 AroundPosition(this Vector3 start, float radius)
        {
            // Random direction.
            Vector3 direction = RandomXY();
            Vector3 around = start + (direction * radius);
            return around;
        }

        public static Vector3 CreateFromString(string x, string y, string z) => new Vector3(Convert.ToSingle(x), Convert.ToSingle(y), Convert.ToSingle(z));

        public static float DistanceTo(this Vector3 start, Vector3 end)
        {
            return (end - start).Length();
        }

        public static Vector3 RandomXY()
        {
            Vector3 vector3 = new Vector3();
            vector3.X = (float)(RandomNumber.RandomValue.NextDouble() - 0.5);
            vector3.Y = (float)(RandomNumber.RandomValue.NextDouble() - 0.5);
            vector3.Z = 0.0f;
            vector3.Normalize();
            return vector3;
        }

        public static Vector3 VectorAtAngle(this Vector3 center, float radius, float angle)
        {
            Vector3 pos;
            pos.X = center.X + radius * Convert.ToSingle(Math.Sin(angle));
            pos.Y = center.Y + radius * Convert.ToSingle(Math.Cos(angle));
            pos.Z = center.Z;
            return pos;
        }
    }
}
