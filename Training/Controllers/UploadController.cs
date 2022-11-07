using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Training.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [HttpPost]
        [Route("upload-file")]
        public async Task<string> UploadAsync(IFormFile file)
        {
            if (file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", file.FileName);

                var stream = System.IO.File.Create(path);

                await file.CopyToAsync(stream);
            }

            return file.FileName;
        }

        [HttpPost]
        [Route("upload-base64-file")]
        public async Task<string> UploadBase64Async(string fileContent)
        {
            if (fileContent.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString();

                var bytes = Convert.FromBase64String(fileContent.Replace("base64,", string.Empty));

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Files", fileName);

                await System.IO.File.WriteAllBytesAsync(path, bytes);

                return fileName;
            }
            return "";
        }
    }
}
