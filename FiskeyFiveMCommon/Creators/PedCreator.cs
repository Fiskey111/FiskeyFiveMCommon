using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CommonClient.Objects;
using CommonClient.Utilities;

namespace CommonClient.Creators
{
    public class PedCreator : BaseScript
    {
        public static async Task<Ped> CreatePed(Vector3 spawn, Vehicle veh = null)
        {
            string model = PedModels.PModels[RandomNumber.RandomInt(PedModels.PModels.Length)];

            Model newModel = new Model(model);
            await newModel.Request(10000);

            if (veh == null) return await SpawnPed(newModel, spawn, 0f);

            return await SpawnPedInVehicle(model, veh);
        }

        public static async Task<Ped> CreatePed(Vector3 pos, float heading = 0f, Vehicle veh = null)
        {
            string model = PedModels.PModels[RandomNumber.RandomInt(PedModels.PModels.Length)];

            Model newModel = new Model(model);
            await newModel.Request(10000);

            if (veh == null) return await SpawnPed(newModel, pos, heading);

            return await SpawnPedInVehicle(model, veh);
        }

        public static async Task<Ped> CreatePed(string model, Vector3 pos, float heading = 0f, Vehicle veh = null)
        {
            Model newModel = new Model(model);
            await newModel.Request(10000);

            if (veh == null) return await SpawnPed(newModel, pos, heading);

            return await SpawnPedInVehicle(model, veh);
        }

        public static async Task<Ped> CreatePed(PedHash model, Vector3 pos, float heading = 0f, Vehicle veh = null)
        {
            Model newModel = new Model(model);
            await newModel.Request(10000);

            if (veh == null) return await SpawnPed(newModel, pos, heading);

            return await SpawnPedInVehicle(model, veh);
        }

        public static async Task<Ped> CreatePed(Model model, Vector3 pos, float heading = 0f, Vehicle veh = null)
        {
            if (veh == null) return await SpawnPed(model, pos, heading);

            return await SpawnPedInVehicle(model, veh);
        }

        private static async Task<Ped> SpawnPedInVehicle(Model model, Vehicle veh)
        {
            try
            {
                int handle = API.CreatePedInsideVehicle(veh.Handle, 4, (uint)model.Hash, -1, true, true);
                await Delay(0500);
                return new Ped(handle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SpawnPedInVehicle error: {ex}");
                return null;
            }
        }

        private static async Task<Ped> SpawnPed(Model model, Vector3 pos, float heading = 0f)
        {
            try
            {
                int handle = API.CreatePed(4, (uint)model.Hash, pos.X, pos.Y, pos.Z, heading, true, true);
                await Delay(0500);
                return new Ped(handle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SpawnPed error: {ex}");
                return null;
            }
        }
    }
}
