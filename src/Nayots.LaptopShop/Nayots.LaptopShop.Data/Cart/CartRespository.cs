using Dapper;
using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Common.Models.Cart;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Data.Cart
{
    public class CartRespository : ICartRespository
    {
        private readonly IConnectionProvider _connectionProvider;

        public CartRespository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task DeleteUserCartItemAsync(int userID, int productID)
        {
            await _connectionProvider.GetConnection()
                .ExecuteAsync("DELETE FROM CartItems WHERE UserID = @UserID AND ProductID = @ProductID"
                , new { UserID = userID, ProductID = productID });
        }

        public async Task<ICollection<CartItem>> GetUserCartItemsAsync(int userID)
        {
            return (await _connectionProvider.GetConnection()
                .QueryAsync<CartItem>(
                @"SELECT ci.ProductID, prd.Name AS ProductName, prd.Price, prd.ProductType
                FROM Products AS prd
                JOIN CartItems as ci ON ci.ProductID = prd.ID
                WHERE ci.UserID = @UserID", new { UserID = userID })).ToList();
        }

        public async Task InsertItemInCartAsync(int userID, int productID)
        {
            await _connectionProvider.GetConnection()
                .ExecuteAsync("INSERT INTO CartItems (UserID, ProductID) VALUES(@UserID, @ProductID);",
                new { UserID = userID, ProductID = productID });
        }

        public async Task<bool> UserHasProductTypeInCartAsync(int userID, int productID)
        {
            var wtf = await _connectionProvider.GetConnection()
            .ExecuteScalarAsync<bool>(
            @"SELECT COUNT(*) FROM Products AS prd
            JOIN CartItems AS ci ON ci.ProductID = prd.ID
            WHERE ci.UserID = @UserID AND prd.ProductType = (SELECT ProductType FROM Products WHERE ID = @ProductID)
            ", new { UserID = userID, ProductID = productID });
            return wtf;
        }
    }
}