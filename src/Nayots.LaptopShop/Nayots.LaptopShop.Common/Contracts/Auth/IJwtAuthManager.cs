namespace Nayots.LaptopShop.Common.Contracts.Auth
{
    public interface IJwtAuthManager
    {
        public string Authenticate(string username, string password);
    }
}