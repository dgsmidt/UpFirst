using Microsoft.AspNetCore.Http;

namespace Web.ViewModels
{
    public class UploadFile
    {
        public IFormFile FormFile { get; set; }
        public int? Id { get; set; }
    }
}
