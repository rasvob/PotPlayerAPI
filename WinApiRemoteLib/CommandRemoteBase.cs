using System.Windows.Forms;
using System.Windows.Input;

namespace WinApiRemoteLib
{
    public class CommandRemoteBase
    {
        protected readonly ProcessWindow Window;
        protected readonly WindowFocuser Focuser;

        public CommandRemoteBase(ProcessWindow window)
        {
            Window = window;
            Focuser = new WindowFocuser();
        }

        public void Command(Key key)
        {
            Focuser.FocusByHandle(Window.Handle, WindowMode.Restore);
            SendKeysInterop.PressKey(Window.Handle, key);
        }

        public void CommandSequence(string keys)
        {
            Focuser.FocusByHandle(Window.Handle, WindowMode.Restore);
            SendKeys.SendWait(keys);
        }
    }
}