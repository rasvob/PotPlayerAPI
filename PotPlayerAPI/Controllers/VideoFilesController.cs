using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PotPlayerApiLib;
using PotPlayerAPI.Models.AppSettings;
using PotPlayerAPI.ViewModels.VideoFiles;

namespace PotPlayerAPI.Controllers
{
    public class VideoFilesController : Controller
    {
        private readonly ILogger<VideoFilesController> _logger;
        private readonly IOptions<PotPlayerSettings> _options;

        public VideoFilesController(ILogger<VideoFilesController> logger, IOptions<PotPlayerSettings> options)
        {
            _logger = logger;
            _options = options;
        }
        
        public IActionResult Index(string sortParamMovie, string sortParamTvShows)
        {
            IEnumerable<VideoViewModel> OrderCollection(IEnumerable<VideoViewModel> models, string sortOrder)
            {
                switch (sortOrder)
                {
                    case "name":
                        return models.OrderBy(t => t.Name);
                    case "name_desc":
                        return models.OrderByDescending(t => t.Name);
                    case "subtitles":
                        return models.OrderBy(t => t.HasSubtitles);
                    case "subtitles_desc":
                        return models.OrderByDescending(t => t.HasSubtitles);
                    default:
                        return models;
                }
            }

            var lister = new FileLister.FileLister();
            IEnumerable<FileInfo> movies = lister.ListFiles(_options.Value.MoviesLocation);
            IEnumerable<FileInfo> tvSeries = lister.ListFiles(_options.Value.TvSeriesLocation);

            IEnumerable<IGrouping<string, FileInfo>> groupByMovies = movies.GroupBy(t => Path.GetFileNameWithoutExtension(t.FullName));
            IEnumerable<IGrouping<string, FileInfo>> groupByTvSeries = tvSeries.GroupBy(t => Path.GetFileNameWithoutExtension(t.FullName));

            var moviesVm = groupByMovies.Select(t => new VideoViewModel(t)).Where(t => t.FullPath != null);
            var tvSeriesVm = groupByTvSeries.Select(t => new VideoViewModel(t)).Where(t => t.FullPath != null);

            if (string.IsNullOrEmpty(sortParamMovie))
            {
                sortParamMovie = "name";
            }

            if (string.IsNullOrEmpty(sortParamTvShows))
            {
                sortParamTvShows = "name";
            }

            ViewBag.NameSortParamMovie = sortParamMovie == "name" ? "name_desc" : "name";
            ViewBag.SubtitlesSortParamMovie = sortParamMovie == "subtitles" ? "subtitles_desc" : "subtitles";
            ViewBag.CurrentSortMovie = sortParamMovie;

            ViewBag.NameSortParamTvShows = sortParamTvShows == "name" ? "name_desc" : "name";
            ViewBag.SubtitlesSortParamTvShows = sortParamTvShows == "subtitles" ? "subtitles_desc" : "subtitles";
            ViewBag.CurrentSortTvShows = sortParamTvShows;

            VideoFilesTableViewModel viewModel = new VideoFilesTableViewModel {Movies = OrderCollection(moviesVm, sortParamMovie), TvShows = OrderCollection(tvSeriesVm, sortParamTvShows) };
            return View(viewModel);
        }

        [Route("{videoPath}")]
        public async Task<IActionResult> PlayVideo(string videoPath)
        {
            try
            {
                PotPlayerRemote.PlayVideo(_options.Value.ExeLocation, videoPath);
                //PotPlayer has delay when loading file and setting window title by it
                await Task.Delay(1500);
                return RedirectToAction("Remote", "Pot");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return RedirectToAction("Index");
            }
        }
    }
}