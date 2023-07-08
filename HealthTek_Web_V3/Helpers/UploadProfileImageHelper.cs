using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace HealthTek_Web_V3.Helpers
{
    public class UploadProfileImageHelper
    {
        private readonly IWebHostEnvironment _hostEnv;

        private readonly string _uploadFileBasePath;

        public UploadProfileImageHelper(IWebHostEnvironment hostEnv)
        {
            this._hostEnv = hostEnv;
            this._uploadFileBasePath = Path.Combine("profileImgs");
        }

        public async Task<string> UploadFileAsync(IFormFile formFile, bool renameFile = true, string? newFileName = null)
        {
            if (formFile == null)
            {
                return string.Empty;
            }

            var fileName = formFile.FileName;
            if (renameFile)
            {
                var fileExtention = Path.GetExtension(formFile.FileName);
                fileName = newFileName + fileExtention;
            }

            var partialPath = Path.Combine(this._uploadFileBasePath, fileName);
            var path = Path.Combine(this._hostEnv.WebRootPath, partialPath);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
                await stream.FlushAsync();
                await stream.DisposeAsync();
            }

            return partialPath;
        }
    }
}