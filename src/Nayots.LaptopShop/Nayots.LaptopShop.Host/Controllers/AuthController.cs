using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nayots.LaptopShop.Common.Contracts.Auth;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Auth;
using System.Security.Claims;

namespace Nayots.LaptopShop.Host.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IUsersService _usersService;

        public AuthController(IJwtAuthManager jwtAuthManager, IUsersService usersService)
        {
            _jwtAuthManager = jwtAuthManager;
            _usersService = usersService;
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
        public IActionResult GetUser()
        {
            return Ok(_usersService.GetCurrentUser());
        }
    }
}