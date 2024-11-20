using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJWTToken (IdentityUser user, List<string> roles );
    }
}
