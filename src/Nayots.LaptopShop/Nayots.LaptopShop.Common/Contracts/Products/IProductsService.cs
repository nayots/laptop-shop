using Nayots.LaptopShop.Common.Models.Products;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Common.Contracts.Products
{
    public interface IProductsService
    {
        public Task<ProductsResult> GetAllProductsOfTypeAsync(ProductType productType);

        public Task<ComponentsResult> GetAllComponentsAsync();

        public Task<bool> DoesProductExistAsync(int productID);
    }
}