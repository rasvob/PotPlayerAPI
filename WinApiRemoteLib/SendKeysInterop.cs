using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace WinApiRemoteLib
{
    public static class SendKeysInterop
    {
        private const int WmKeydown = 0x100;
        private const int WmKeyup = 0x101;
        private const int WmSysKeydown = 0x0104;
        private const int WmSysKeyup = 0x0105;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        internal static extern bool PostMessage(IntPtr handle, uint msg, int wParam, int lParam);

        public static bool PressKey(IntPtr handle, Key key)
        {
            return PostMessage(handle, WmKeydown, KeyInterop.VirtualKeyFromKey(key), 0);
        }

        public static bool ReleaseKey(IntPtr handle, Key key)
        {
            return PostMessage(handle, WmKeyup, KeyInterop.VirtualKeyFromKey(key), 0);
        }

        public static bool HoldKeyDown(IntPtr handle, Key key)
        {
            return PostMessage(handle, WmSysKeydown, KeyInterop.VirtualKeyFromKey(key), 0);
        }

        public static bool ReleaseKeyUp(IntPtr handle, Key key)
        {
            return PostMessage(handle, WmSysKeyup, KeyInterop.VirtualKeyFromKey(key), 0);
        }

        public static void PressKeys(IntPtr handle, IEnumerable<Key> keys)
        {
            foreach (Key key in keys)
            {
                PressKey(handle, key);
            }
        }
    }
}