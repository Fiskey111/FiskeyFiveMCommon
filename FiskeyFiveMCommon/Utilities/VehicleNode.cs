using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Runtime.Remoting;

namespace CommonClient.Utilities
{
    public class VehicleNode
    {
        public static bool GetClosestVehicleNode(Vector3 position, NodeType desiredType, ref Vector3 outPos, ref float heading)
        {
            return API.GetClosestVehicleNodeWithHeading(position.X, position.Y, position.Z, ref outPos, ref heading, (int)desiredType, 3f, 0);
        }

        public static bool GetClosestVehicleNodeFavorDirection(Vector3 position, Vector3 favoredDirection, NodeType desiredType, ref Vector3 outPos, ref float heading, int nthClosest = 0)
        {
            return API.GetNthClosestVehicleNodeFavourDirection(position.X, position.Y, position.Z, favoredDirection.X, favoredDirection.Y, favoredDirection.Z, nthClosest,
                ref outPos, ref heading, (int)desiredType, 0x40400000, 0);
        }

        public static bool GetNodeOnSideOfRoad(Vector3 position, float heading, ref Vector3 outPos)
        {
            return API.GetRoadSidePointWithHeading(position.X, position.Y, position.Z, heading, ref outPos);
        }

        public static bool GetNodeOnSideOfRoad(Vector3 position, ref Vector3 outPos, out float heading)
        {
            var refPos = Vector3.Zero;
            var refHeading = 0f;
            heading = refHeading;
            bool valid = GetClosestVehicleNode(position, NodeType.AnyRoad, ref refPos, ref refHeading);
            if (!valid) return false;
            return API.GetRoadSidePointWithHeading(position.X, position.Y, position.Z, refHeading, ref outPos);
        }

        public static bool GetPositionOnSidewalk(Vector3 position, NodeType desiredType, ref Vector3 outPos, ref float heading)
        {
            var node = API.GetClosestVehicleNodeWithHeading(position.X, position.Y, position.Z, ref outPos, ref heading, (int)desiredType, 3f, 0);
            if (!node) return false;
            outPos = World.GetNextPositionOnSidewalk(outPos);
            return true;
        }

        public enum NodeType
        {
            Road, AnyRoad, Water = 3
        }
    }
}
