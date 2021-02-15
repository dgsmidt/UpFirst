﻿using System.Collections.Generic;
using System.Linq;

namespace Upfirst.Models
{
    public class JsonFiles
    {
        public UploadFilesResult[] files;
        public string TempFolder { get; set; }
        public JsonFiles(List<UploadFilesResult> filesList)
        {
            files = new UploadFilesResult[filesList.Count];
            for (int i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }
        }
    }
}
