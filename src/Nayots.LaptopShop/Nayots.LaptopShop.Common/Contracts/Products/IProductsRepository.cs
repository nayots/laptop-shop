using Nayots.LaptopShop.Common.Models.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Common.Contracts.Products
{
    public interface IProductsRepository
    {
        public Task<ICollection<Product>> GetAllOfTypeAsync(ProductType productType);

        public Task<IDictionary<ProductType,ICollection<Product>>> GetAllOfTypesAsync(params ProductType[] productTypes);
        public Task<bool> DoesProductExistAsync(int productID);
    }
}
