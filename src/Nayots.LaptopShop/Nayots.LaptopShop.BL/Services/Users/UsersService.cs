using Microsoft.AspNetCore.Http;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Nayots.LaptopShop.BL.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IDictionary<int, User> _users = new Dictionary<int, User> { { 1, new(1, "admin", "admin") } };

        public UserInfo GetCurrentUser() => GetCurrentUserInternal();

        public bool TryGetUser(string username, out UserInfo userInfo)
        {
            var user = _users.Values.FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));

            if (user != default)
            {
                userInfo = new(user.ID, user.Username);
                return true;
            }

            userInfo = null;
            return false;
        }

        public bool TryGetUser(int userID, out UserInfo userInfo)
        {
            if (_users.TryGetValue(userID, out var user))
            {
                userInfo = new(user.ID, user.Username);
                return true;
            }

            userInfo = null;
            return false;
        }

        public bool TryGetUser(string username, string password, out UserInfo userInfo)
        {
            var user = _users.Values.FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase) && x.Password.Equals(password));

            if (user != default)
            {
                userInfo = new(user.ID, user.Username);
                return true;
            }

            userInfo = null;
            return false;
        }

        private UserInfo GetCurrentUserInternal()
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var username = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            if (!string.IsNullOrWhiteSpace(username) && TryGetUser(username, out var userInfo))
                return userInfo;

            return default;
        }
    }
}