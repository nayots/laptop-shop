using Nayots.LaptopShop.Common.Models.Products;
using System.Collections.Generic;

namespace Nayots.LaptopShop.Common.Contracts.Products
{
    public interface IProductsService
    {
        public ProductsResult GetAllProductsOfType(ProductType productType);
    }
}