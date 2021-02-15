using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;

namespace Upfirst.Utilities
{
    public class FilesOnServer
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FilesOnServer(IWebHostEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        
        public List<string> GetFilesOnServer()
        {
            // Get files into uploads folder
            string FilePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");

            string[] filePaths = Directory.GetFiles(FilePath);

            List<string> videosList = new List<string>();

            foreach (string filePath in filePaths)
            {
                videosList.Add(Path.GetFileName(filePath));
            }

            return videosList;
        }
    }
}
