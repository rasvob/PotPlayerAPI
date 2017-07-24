using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PotPlayerApiLib;

namespace PotPlayerAPI.Controllers
{
    public class PotController: Controller
    {
        public IActionResult Play()
        {
            Process[] processes = Process.GetProcesses();
            //Process process = processes.FirstOrDefault(t => t.ProcessName.Equals("PotPlayerMini64", StringComparison.CurrentCultureIgnoreCase));
            Process process = processes.FirstOrDefault(t => t.ProcessName.StartsWith("subli", StringComparison.CurrentCultureIgnoreCase));

            var remote = new PotPlayerRemote(new PotPlayerWindow(process));
            remote.Pause();

            return Ok();
        }

        public IActionResult Pause()
        {
            return BadRequest();
        }
    }
}