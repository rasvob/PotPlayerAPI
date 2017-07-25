using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using PotPlayerApiLib;
using WinApiRemoteLib;

namespace PotPlayerAPI.Controllers
{
    public class PotController: Controller
    {
        public IActionResult Play()
        {
            Process[] processes = Process.GetProcesses();
            Process process = processes.FirstOrDefault(t => t.ProcessName.Equals("PotPlayerMini64", StringComparison.CurrentCultureIgnoreCase));

            var remote = new PotPlayerRemote(new ProcessWindow(process));
            remote.Pause();
            Thread.Sleep(1000);
            remote.Fullscreen();
            Thread.Sleep(1000);
            remote.Fullscreen();
            Thread.Sleep(1000);
            remote.Pause();
            return Ok();
        }

        public IActionResult Pause()
        {
            return BadRequest();
        }
    }
}