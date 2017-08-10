using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileLister
{
    public class FileLister
    {
        public bool IncludeSubDirectories { get; set; }

        public FileLister(bool includeSubDirectories = true)
        {
            IncludeSubDirectories = includeSubDirectories;
        }

        public IEnumerable<FileInfo> ListFiles(string dir, string[] excludeExtintions)
        {
            return ListFiles(dir).Where(t => excludeExtintions.Any(s => !t.Extension.Equals(s)));
        }

        public IEnumerable<FileInfo> ListFiles(string dir)
        {
            if (!IncludeSubDirectories)
            {
                return MapFilesToFileInfo(dir);
            }

            var queue = new Queue<string>();
            var res = new List<FileInfo>();

            queue.Enqueue(dir);

            while (queue.Any())
            {
                string itemDequeue = queue.Dequeue();
                res.AddRange(MapFilesToFileInfo(itemDequeue));
                Directory.GetDirectories(itemDequeue).ToList().ForEach(t => queue.Enqueue(t));
            }

            return res;
        }

        private IEnumerable<FileInfo> MapFilesToFileInfo(string dir)
        {
            return Directory.GetFiles(dir).Select(t => new FileInfo(t));
        }
    }
}