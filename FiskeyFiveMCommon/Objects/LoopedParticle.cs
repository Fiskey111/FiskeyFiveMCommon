using System.Drawing;
using System.Linq;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.Objects
{
    public class LoopedParticle
    {
        public string AssetName { get; }
        public string ParticleName { get; }
        public int Handle { get; private set; }
        public float Scale { get; private set; }
        public Vector3 Position { get; private set; }

        public LoopedParticle(string assetName, string particleName, Ped ped, Bone bone, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            Position = offset;
            LoadAsset();
            Handle = API.StartParticleFxLoopedOnEntityBone_2(particleName,
                ped.Handle,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                (int)bone,
                scale,
                false, false, false);
            SetScale(Scale);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            Position = offset;
            LoadAsset();
            Handle = API.StartParticleFxLoopedOnEntity_2(particleName,
                entity.Handle,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                scale,
                false, false, false);
            SetScale(Scale);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, int boneIndex, Vector3 offset, Vector3 rotation, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            Position = offset;
            LoadAsset();
            Handle = API.StartParticleFxLoopedOnEntityBone_2(particleName,
                entity.Handle,
                offset.X, offset.Y, offset.Z,
                rotation.X, rotation.Y, rotation.Z,
                boneIndex,
                scale,
                false, false, false);
            SetScale(Scale);
        }

        public LoopedParticle(string assetName, string particleName, Entity entity, string boneName, Vector3 offset, Vector3 rotation, float scale)
            : this(assetName, particleName, entity, entity.Bones[boneName], offset, rotation, scale)
        {
        }

        public LoopedParticle(string assetName, string particleName, Vector3 position, float scale)
        {
            AssetName = assetName;
            ParticleName = particleName;
            Scale = scale;
            Position = position;
            LoadAsset();
            Handle = API.StartParticleFxLoopedAtCoord(particleName,
                position.X, position.Y, position.Z,
                0f, 0f, 0f,
                scale,
                false, false, false, false);
            SetScale(Scale);
        }

        private bool LoadAsset()
        {
            API.RequestNamedPtfxAsset(AssetName);
            var waitCounter = 0;

            while (!API.HasNamedPtfxAssetLoaded(AssetName))
            {
                BaseScript.Delay(0010);

                if (waitCounter > 10)
                {
                    Debug.WriteLine("break");
                    break;
                }
                waitCounter++;
            }

            if (waitCounter >= 10) return false;
            API.SetPtfxAssetNextCall(AssetName);
            return true;
        }

        public void SetOffsets(Vector3 offset, Vector3 rotation)
        {
            Position = offset;
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
            API.SetParticleFxLoopedScale(Handle, Scale);
        }

        public bool IsValid() => API.DoesParticleFxLoopedExist(Handle);

        public void Stop() => API.StopParticleFxLooped(Handle, false);
    }
}
