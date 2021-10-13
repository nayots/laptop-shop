using Nayots.LaptopShop.Common.Models.Products;
using System.Collections.Generic;

namespace Nayots.LaptopShop.Common.Contracts.Products
{
    public interface IProductsRepository
    {
        public ICollection<IPricedProduct> GetAllOfType(ProductType productType);
    }
}
