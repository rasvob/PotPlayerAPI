using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PotPlayerAPI.ViewModels.VideoFiles;

namespace PotPlayerAPI.ViewComponents.VideoFiles
{
    public class VideoTableViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<VideoViewModel> videoFiles, string tableTitle)
        {
            ViewBag.TableTitle = tableTitle;
            return View(videoFiles);
        }
    }
}