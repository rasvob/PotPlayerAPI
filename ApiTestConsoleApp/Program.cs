using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using PotPlayerApiLib;
using WinApiRemoteLib;

namespace ApiTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<Process> processes = Process.GetProcesses().Where(t => t.ProcessName.ToLower().Contains("ai"));
            Process process = Process.GetProcesses().FirstOrDefault(t => t.ProcessName.StartsWith("PotPlayerMini64", StringComparison.CurrentCultureIgnoreCase));

            var remote = new PotPlayerRemote(new ProcessWindow(process));
            remote.Pause();
            Thread.Sleep(1000);
            remote.Forward();
            Thread.Sleep(1000);
            remote.Forward();
            Thread.Sleep(1000);
            remote.Rewind();

            Console.ReadKey();
        }
    }
}
