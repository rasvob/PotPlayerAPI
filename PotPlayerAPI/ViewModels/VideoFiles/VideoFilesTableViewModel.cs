using System.Collections.Generic;

namespace PotPlayerAPI.ViewModels.VideoFiles
{
    public class VideoFilesTableViewModel
    {
        public IEnumerable<VideoViewModel> Movies { get; set; }
        public IEnumerable<VideoViewModel> TvShows { get; set; }
    }
}