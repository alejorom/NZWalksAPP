using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImagesUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Upload file to repository

        }

        private void ValidateFileUpload(ImagesUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "Invalid file type. Only .jpg, .jpeg, .png files are allowed.");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File size must be less than 10MB.");
            }
        }
    }
}
