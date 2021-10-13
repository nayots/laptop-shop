using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;
using System.Collections.Generic;

namespace Nayots.LaptopShop.Data.Products
{
    public class ProductsRepository : IProductsRepository
    {
        public ICollection<IPricedProduct> GetAllOfType(ProductType productType)
        {
            return new List<IPricedProduct>(0);
        }
    }
}
