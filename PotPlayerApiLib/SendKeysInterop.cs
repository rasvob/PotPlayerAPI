using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Input;

namespace PotPlayerApiLib
{
    public static class SendKeysInterop
    {
        private const int WmKeydown = 0x100;
        private const int WmKeyup = 0x101;

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

        public static void PressKeys(IntPtr handle, IEnumerable<Key> keys)
        {
            foreach (Key key in keys)
            {
                PressKey(handle, key);
            }
        }
    }
}