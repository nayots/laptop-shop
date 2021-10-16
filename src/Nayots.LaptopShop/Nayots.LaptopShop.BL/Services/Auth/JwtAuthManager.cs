using Microsoft.IdentityModel.Tokens;
using Nayots.LaptopShop.Common.Contracts.Auth;
using Nayots.LaptopShop.Common.Contracts.Users;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Nayots.LaptopShop.BL.Services.Auth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly string _key;
        private readonly IUsersService _usersService;

        public JwtAuthManager(string key, IUsersService usersService)
        {
            _key = key;
            _usersService = usersService;
        }

        public string Authenticate(string username, string password)
        {
            if (!_usersService.TryGetUser(username, password, out var userInfo))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userInfo.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(
                        new SymmetricSecurityKey(tokenKey),
                        SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}