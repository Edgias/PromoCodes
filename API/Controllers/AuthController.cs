using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TheRoom.PromoCodes.API.Models.Requests;
using TheRoom.PromoCodes.ApplicationCore.Interfaces;
using TheRoom.PromoCodes.Infrastructure.Identity;

namespace TheRoom.PromoCodes.API.Controllers
{
    [ApiController]
    [Route("v1.0/auth")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticateRequest request)
        {
            string response = string.Empty;

            // Sign user using the ASP.NET Identity platform.
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, true);

            if (result.Succeeded)
            {
                response = await _tokenClaimsService.GetTokenAsync(request.Username);
            }

            return Ok(response);
        }
    }
}
