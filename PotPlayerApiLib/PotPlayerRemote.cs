using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace PotPlayerApiLib
{
    public class PotPlayerRemote
    {
        private readonly PotPlayerWindow _window;
        private readonly WindowFocuser _focuser;

        public PotPlayerRemote(PotPlayerWindow window)
        {
            _window = window;
            _focuser = new WindowFocuser();
        }

        public void Pause()
        {
            _focuser.FocusByHandle(_window.Handle, WindowMode.Show);
            //SendKeysInterop.PressKey(_window.Handle, Key.Space);
            WindowFocuser.Rect pos = _focuser.GetWindowPosition(_window.Handle);
            //VirtualMouse.Move((pos.Left + pos.Right) / 2, (pos.Top + pos.Bottom) / 2);
            System.Windows.Forms.Cursor.Position = new Point(2000, 500);
            VirtualMouse.LeftClick();
            VirtualMouse.RightClick();
            //SendKeys.SendWait(" ");
        }

        public WindowFocuser.Rect GetPos()
        {
            return _focuser.GetWindowPosition(_window.Handle);
        }
    }
}