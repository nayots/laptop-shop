using Nayots.LaptopShop.Common.Models.Cart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Common.Contracts.Cart
{
    public interface ICartRespository
    {
        public Task<bool> UserHasProductTypeInCartAsync(int userID, int productID);

        public Task InsertItemInCartAsync(int userID, int productID);

        public Task<ICollection<CartItem>> GetUserCartItemsAsync(int userID);
        public Task DeleteUserCartItemAsync(int userID, int productID);
    }
}