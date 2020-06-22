using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClient.UI
{
    public class InputProcessing
    {
        public static InputMethod GetInputMethod() => API.GetLastInputMethod(0) ? InputMethod.Keyboard : InputMethod.Controller;

        public enum InputMethod { Keyboard, Controller }
    }
}
