using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinApiRemoteLib;

namespace PotPlayerApiLib.Service
{
    public class PotPlayerApiService: IPotPlayerApiService
    {
        public void InvokeRemoteCommand(int handle, PotPlayerAction action)
        {
            var remote = new PotPlayerRemote(new ProcessWindow() {Handle = (IntPtr)handle});
            remote.DoAction(action);
        }

        public IEnumerable<ProcessWindow> GetProcessWindows()
        {
            return PotPlayerRemote.GetProcessWindowsForApp();
        }

        public void StartNewInstance(string path)
        {
            Process.Start(path);
        }

        public int GetNumber()
        {
            return 21;
        }
    }
}