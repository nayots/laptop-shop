using Nayots.LaptopShop.Common.Contracts.Products;
using System.Collections.Generic;

namespace Nayots.LaptopShop.Common.Models.Products
{
    public record ProductsResult(ProductType ProductType, ICollection<IPricedProduct> Products)
    {
        public int Count => Products?.Count ?? 0;
    }
}
