using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CommonClient.Objects;
using CommonClient.Utilities;

namespace CommonClient.Extensions
{
    public class CameraExtensions : BaseScript
    {
        public static async Task<Camera> FocusCamOnObjectWithInterpolation(Camera customCam, Vector3 camPos, Vector3 rotation, int interpolateTime = 4000, bool freezePlayer = true)
        {
            customCam.Position = camPos;
            customCam.Rotation = rotation;

            Camera gameCam = RetrieveGameCam();
            gameCam.IsActive = true;

            customCam.IsActive = true;
            await CamInterpolate(gameCam, customCam, interpolateTime, true, true, true);
            
            if (freezePlayer) SetLocalPlayerPropertiesWhileCamOn(true);

            return gameCam;
        }

        public static async void InterpolateCameraBack(Camera customCam, Camera gameCam)
        {
            if (gameCam == null || customCam == null) return;

            await CamInterpolate(customCam, gameCam, 1000, true, true, true);

            customCam.IsActive = false;
            customCam.Delete();
            customCam = null;

            gameCam.Delete();
            gameCam = null;

            SetLocalPlayerPropertiesWhileCamOn(false);
        }

        /// <summary>
        /// Will fade the screen out and back in while disabling the camera and fading the screen in
        /// </summary>
        public static void DisableCustomCamWithFading(Camera customCam, bool fadeIn = true, int fadeTime = 1500)
        {
            RageGame.FadeScreenOut(fadeTime);

            if (customCam.Exists())
            {
                customCam.IsActive = false;
                customCam.Delete();
            }

            if (!fadeIn) return;

            RageGame.FadeScreenIn(fadeTime);

            SetLocalPlayerPropertiesWhileCamOn(false);
        }

        public static Camera RetrieveGameCam()
        {
            Camera gamecam = new Camera(0);
            gamecam.FieldOfView = Function.Call<float>(Hash.GET_GAMEPLAY_CAM_FOV);
            gamecam.Position = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_COORD);
            gamecam.Rotation = Function.Call<Vector3>(Hash.GET_GAMEPLAY_CAM_ROT, 0);
            
            return gamecam;
        }

        private static async Task<bool> CamInterpolate(
            Camera camfrom, Camera camto,
            int totaltime,
            bool easeLocation, bool easeRotation, bool waitForCompletion,
            float x = 0f, float y = 0f, float z = 0f)
        {
            Function.Call(Hash.SET_CAM_ACTIVE_WITH_INTERP, camto, camfrom,
                totaltime, easeLocation, easeRotation);

            if (waitForCompletion) await Delay(totaltime);

            return true;
        }

        private static void SetLocalPlayerPropertiesWhileCamOn(bool on)
        {
            Function.Call(Hash.FREEZE_ENTITY_POSITION, Game.PlayerPed, on);
            Game.PlayerPed.IsInvincible = on;
        }

        public static void SetCameraPosRot(Camera cam, Vector3 sp, Vector3 rot)
        {
            cam.Position = sp;
            cam.Rotation = rot;
        }
    }
}
