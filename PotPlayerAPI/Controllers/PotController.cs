using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotPlayerApiLib;
using PotPlayerApiLib.Service;
using PotPlayerAPI.BusinessLogic;
using PotPlayerAPI.ViewModels.Pot;
using Remotion.Linq.Parsing.Structure;
using WinApiRemoteLib;

namespace PotPlayerAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PotController: Controller
    {
        private readonly ILogger<PotController> _logger;
        private readonly IPotPlayerApiService _potPlayerServiceClient;

        public PotController(ILogger<PotController> logger, IPotPlayerApiService potPlayerServiceClient)
        {
            _logger = logger;
            _potPlayerServiceClient = potPlayerServiceClient;
        }

        public IActionResult Remote()
        {
            IEnumerable<ProcessWindow> windows;
            try
            {
                windows = _potPlayerServiceClient.GetProcessWindows();
            }
            catch (EndpointNotFoundException exception)
            {
                _logger.LogError(exception.Message);
                windows = new List<ProcessWindow>();
                TempData["ServiceNotRunning"] = true;
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message);
                windows = new List<ProcessWindow>();
            }

            var vm = new PotRemoteViewModel
            {
                Windows = windows,
                PotRemotePost = new PotRemotePostViewModel()
            };
            
            return View(vm);
        }

        //TODO: Add JS to handle POSTs
        [HttpPost]
        public IActionResult Remote([FromForm]PotRemotePostViewModel viewModel)
        {
            TempData["Error"] = false;
            TempData["ServiceNotRunning"] = false;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = true;
                return RedirectToAction("Remote");
            }

            try
            {
                _potPlayerServiceClient.InvokeRemoteCommand((IntPtr) viewModel.Handle, viewModel.PotPlayerAction);
                _logger.LogInformation($"Performed {viewModel.PotPlayerAction} action on {viewModel.Handle}");
            }
            catch (EndpointNotFoundException exception)
            {
                _logger.LogError(exception.Message);
                TempData["ServiceNotRunning"] = true;
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
            try
            {
                IEnumerable<ProcessWindow> data = _potPlayerServiceClient.GetProcessWindows();
                return Json(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        [HttpPost("{potAction}")]
        public IActionResult Command(PotPlayerAction potAction, [FromBody] ProcessWindow window)
        {
            try
            {
                _potPlayerServiceClient.InvokeRemoteCommand(window.Handle, potAction);
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
                _potPlayerServiceClient.StartNewInstance(@"C:\Program Files\DAUM\PotPlayer\PotPlayerMini64.exe");
            }
            catch (EndpointNotFoundException exception)
            {
                _logger.LogError(exception.Message);
                TempData["ServiceNotRunning"] = true;
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