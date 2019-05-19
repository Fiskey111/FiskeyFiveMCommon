using CitizenFX.Core;

namespace CommonClient.Objects
{
    public class SpawnPoint
    {
        public float Heading { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public SpawnPoint(float x, float y, float z)
        {
            Position = new Vector3(x, y, z);
        }

        public SpawnPoint(Vector3 pos)
        {
            Position = pos;
        }

        public SpawnPoint(float x, float y, float z, float heading)
        {
            Position = new Vector3(x, y, z);
            Heading = heading;
        }

        public SpawnPoint(float heading, Vector3 pos)
        {
            Position = pos;
            Heading = heading;
        }

        public SpawnPoint(Vector3 pos, Vector3 rot)
        {
            Position = pos;
            Rotation = rot;
        }

        public SpawnPoint(float x, float y, float z, Vector3 rot)
        {
            Position = new Vector3(x, y, z);
            Rotation = rot;
        }
    }
}