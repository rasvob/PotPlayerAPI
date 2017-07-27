﻿using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotPlayerApiLib;
using PotPlayerAPI.ViewModels.Pot;
using Remotion.Linq.Parsing.Structure;
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
                Windows = PotPlayerRemote.GetProcessWindowsForApp()
            };
            
            return View(vm);
        }

        [HttpPost]
        public IActionResult Remote(PotRemoteViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = true;
            }

            try
            {
                var remote = new PotPlayerRemote(new ProcessWindow()
                {
                    Handle = (IntPtr)viewModel.PotRemotePost.Handle
                });
                remote.DoAction(viewModel.PotRemotePost.PotPlayerAction);
                _logger.LogInformation($"Performed {viewModel.PotRemotePost.PotPlayerAction} action");
                return Ok();
            }
            catch (Exception exception) when (exception is ArgumentNullException || exception is ArgumentOutOfRangeException)
            {
                _logger.LogError(exception.Message);
            }

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
                _logger.LogInformation($"Performed {potAction} action");
                return Ok();
            }
            catch (Exception exception) when(exception is ArgumentNullException || exception is ArgumentOutOfRangeException)
            {
                _logger.LogError(exception.Message);
            }

            return BadRequest();
        }
    }
}