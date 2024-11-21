using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Repositories.Implementation
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ApplicationDbContext applicationDbContext;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor, 
            ApplicationDbContext applicationDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.applicationDbContext = applicationDbContext;
        }

        /// <summary>
        /// Uploads an image to the server and saves its details in the database.
        /// </summary>
        /// <param name="image">The image to be uploaded.</param>
        /// <returns>The uploaded image with updated file path.</returns>
        public async Task<Image> Upload(Image image)
        {
            var folderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            using var fileStream = new FileStream(folderPath, FileMode.Create);
            await image.File.CopyToAsync(fileStream);

            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://" +
                $"{httpContextAccessor.HttpContext.Request.Host}" +
                $"{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;
            await applicationDbContext.Images.AddAsync(image);
            await applicationDbContext.SaveChangesAsync();

            return image;
        }
    }
}
