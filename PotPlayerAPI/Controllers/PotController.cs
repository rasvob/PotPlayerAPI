using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotPlayerApiLib;
using PotPlayerAPI.ViewModels.Pot;
using WinApiRemoteLib;

namespace PotPlayerAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PotController: Controller
    {
        private readonly ILogger<PotController> _logger;
        public PotController(ILogger<PotController> logger)
        {
            _logger = logger;
        }

        public IActionResult Remote()
        {
            var vm = new PotRemoteViewModel
            {
                Windows = PotPlayerRemote.GetProcessWindowsForApp(),
                PotRemotePost = new PotRemotePostViewModel()
            };
            
            return View(vm);
        }

        //TODO: Add JS to handle POSTs
        [HttpPost]
        public IActionResult Remote([FromForm]PotRemotePostViewModel viewModel)
        {
            TempData["Error"] = false;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = true;
                return RedirectToAction("Remote");
            }

            try
            {
                var remote = new PotPlayerRemote(new ProcessWindow() {Handle = (IntPtr) viewModel.Handle});
                remote.DoAction(viewModel.PotPlayerAction);
                _logger.LogInformation($"Performed {viewModel.PotPlayerAction} action on {viewModel.Handle}");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                TempData["Error"] = true;
            }

            TempData["SelectedHandle"] = viewModel.Handle;
            return RedirectToAction("Remote");
        }

        public IActionResult IsRunning()
        {
            return Ok();
        }

        public IActionResult ListWindows()
        {
            return Json(PotPlayerRemote.GetProcessWindowsForApp());
        }

        [HttpPost("{potAction}")]
        public IActionResult Command(PotPlayerAction potAction, [FromBody] ProcessWindow window)
        {
            try
            {
                var remote = new PotPlayerRemote(window);
                remote.DoAction(potAction);
                return Ok();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
            }

            return BadRequest();
        }

        public IActionResult StartPlayer()
        {
            try
            {
                Process.Start(@"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                TempData["Error"] = true;
            }
            return RedirectToAction("Remote");
        }
    }
}