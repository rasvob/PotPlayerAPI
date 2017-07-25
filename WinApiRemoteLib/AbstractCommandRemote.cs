using System.Windows.Forms;
using System.Windows.Input;

namespace WinApiRemoteLib
{
    public abstract class AbstractCommandRemote
    {
        protected readonly ProcessWindow Window;
        protected readonly WindowFocuser Focuser;

        protected AbstractCommandRemote(ProcessWindow window)
        {
            Window = window;
            Focuser = new WindowFocuser();
        }

        protected void Command(Key key)
        {
            Focuser.FocusByHandle(Window.Handle, WindowMode.Restore);
            SendKeysInterop.PressKey(Window.Handle, key);
        }

        protected void CommandSequence(string keys)
        {
            Focuser.FocusByHandle(Window.Handle, WindowMode.Restore);
            SendKeys.SendWait(keys);
        }
    }
}