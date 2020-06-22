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

        public static void GetStreetNamesAtCoordinates(this Vector3 position, out string street, out string cross)
        {
            street = string.Empty;
            cross = string.Empty;
            uint streetHash = uint.MinValue;
            uint crossHash = uint.MinValue;
            API.GetStreetNameAtCoord(position.X, position.Y, position.Z, ref streetHash, ref crossHash);
            if (streetHash != uint.MinValue) street = API.GetStreetNameFromHashKey(streetHash);
            if (crossHash != uint.MinValue) cross = API.GetStreetNameFromHashKey(crossHash);
        }

        /// <summary>
        /// Native 0x2A70BAE8883E4C81 -- determines if the specified point is within the other two points in a right angle wedge
        /// </summary>
        /// <param name="pointToCheck">Point to check</param>
        /// <param name="point1">Base of the wedge</param>
        /// <param name="point2">Opposite edge of the wedge</param>
        /// <param name="length">The wedge edge length</param>
        /// <returns></returns>
        public static bool IsInAngledArea(this Vector3 pointToCheck, Vector3 point1, Vector3 point2, float length)
        {
            return Function.Call<bool>(Hash.IS_POINT_IN_ANGLED_AREA, pointToCheck.X, pointToCheck.Y, pointToCheck.Z, point1.X, point1.Y, point1.Z, point2.X, point2.Y, point2.Z, length, 0, true);
        }
    }
}
