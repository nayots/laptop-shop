﻿using Nayots.LaptopShop.Common.Models.Cart;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Common.Contracts.Cart
{
    public interface ICartService
    {
        public Task<bool> UserHasProductTypeInCartAsync(int userID, int productID);

        public Task<UserCart> AddItemToCartAsync(int userID, int productID);
        public Task<UserCart> RemoveItemFromCartAsync(int userID, int productID);

        public Task<UserCart> GetUserCartAsync(int userID);
    }
}