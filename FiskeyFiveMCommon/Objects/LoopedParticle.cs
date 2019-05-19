using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.Objects
{
    public class LoopedParticle
    {
        public string AssetName { get; }
        public string ParticleName { get; }
        public uint Handle { get; }
        public bool Exists { get; private set; }
        public Prop Prop { get; set; }
        public float Scale { get; set; }

        public LoopedParticle(string assetName, string particleName, Ped ped, Bone bone, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            LoadAsset();
            Handle = Function.Call<uint>(Hash._START_PARTICLE_FX_LOOPED_ON_ENTITY_BONE_2, particleName,
                ped,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                (int)bone,
                scale,
                false, false, false);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            LoadAsset();
            // Network
            Handle = Function.Call<uint>(Hash._START_PARTICLE_FX_LOOPED_ON_ENTITY_2, particleName,
                entity,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                scale,
                false, false, false);

            //            Handle = Function.Call<uint>(Hash.START_PARTICLE_FX_LOOPED_ON_ENTITY, particleName,
            //                entity,
            //                offset.X, offset.Y, offset.Z,
            //                rotation.X, rotation.Y, rotation.Z,
            //                scale,
            //                false, false, false);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, int boneIndex, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            LoadAsset();
            Handle = Function.Call<uint>(Hash._START_PARTICLE_FX_LOOPED_ON_ENTITY_BONE_2, particleName,
                entity,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                boneIndex,
                scale,
                false, false, false);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, string boneName, Vector3 offset, Vector3 rotation, float scale)
            : this(assetName, particleName, entity, entity.Bones[boneName], offset, rotation, scale)
        {
        }

        public LoopedParticle(string assetName, string particleName, Vector3 position, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            LoadAsset();
            Handle = Function.Call<uint>(Hash.START_PARTICLE_FX_LOOPED_AT_COORD, particleName,
                position.X, position.Y, position.Z,
                rotation.X, rotation.Y, rotation.Z,
                scale,
                false, false, false, false);
        }

        private void LoadAsset()
        {
            Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, AssetName);
            var waitCounter = 10;
            while (!Function.Call<bool>(Hash.HAS_PTFX_ASSET_LOADED, AssetName) && waitCounter > 0)
            {
                BaseScript.Delay(10);
                waitCounter--;
            }

            Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, AssetName);
            this.Exists = true;
        }

        public void SetOffsets(Vector3 offset, Vector3 rotation)
        {
            Function.Call(Hash.SET_PARTICLE_FX_LOOPED_OFFSETS, Handle,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z);
        }

        public void SetColor(Color color)
        {
            Function.Call(Hash.SET_PARTICLE_FX_LOOPED_COLOUR, Handle, color.R / 255f, color.G / 255f, color.B / 255f, false);
            Function.Call(Hash.SET_PARTICLE_FX_LOOPED_ALPHA, Handle, color.A / 255f);
        }

        public void SetScale(float scale)
        {
            Scale = scale;
            Function.Call(Hash.SET_PARTICLE_FX_LOOPED_SCALE, Handle, scale);
            //            BaseScript.TriggerServerEvent(isPaused ? "Unpause_PTFX_Everyone" : "Pause_PTFX_Everyone",
            //                Game.Player.ServerId);
        }

        public bool IsValid() => Function.Call<bool>(Hash.DOES_PARTICLE_FX_LOOPED_EXIST, Handle);

        public void Stop()
        {
            Function.Call(Hash.STOP_PARTICLE_FX_LOOPED, Handle, false);
            Exists = false;
        }

        public static implicit operator bool(LoopedParticle value) => value.Exists;
    }
}
