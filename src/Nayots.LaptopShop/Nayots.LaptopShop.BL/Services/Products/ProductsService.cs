using Nayots.LaptopShop.Common.Constants;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.BL.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ProductsResult> GetAllProductsOfTypeAsync(ProductType productType)
        {
            var products = await _productsRepository.GetAllOfTypeAsync(productType);
            return new(productType, products);
        }

        public async Task<ComponentsResult> GetAllComponentsAsync()
        {
            var components = await _productsRepository.GetAllOfTypesAsync(ShopConstants.ComponentsProductTypes);
            return new(components);
        }

        public async Task<bool> DoesProductExistAsync(int productID)
        {
            return await _productsRepository.DoesProductExistAsync(productID);
        }
    }
}