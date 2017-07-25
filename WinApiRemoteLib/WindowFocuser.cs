using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace WinApiRemoteLib
{
    public class WindowFocuser
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }

        public bool FocusByProcessName(string name)
        {
            return FocusByProcessName(name, WindowMode.Restore);
        }

        public bool FocusByProcessName(string name, WindowMode mode)
        {
            var win = ListProcesses()
                .FirstOrDefault(t => t.ProcessName.Equals(name, StringComparison.CurrentCultureIgnoreCase));

            if (win == null)
                return false;

            FocusByHandle(win.MainWindowHandle, mode);
            return true;
        }

        public void FocusByHandle(IntPtr handlePtr, WindowMode mode)
        {
            ShowWindow(handlePtr, (int) mode);
            SetForegroundWindow(handlePtr);
        }

        public void FocusByHandle(IntPtr handlePtr)
        {
            FocusByHandle(handlePtr, WindowMode.Restore);
        }

        public Rect GetWindowPosition(IntPtr handle)
        {
            Rect res = new Rect();
            GetWindowRect(handle, ref res);
            return res;
        }

        public IEnumerable<Process> ListProcesses()
        {
            return Process.GetProcesses();
        }

        public IEnumerable<Process> ProcessesFilteredByProcessName(string name)
        {
            return ListProcesses().Where(t => t.ProcessName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}