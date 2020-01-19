using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CommonClient.Objects;
using static CitizenFX.Core.Native.Function;
using static CitizenFX.Core.Native.Hash;

namespace CommonClient.Extensions
{
    public static class Entities
    {
        public static void PlayScenario(this Ped ped, Scenario scenario) => scenario.Play(ped);

        public static void PlayScenario(this Ped ped, Scenario.ScenarioList scenarioName)
        {
            Scenario s = new Scenario("", scenarioName);
            s.Play(ped);
        }
        public static float DistanceTo(this ISpatial start, ISpatial end)
        {
            return (end.Position - start.Position).Length();
        }

        public static float DistanceTo(this ISpatial start, Vector3 end)
        {
            return (end - start.Position).Length();
        }

        public static float DistanceTo(this Vector3 start, ISpatial end)
        {
            return (end.Position - start).Length();
        }

        public static void SetSpawnPoint(this Entity ent, SpawnPoint point)
        {
            if (!ent.IsValid()) return;

            ent.Position = point.Position;
            if (point.Rotation != Vector3.Zero) ent.Rotation = point.Rotation;
            ent.Heading = point.Heading;
        }

        public static void SetOnFire(this Entity entity)
        {
            if (!entity.Exists()) return;
            Call(ADD_EXPLOSION, entity.Position.X, entity.Position.Y, entity.Position.Z, (int) ExplosionType.Molotov1,
                1f, false, true, 0f);
        }

        public static void DisposeOf(this Entity entity)
        {
            if (!entity.Exists()) return;
            entity.IsPersistent = false;
            entity.MarkAsNoLongerNeeded();
        }

        public static void ChangeDoors(this Vehicle vehicle, bool open = true)
        {
            if (!vehicle.Exists()) return;
            foreach (VehicleDoor door in vehicle.Doors)
            {
                if (open) door.Open(false, true);
                else door.Close(true);
            }
        }

        public static Bone? GetLastDamageBone(this Ped ped)
        {
            OutputArgument args = new OutputArgument();
            
            return Function.Call<bool>(Hash.GET_PED_LAST_DAMAGE_BONE, ped, args)
                ? (Bone?)(Bone)args.GetResult<int>() : null;
        }

        public static string GetPedCauseOfDeath(this Ped ped)
        {
            uint cause = Function.Call<uint>((Hash)0x16FFE42AB2D2DC59, ped); // GET_PED_CAUSE_OF_DEATH
            
            if (Enum.IsDefined(typeof(WeaponHash), cause)) return WeaponCause(cause);
            if (Enum.IsDefined(typeof(VehicleHash), cause)) return VehicleCause(cause);
            if (Enum.IsDefined(typeof(PedHash), cause)) return PedCause(cause);

            return "Unknown";
        }

        private static string WeaponCause(uint cause)
        {
            WeaponHash value = (WeaponHash)cause;
            return value.ToString();
        }

        private static string VehicleCause(uint cause)
        {
            VehicleHash value = (VehicleHash)cause;
            return value.ToString();
        }

        private static string PedCause(uint cause)
        {
            PedHash value = (PedHash)cause;
            return value.ToString();
        }

        public static SpawnPoint GetSpawnPoint(this Entity entity) => 
            !entity.IsValid() ? null : new SpawnPoint(entity.Position.X, entity.Position.Y, entity.Position.Z, entity.Heading);

        public static bool IsAPlayer(this Ped ped) => API.IsPedAPlayer(ped.Handle);

        public static int GetPlayerIndexFromPed(this Ped ped) => API.NetworkGetPlayerIndexFromPed(ped.Handle);

        public static bool IsValid(this Entity entity) => entity != null && entity.Exists();
        public static void RequestControl(this Entity ent) => Call(NETWORK_REQUEST_CONTROL_OF_ENTITY, ent);

        public static bool HasControlOfEntity(this Entity ent) => Call<bool>(NETWORK_HAS_CONTROL_OF_ENTITY, ent);

        public static int GetEntityNetworkID(this Entity ent) => Call<int>(PED_TO_NET, ent);

        public static int GetEntityFromNetworkID(this int netID) => Call<int>(NET_TO_PED, netID);

        public static void SetAsMissionEntity(this Entity ent) => Call(SET_ENTITY_AS_MISSION_ENTITY, ent, 1, 1);

        public static void SetAsNoLongerNeeded(this Entity ent) =>
            Call(SET_ENTITY_AS_NO_LONGER_NEEDED, ent);

        // Components/Variation

        public static int GetPedDrawableVariation(this Ped ped, Components componentID)
            => Call<int>(GET_PED_DRAWABLE_VARIATION, ped, (int)componentID);

        public static int GetPedTextureVariation(this Ped ped, Components componentID)
            => Call<int>(GET_PED_TEXTURE_VARIATION, ped, (int)componentID);

        public static int GetPedPropIndex(this Ped ped, Props propID)
            => Call<int>(GET_PED_PROP_INDEX, ped, (int)propID);

        public static int GetPedPropTextureIndex(this Ped ped, Props propID)
            => Call<int>(GET_PED_PROP_TEXTURE_INDEX, ped, (int)propID);

        public static void SetPedComponentVariation(this Ped ped, Components componentID, int drawableID, int textureID, int paletteID)
            => Call(SET_PED_COMPONENT_VARIATION, ped, (int)componentID, drawableID, textureID, paletteID);

        public static void SetPedPropIndex(this Ped ped, Props propID, int componentID, int drawableID, int textureID)
            => Call(SET_PED_PROP_INDEX, ped, (int)propID, componentID, drawableID, textureID, true);

        public static void SetPedHelmetPropIndex(this Ped ped, Props propID)
            => Call(SET_PED_HELMET_PROP_INDEX, ped, (int)propID);

        public enum Components { Head, Beard, Haid, Torso, Legs, Hands, Foot, BackAcc = 8, HeadAcc = 9, Decals = 10, Auxiliary = 11 }
        public enum Props { Helmets, Glasses, EarAcc }

        public static bool IsAPed(this Entity ent)
        {
            return ent != null && Call<bool>(IS_ENTITY_A_PED, ent);
        }

        public static bool IsAVehicle(this Entity ent)
        {
            return ent != null && Call<bool>(IS_ENTITY_A_VEHICLE, ent);
        }

        public static void OpenDoors(this Vehicle veh)
        {
            if (!veh.Exists()) return;
            foreach (VehicleDoor door in veh.Doors)
            {
                door.Open(false, true);
            }
        }

        public static void CloseDoors(this Vehicle veh)
        {
            if (!veh.Exists()) return;
            foreach (VehicleDoor door in veh.Doors)
            {
                door.Close(true);
            }
        }
    }
}
