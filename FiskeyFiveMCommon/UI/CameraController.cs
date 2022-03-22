using CitizenFX.Core;
using CitizenFX.Core.Native;
using System.Threading.Tasks;
using static CitizenFX.Core.Control;

namespace CommonClient.UI
{
    public class CameraController : BaseScript
    {
        public CameraController() { }
        private Camera _freeCam, _gameCam;
        public bool IsRunning { get; private set; }
        private Task _task;

        public void Create(bool create)
        {
            IsRunning = create;

            if (create)
            {
                _gameCam = RetrieveGameCam();
                _freeCam = CreateCamera(Game.PlayerPed.GetOffsetPosition(new Vector3(0f, -1f, 0f)), Game.PlayerPed.Position, _gameCam);
                _task = new Task(Control);
                _task.Start();
                Game.Player.CanControlCharacter = false;
            }
            else
            {
                EndCamera(_freeCam, _gameCam);
                _task.Dispose();
                _freeCam = null;
                _gameCam = null;
                Game.Player.CanControlCharacter = true;
            }
        }

        private async void Control()
        {
            while (IsRunning)
            {
                await Delay(0);

                float ud = 0f, lr = 0f, lr2 = 0f, ud2 = 0f, vert = 0f;

                if (IsTrue(MoveLeftOnly))
                {
                    lr = -0.10f;
                    if (IsTrue(Sprint)) lr = lr * 2f;
                }
                else if (IsTrue(MoveRightOnly))
                {
                    lr = 0.10f;
                    if (IsTrue(Sprint)) lr = lr * 2f;
                }
                else if (IsTrue(MoveUpOnly))
                {
                    ud = 0.10f;
                    if (IsTrue(Sprint)) ud = ud * 2f;
                }
                else if (IsTrue(MoveDownOnly))
                {
                    ud = -0.10f;
                    if (IsTrue(Sprint)) ud = ud * 2f;
                }
                else if (IsTrue(Sprint))
                {
                    vert = 0.10f;
                }
                else if (IsTrue(FrontendRs))
                {
                    vert = -0.10f;
                }

                if (IsTrue(Aim)) // Right-Click
                {
                    Rotate(ud * 3f, 0f, lr * 3f);
                }
                else
                {
                    Move(lr, ud, vert);
                }

                if (IsTrue(WeaponWheelNext))
                {
                    _freeCam.FieldOfView = _freeCam.FieldOfView + 0.25f;
                }
                else if (IsTrue(WeaponWheelPrev))
                {
                    _freeCam.FieldOfView = _freeCam.FieldOfView - 0.25f;
                }
            }
        }

        private void Move(float x = 0.0f, float y = 0.0f, float z = 0.0f) => _freeCam.Position = _freeCam.GetOffsetPosition(new Vector3(x, y, z));
        private void Rotate(float x = 0.0f, float y = 0.0f, float z = 0.0f) => _freeCam.Rotation = new Vector3(_freeCam.Rotation.X - x, _freeCam.Rotation.Y - y, _freeCam.Rotation.Z - z);

        private bool IsTrue(Control control) => Game.IsControlPressed(0, control);

        private Camera CreateCamera(Vector3 position, Vector3 facePosition, Camera gameCam, bool active = true)
        {
            Game.Player.CanControlCharacter = false;
            Camera c = new Camera(API.CreateCam("DEFAULT_SCRIPTED_CAMERA", true));
            c.Position = position;
            c.PointAt(facePosition);
            c.StopPointing();
            API.RenderScriptCams(true, false, 0, true, true);
            c.IsActive = active;
            API.SetCamActive(gameCam.Handle, false);
            return c;
        }

        private Camera RetrieveGameCam()
        {
            return World.CreateCamera(API.GetGameplayCamCoord(), API.GetGameplayCamRot(0), API.GetGameplayCamFov());
        }
        private void EndCamera(Camera cam, Camera gameCam)
        {
            API.SetCamActive(cam.Handle, false);
            API.DestroyCam(cam.Handle, true);
            API.RenderScriptCams(false, false, 1, true, true);
            API.SetCamActive(gameCam.Handle, true);
            Game.Player.CanControlCharacter = true;
        }
    }
}
