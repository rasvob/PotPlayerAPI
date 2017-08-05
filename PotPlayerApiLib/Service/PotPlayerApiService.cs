using System;
using System.Collections.Generic;
using System.Diagnostics;
using WinApiRemoteLib;

namespace PotPlayerApiLib.Service
{
    public class PotPlayerApiService: IPotPlayerApiService
    {
        public void InvokeRemoteCommand(IntPtr handle, PotPlayerAction action)
        {
            var remote = new PotPlayerRemote(new ProcessWindow() {Handle = handle});
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
    }
}