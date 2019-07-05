using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using CommonClient.Extensions;

namespace CommonClient.Utilities
{
    public class Raycast : BaseScript
    {
        public static Entity CastForPedOrPlayer()
        {
            RaycastResult cast = Raycast.CastCapsule(Game.PlayerPed.Position, Game.PlayerPed.GetOffsetPosition(new Vector3(0f, 1f, 0f)), Raycast.CapsuleFlags.Peds, Game.PlayerPed);

            if (!cast.HasHitEntity)
            {
                return null;
            }
            if (!cast.HitEntity.IsAPed())
            {
                return null;
            }
            return cast.HitEntity as Entity;
        }
        public static RaycastResult CastCapsule(Vector3 start, Vector3 end, CapsuleFlags type, Entity ignoredEntity, float radius = 1.0f)
        {
            int handle = Function.Call<int>(Hash.START_SHAPE_TEST_CAPSULE, start.X, start.Y, start.Z, end.X,
                end.Y, end.Z, radius, (int)type, ignoredEntity, 7);

            OutputArgument hit = new OutputArgument(typeof(bool));
            OutputArgument coords = new OutputArgument(typeof(Vector3));
            OutputArgument normal = new OutputArgument(typeof(Vector3));
            OutputArgument ent = new OutputArgument(typeof(int));

            int result = Function.Call<int>(Hash.GET_SHAPE_TEST_RESULT, handle, hit, coords, normal, ent);
            
            return new RaycastResult(Entity.FromHandle(ent.GetResult<int>()), hit.GetResult<bool>(), coords.GetResult<Vector3>());
        }

        public static RaycastResult CastProbe(Vector3 start, Vector3 end, ProbeFlags flag, Entity ignoredEntity)
        {
            int handle = Function.Call<int>(Hash.START_SHAPE_TEST_LOS_PROBE, start.X, start.Y, start.Z, end.X, end.Y,
                end.Z, (int)flag, ignoredEntity, 7);

            OutputArgument hit = new OutputArgument(typeof(bool));
            OutputArgument coords = new OutputArgument(typeof(Vector3));
            OutputArgument normal = new OutputArgument(typeof(Vector3));
            OutputArgument ent = new OutputArgument(typeof(int));

            int result = Function.Call<int>(Hash.GET_SHAPE_TEST_RESULT, handle, hit, coords, normal, ent);
            
            Entity hitEnt = Entity.FromHandle(ent.GetResult<int>());
            return new RaycastResult(hitEnt, hit.GetResult<bool>(), coords.GetResult<Vector3>());
        }

        public static async Task<Entity[]> CircleCast(Vector3 start, float radius, CapsuleFlags flag, int castNumber = 15, int delay = 0010)
        {
            List<Entity> entList = new List<Entity>();

            for (float i = 0f; i < 360; i = i + 30)
            {
                Vector3 end = start.VectorAtAngle(5f, i);

                RaycastResult result = CastCapsule(start, end, flag, Game.PlayerPed);

                if (!result.HasHitEntity) continue;
                
                Entity target = result.HitEntity;

                entList.Add(target);

                await Delay(delay);
            }
            return entList.ToArray();
        }

        public enum CapsuleFlags { Vehicles = 10, Peds = 12 }
        public enum ProbeFlags { World = 1, Vehicles = 2, Peds = 8 }
    }

    public class RaycastResult
    {
        public Entity HitEntity { get; }
        public bool HasHitEntity { get; }
        public Vector3 HitCoordinates { get; }
        
        public RaycastResult() { }

        public RaycastResult(Entity hitEnt, bool hasHit, Vector3 hitCoord)
        {
            HitEntity = hitEnt;
            HasHitEntity = hasHit;
            HitCoordinates = hitCoord;
        }
    }
}
