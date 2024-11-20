using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interface;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        // POST: api/Auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            // Register user
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded && registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
            {
                identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                if (identityResult.Succeeded) return Ok("User was register! Please login.");
            }
            return BadRequest("User was not registered!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user == null) return BadRequest("Invalid user name or password!");

            var passwordValid = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (!passwordValid) return BadRequest("Invalid user name or password!");

            var roles = await userManager.GetRolesAsync(user);
            if (roles == null || !roles.Any()) return BadRequest("User has no roles!");

            var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
            var loginResponseDto = new LoginResponseDto { JwtToken = jwtToken };
            return Ok(loginResponseDto);
        }
    }
}
