using Nayots.LaptopShop.Common.Models.Users;

namespace Nayots.LaptopShop.Common.Contracts.Users
{
    public interface IUsersService
    {
        public bool TryGetUser(string username, out UserInfo userInfo);

        public bool TryGetUser(int userID, out UserInfo userInfo);

        public bool TryGetUser(string username, string password, out UserInfo userInfo);

        public UserInfo GetCurrentUser();
    }
}