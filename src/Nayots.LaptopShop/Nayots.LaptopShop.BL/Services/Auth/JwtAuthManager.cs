using Microsoft.IdentityModel.Tokens;
using Nayots.LaptopShop.Common.Contracts.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Nayots.LaptopShop.BL.Services.Auth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IDictionary<string, string> _users = new Dictionary<string, string> { { "admin", "admin" } };
        private readonly string _key;

        public JwtAuthManager(string key)
        {
            _key = key;
        }

        public string Authenticate(string username, string password)
        {
            if (!_users.Any(x => x.Key.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Value.Equals(password)))
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
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