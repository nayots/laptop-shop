using Dapper;
using Nayots.LaptopShop.Common.Contracts.Data;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Data.Products
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IConnectionProvider _connectionProvider;

        public ProductsRepository(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task<bool> DoesProductExistAsync(int productID)
            => await _connectionProvider.GetConnection()
            .ExecuteScalarAsync<bool>("SELECT COUNT(1) FROM Products WHERE ID = @ProductID", new { ProductID = productID });

        public async Task<ICollection<Product>> GetAllOfTypeAsync(ProductType productType)
        {
            var parameters = new { ProductType = productType };

            return (await _connectionProvider.GetConnection()
                    .QueryAsync<Product>("SELECT ID, Name, Price, ProductType FROM Products WHERE ProductType = @ProductType;", parameters)).ToList();
        }

        public async Task<IDictionary<ProductType, ICollection<Product>>> GetAllOfTypesAsync(params ProductType[] productTypes)
        {
            var parameters = new { ProductTypes = productTypes };

            return (await _connectionProvider.GetConnection().QueryAsync<Product>("SELECT ID, Name, Price, ProductType FROM Products WHERE ProductType IN @ProductTypes;", parameters))
                .Aggregate(new Dictionary<ProductType, ICollection<Product>>(), (acc, curr) =>
            {
                if (!acc.ContainsKey(curr.ProductType))
                    acc[curr.ProductType] = new List<Product>();

                acc[curr.ProductType].Add(curr);
                return acc;
            });
        }
    }
}