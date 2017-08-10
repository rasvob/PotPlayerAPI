using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PotPlayerAPI.ViewModels.VideoFiles;

namespace PotPlayerAPI.ViewComponents.VideoFiles
{
    public class VideoTableViewComponent: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<VideoViewModel> videoFiles, string tableTitle, string nameSortParam, string subtitlesSortParam, string currentSortParam, string sortBy, string currentSortAlt)
        {
            ViewBag.TableTitle = tableTitle;
            ViewBag.CurrentSort = currentSortParam;
            ViewBag.CurrentSortAlt = currentSortAlt;

            switch (sortBy)
            {
                case "movie":
                    ViewBag.Mov1 = nameSortParam;
                    ViewBag.Tv1 = currentSortParam;
                    ViewBag.Mov2 = subtitlesSortParam;
                    ViewBag.Tv2 = currentSortParam;
                    break;
                case "tv":
                    ViewBag.Mov1 = currentSortParam;
                    ViewBag.Tv1 = nameSortParam;
                    ViewBag.Mov2 = currentSortParam;
                    ViewBag.Tv2 = subtitlesSortParam;
                    break;
            }

            return View(videoFiles);
        }
    }
}