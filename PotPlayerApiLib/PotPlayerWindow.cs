using System;
using System.Diagnostics;

namespace PotPlayerApiLib
{
    public class PotPlayerWindow
    {
        public string ProcessName { get; set; }
        public string Title { get; set; }
        public IntPtr Handle { get; set; }

        public PotPlayerWindow(Process process)
        {
            ProcessName = process.ProcessName;
            Title = process.MainWindowTitle;
            Handle = process.MainWindowHandle;
        }
    }
}