using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interface
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
