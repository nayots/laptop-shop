namespace Nayots.LaptopShop.Contracts.Auth
{
    public interface IJwtAuthManager
    {
        public string Authenticate(string username, string password);
    }
}