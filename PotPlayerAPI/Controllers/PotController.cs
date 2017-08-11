using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PotPlayerApiLib;
using PotPlayerAPI.Models.AppSettings;
using PotPlayerAPI.ViewModels.Pot;
using WinApiRemoteLib;

namespace PotPlayerAPI.Controllers
{
    //[Route("[controller]/[action]")]
    public class PotController: Controller
    {
        private readonly ILogger<PotController> _logger;
        private readonly IOptions<PotPlayerSettings> _options;

        public PotController(ILogger<PotController> logger, IOptions<PotPlayerSettings> options)
        {
            _logger = logger;
            _options = options;
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

        [HttpPost]
        public IActionResult AjaxRemote([FromBody]PotRemotePostViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var remote = new PotPlayerRemote(new ProcessWindow() { Handle = (IntPtr)viewModel.Handle });
                remote.DoAction(viewModel.PotPlayerAction);
                _logger.LogInformation($"Performed {viewModel.PotPlayerAction} action on {viewModel.Handle}");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                return BadRequest();
            }

            return Ok();
        }

        public async Task<IActionResult> StartPlayer()
        {
            try
            {
                await Task.Run(() => Process.Start(_options.Value.ExeLocation));
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