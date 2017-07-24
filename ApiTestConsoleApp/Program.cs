using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PotPlayerApiLib;

namespace ApiTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            WindowFocuser focuser = new WindowFocuser();
            IEnumerable<Process> processes = focuser.ListProcesses().Where(t => t.ProcessName.StartsWith("Sub", StringComparison.CurrentCultureIgnoreCase));
            //Process process = focuser.ListProcesses().FirstOrDefault(t => t.ProcessName.StartsWith("PotPlayerMini64", StringComparison.CurrentCultureIgnoreCase));
            Process process = focuser.ListProcesses().FirstOrDefault(t => t.ProcessName.StartsWith("sub", StringComparison.CurrentCultureIgnoreCase));

            var remote = new PotPlayerRemote(new PotPlayerWindow(process));
            remote.Pause();
            Console.ReadKey();
        }
    }
}
