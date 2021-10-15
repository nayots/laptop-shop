using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Models.Cart;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.BL.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly ICartRespository _cartRespository;

        public CartService(ICartRespository cartRespository)
        {
            _cartRespository = cartRespository;
        }

        public async Task<UserCart> AddItemToCartAsync(int userID, int productID)
        {
            await _cartRespository.InsertItemInCartAsync(userID, productID);
            return await GetUserCartAsync(userID);
        }

        public async Task<UserCart> GetUserCartAsync(int userID)
        {
            var cartItems = await _cartRespository.GetUserCartItemsAsync(userID);

            return new(cartItems);
        }

        public async Task RemoveItemFromCartAsync(int userID, int productID)
        {
            await _cartRespository.DeleteUserCartItemAsync(userID, productID);
        }

        public async Task<bool> UserHasProductTypeInCartAsync(int userID, int productID)
            => await _cartRespository.UserHasProductTypeInCartAsync(userID, productID);
    }
}