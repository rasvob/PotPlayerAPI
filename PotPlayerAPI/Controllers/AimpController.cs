using System;
using AimpApiLib;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PotPlayerApiLib;
using WinApiRemoteLib;

namespace PotPlayerAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class AimpController: Controller
    {
        private readonly ILogger<AimpController> _logger;

        public AimpController(ILogger<AimpController> logger)
        {
            _logger = logger;
        }

        public IActionResult Remote()
        {
            return View();
        }

        public IActionResult IsRunning()
        {
            return Ok();
        }

        [HttpPost("{aimpAction}")]
        public IActionResult Command(AimpActions aimpAction)
        {
            try
            {
                ProcessWindow app = AimpRemote.GetProcessWindowForApp();
                var remote = new AimpRemote(app);
                remote.DoAction(aimpAction);
                _logger.LogInformation($"Performed {aimpAction} action");
                return Ok();
            }
            catch (Exception exception) when (exception is ArgumentNullException || exception is ArgumentOutOfRangeException)
            {
                _logger.LogError(exception.Message);
            }

            return BadRequest();
        }
    }
}