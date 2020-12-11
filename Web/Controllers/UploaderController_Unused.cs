using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Web.Filters;
using Web.Hubs;
using Web.Utilities;

namespace Web.Controllers
{
    public class UploaderController_Unused : Controller
    {
        private IWebHostEnvironment hostingEnvironment;
        private readonly IHubContext<UploadHub> _hubContext;
        private readonly string[] _permittedExtensions = { ".txt" };
        private readonly long _fileSizeLimit;
        private readonly string _targetFilePath;
        private readonly ILogger<UploaderController_Unused> _logger;

        // Get the default form options so that we can use them to set the default 
        // limits for request body data.
        private static readonly FormOptions _defaultFormOptions = new FormOptions();
        public UploaderController_Unused(IWebHostEnvironment hostingEnvironment, IHubContext<UploadHub> hubContext,
            IConfiguration config, ILogger<UploaderController_Unused> logger)
        {
            _logger = logger;

            this.hostingEnvironment = hostingEnvironment;
            _hubContext = hubContext;
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");

            // To save physical files to the temporary files folder, use:
            //_targetFilePath = Path.GetTempPath();
        }

        // The following upload methods:
        //
        // 1. Disable the form value model binding to take control of handling 
        //    potentially large files.
        //
        // 2. Typically, antiforgery tokens are sent in request body. Since we 
        //    don't want to read the request body early, the tokens are sent via 
        //    headers. The antiforgery token filter first looks for tokens in 
        //    the request header and then falls back to reading the body.

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
        [HttpPost]
        [DisableFormValueModelBinding]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadPhysical()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File",
                    $"The request couldn't be processed (Error 1).");
                // Log error

                return BadRequest(ModelState);
            }

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType),
                _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(
                        section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    // This check assumes that there's a file
                    // present without form data. If form data
                    // is present, this method immediately fails
                    // and returns the model error.
                    if (!MultipartRequestHelper
                        .HasFileContentDisposition(contentDisposition))
                    {
                        ModelState.AddModelError("File",
                            $"The request couldn't be processed (Error 2).");
                        // Log error

                        return BadRequest(ModelState);
                    }
                    else
                    {
                        // Don't trust the file name sent by the client. To display
                        // the file name, HTML-encode the value.
                        var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                                contentDisposition.FileName.Value);
                        var trustedFileNameForFileStorage = Path.GetRandomFileName();

                        // **WARNING!**
                        // In the following example, the file is saved without
                        // scanning the file's contents. In most production
                        // scenarios, an anti-virus/anti-malware scanner API
                        // is used on the file before making the file available
                        // for download or for use by other systems. 
                        // For more information, see the topic that accompanies 
                        // this sample.

                        var streamedFileContent = await FileHelpers.ProcessStreamedFile(
                            section, contentDisposition, ModelState,
                            _permittedExtensions, _fileSizeLimit);

                        if (!ModelState.IsValid)
                        {
                            return BadRequest(ModelState);
                        }

                        using (var targetStream = System.IO.File.Create(
                            Path.Combine(_targetFilePath, trustedFileNameForFileStorage)))
                        {
                            await targetStream.WriteAsync(streamedFileContent);

                            _logger.LogInformation(
                                "Uploaded file '{TrustedFileNameForDisplay}' saved to " +
                                "'{TargetFilePath}' as {TrustedFileNameForFileStorage}",
                                trustedFileNameForDisplay, _targetFilePath,
                                trustedFileNameForFileStorage);
                        }
                    }
                }

                // Drain any remaining section body that hasn't been consumed and
                // read the headers for the next section.
                section = await reader.ReadNextSectionAsync();
            }

            return Created(nameof(UploaderController_Unused), null);
        }

        [HttpPost]
        //[RequestFormLimits(MultipartBodyLengthLimit = 737280000)]
        public async Task<IActionResult> Index(IList<IFormFile> files)
        {
            Startup.Progress = 0;
            Startup.UploadedFileName = string.Empty;
            string uniqueFileName = string.Empty;

            long totalBytes = files.Sum(f => f.Length);

            //UploadHub hub = new UploadHub();
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Preparando...");

            foreach (IFormFile source in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                uniqueFileName = GetUniqueFileName(filename);

                byte[] buffer = new byte[16 * 1024];

                using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(uniqueFileName)))
                {
                    using (Stream input = source.OpenReadStream())
                    {
                        long totalReadBytes = 0;
                        int readBytes;

                        while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            await _hubContext.Clients.All.SendAsync("ReceiveMessage", string.Format("Enviando {0} bytes", totalReadBytes.ToString()));
                            await output.WriteAsync(buffer, 0, readBytes);
                            totalReadBytes += readBytes;
                            //Startup.Progress = totalReadBytes; //(int)((float)totalReadBytes / (float)totalBytes * 100.0);


                            //await hub.Clients.All.SendAsync("ReceiveMessage", totalReadBytes.ToString());


                            //await Task.Delay(10); // It is only to make the process slower
                        }
                    }
                }
            }

            Startup.UploadedFileName = uniqueFileName;
            //await _hubContext.Clients.All.SendAsync("ReceiveMessage", $"");
            return this.Content("success");
        }
        public ActionResult GetUploadedFileName()
        {
            return this.Content(Startup.UploadedFileName);
        }
        //[HttpPost]
        public ActionResult Progress()
        {
            return this.Content(Startup.Progress.ToString());
        }

        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        private string GetPathAndFilename(string filename)
        {
            string path = this.hostingEnvironment.WebRootPath + "\\uploads\\";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }
    }
}
