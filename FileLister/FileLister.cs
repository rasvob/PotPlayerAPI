using System.Collections.Generic;
using System.IO;

namespace FileLister
{
    public class FileLister
    {
        public string Directory { get; set; }
        public bool IncludeSubDirectories { get; set; }

        public FileLister(string directory, bool includeSubDirectories)
        {
            Directory = directory;
            IncludeSubDirectories = includeSubDirectories;
        }

        public IEnumerable<FileInfo> ListFiles(string[] excludeExtintions)
        {
            return null;
        }
    }
}