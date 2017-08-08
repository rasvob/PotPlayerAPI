using Microsoft.AspNetCore.Mvc;

namespace PotPlayerAPI.Controllers
{
    public class VideoFilesController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return
            View();
        }
    }
}