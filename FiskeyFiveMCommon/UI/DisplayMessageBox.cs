using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;

namespace CommonClient.UI
{
    public class DisplayMessageBox : BaseScript
    {
        public static async Task<string> DisplayBox(int maxLength, string windowTitle, string defaultText)
        {
            var value = await GetBox(15, windowTitle, defaultText);
            while (value == null) await Delay(0);
            return value;
        }

        private static async Task<string> GetBox(int maxLength, string windowTitle, string defaultText)
        {
            Function.Call(Hash.DISPLAY_ONSCREEN_KEYBOARD, true, "", "", defaultText, "", "", "", maxLength + 1);
            var scaleform = new Scaleform("TEXT_INPUT_BOX");
            scaleform.CallFunction("SET_TEXT_BOX", "", windowTitle, defaultText);

            var update = 0;
            while (update == 0)
            {
                scaleform.Render2D();
                try
                {
                    update = Function.Call<int>(Hash.UPDATE_ONSCREEN_KEYBOARD);
                }
                catch (NullReferenceException)
                {
                }
                await Delay(1);
            }
            string result;
            try
            {
                result = Function.Call<string>(Hash.GET_ONSCREEN_KEYBOARD_RESULT);
            }
            catch (NullReferenceException)
            {
                result = null;
            }
            return result;
        }
    }
}
