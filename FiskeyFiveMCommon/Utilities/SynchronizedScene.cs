using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using CommonClient.Objects;

namespace CommonClient.Utilities
{
    public class SynchronizedScene : BaseScript
    {
        public int Handle { get; private set; }
        public Vector3 Position { get; private set; }
        public Vector3 Rotation { get; private set; }

        /// <summary>
        /// Gets or sets the phase of the scene
        /// </summary>
        public float Phase { get { return GetPhase(); } set { SetPhase(value); } }
        /// <summary>
        /// Gets or sets the playback rate of the scene
        /// </summary>
        public float Rate { get { return GetRate(); } set { SetRate(value); } }
        public bool IsRunning { get { return IsSceneRunning(); } }
        public bool Looped { get { return IsLooped(); } set { SetLooped(value); } }
        public bool IsValid { get { return Handle != 0; } }
        public bool OcclusionPortal { set { API.SetSynchronizedSceneOcclusionPortal(this.Handle, value); } }
        
        public SynchronizedScene(Vector3 position, Vector3 rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Handle = API.CreateSynchronizedScene(Position.X, Position.Y, Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
        }

        public SynchronizedScene()
        {
            this.Position = Vector3.Zero;
            this.Rotation = Vector3.Zero;
            this.Handle = API.CreateSynchronizedScene(Position.X, Position.Y, Position.Z, Rotation.X, Rotation.Y, Rotation.Z, 2);
        }

        public void Dispose()
        {
            API.DisposeSynchronizedScene(Handle);
            this.Handle = 0;
        }

        public void Detach()
        {
            API.DetachSynchronizedScene(Handle);
        }

        public void AttachToEntity(Entity entity, int boneIndex)
        {
            API.AttachSynchronizedSceneToEntity(Handle, entity.Handle, boneIndex);
        }

        private void SetLooped(bool looped)
        {
            API.SetSynchronizedSceneLooped(Handle, looped);
        }

        private bool IsLooped()
        {
            return API.IsSynchronizedSceneLooped(Handle);
        }

        private bool IsSceneRunning()
        {
            return API.IsSynchronizedSceneRunning(Handle);
        }

        private float GetPhase()
        {
            return API.GetSynchronizedScenePhase(Handle);
        }

        private void SetPhase(float phase)
        {
            API.SetSynchronizedScenePhase(Handle, phase);
        }

        private float GetRate()
        {
            return API.GetSynchronizedSceneRate(Handle);
        }

        private void SetRate(float rate)
        {
            API.SetSynchronizedSceneRate(Handle, rate);
        }

        public async void AddTask(Ped ped, string animDic, string animName, float speed, float speedMulti, int duration, int flag, float playbackRate)
        {
            API.TaskSynchronizedScene(ped.Handle, Handle, animDic, animName, speed, speedMulti, duration, flag, playbackRate, 0);

            AnimationDictionary animDict = new AnimationDictionary(animDic);
            animDict.LoadAndWait();
            while (!animDict.IsLoaded)
            {
                await Delay(0);
            }
            if (animDict.IsLoaded) return;
            Screen.ShowNotification("Animation dictionary not loaded");
        }
        
        public void DestroyAnimatedCam(bool ease = true, int easetime = 3000)
        {
            API.RenderScriptCams(false, ease, easetime, true, false);
        }
    }
}
