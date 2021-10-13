using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nayots.LaptopShop.Common.Contracts.Auth;
using Nayots.LaptopShop.Common.Models.Auth;

namespace Nayots.LaptopShop.Host.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthManager _jwtAuthManager;

        public AuthController(IJwtAuthManager jwtAuthManager)
        {
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpPost()]
        [AllowAnonymous]
        public IActionResult Authenticate(UserCreds userCredentials)
        {
            var token = _jwtAuthManager.Authenticate(userCredentials.UserName, userCredentials.Password);

            if (string.IsNullOrWhiteSpace(token))
                return Unauthorized();

            return Ok(token);
        }

        [HttpGet]
        public IActionResult Test() => Ok(new string[] { "something", "more" });
    }
}