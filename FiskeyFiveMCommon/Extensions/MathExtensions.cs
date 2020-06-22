using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.Extensions
{
    public static class MathExtensions
    {
        public static List<Vector3> GetCirclePoints(int points, float radius, Vector3 center)
        {
            List<Vector3> _points = new List<Vector3>();
            double slice = 2 * Math.PI / points;
            for (int i = 0; i < points; i++)
            {
                double angle = slice * i;
                float newX = (float)(center.X + radius * Math.Cos(angle));
                float newY = (float)(center.Y + radius * Math.Sin(angle));
                _points.Add(new Vector3(newX, newY, center.Z));
            }
            return _points;
        }

        /// <summary>
        /// DO NOT USE
        /// </summary>
        /// <param name="center"></param>
        /// <param name="samples"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static List<Vector3> GetSpherePoints(this Vector3 center, int samples, float radius)
        {
            List<Vector3> points = new List<Vector3>();
            var phi = Math.PI * (3 - Math.Sqrt(5));

            for (int i = 0; i < samples; i++)
            {
                var y = 1 - (i / (Convert.ToSingle(samples - 1)) * 2);
                var r = Math.Sqrt(1 - y * y)  * radius;
                var theta = phi * i;
                var x = Math.Cos(theta) * r;
                var z = Math.Sin(theta) * r;
                points.Add(new Vector3(Convert.ToSingle(x), y, Convert.ToSingle(z)));
            }
            return points;
        }

        public static Vector3 GetPointOnSphere(this Vector3 position, Vector3 rotation, float radius, bool isInDegrees = true)
        {
            if (isInDegrees) radius = Convert.ToSingle(radius * (2 * Math.PI / 360));

            Vector3 temp;
            temp.X = Convert.ToSingle(position.X + radius * Math.Cos(rotation.X) * Math.Sin(rotation.Y));
            temp.Y = Convert.ToSingle(position.Y + radius * Math.Sin(rotation.X) * Math.Sin(rotation.Y));
            temp.Z = Convert.ToSingle(position.Z + radius * Math.Cos(rotation.Y));
            return temp;
        }
    }
}
