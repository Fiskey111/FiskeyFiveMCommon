using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CommonClient.Objects;

namespace CommonClient.Creators
{
    public class VehicleCreator : BaseScript
    {
        public static async Task<Vehicle> CreateVehicle(string model, Vector3 pos, float heading = 0f)
        {
            Model newModel = new Model(model);
            await newModel.Request(10000);

            return await SpawnVehicle(newModel, pos, heading);
        }

        public static async Task<Vehicle> CreateVehicle(VehicleHash model, Vector3 pos, float heading = 0f)
        {
            Model newModel = new Model(model);
            await newModel.Request(10000);

            return await SpawnVehicle(newModel, pos, heading);
        }

        public static async Task<Vehicle> CreateVehicle(Model model, Vector3 pos, float heading = 0f)
        {
            if (!model.IsLoaded)
            {
                await model.Request(10000);
            }
            return await SpawnVehicle(model, pos, heading);
        }

        private static async Task<Vehicle> SpawnVehicle(Model model, Vector3 pos, float heading = 0f)
        {
            try
            {
                int handle = Function.Call<int>(Hash.CREATE_VEHICLE, model.Hash, pos.X, pos.Y,
                    pos.Z, heading, true, true);
                await Delay(0500);
                return new Vehicle(handle);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SpawnVehicle error: {ex}");
                return null;
            }
        }
    }
}
