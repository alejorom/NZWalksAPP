using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImagesUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Convert DTO to Domain Model
            var imageDomain = new Image
            {
                File = request.File,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                FileDescripcion = request.FileDescription
            };

            // Upload file to repository
            var uploadedImage = await imageRepository.Upload(imageDomain);
            return Ok(uploadedImage);

        }

        private void ValidateFileUpload(ImagesUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName.ToLower())))
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
