using System.IO;
using System.Linq;

namespace PotPlayerAPI.ViewModels.VideoFiles
{
    public class VideoViewModel
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
        public bool HasSubtitles { get; set; }

        public VideoViewModel()
        {
        }

        public VideoViewModel(IGrouping<string, FileInfo> infos)
        {
            Name = infos.Key;
            HasSubtitles = infos.Count() > 1;
            FullPath = infos.FirstOrDefault(s => !s.Extension.Equals(".srt"))?.FullName;
        }
    }
}