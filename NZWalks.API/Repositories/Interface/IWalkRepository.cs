using NZWalks.API.Models.Domain;
using System.Runtime.InteropServices;

namespace NZWalks.API.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync();
        Task<Walk?> GetByIdAsync(Guid id);
    }
}
