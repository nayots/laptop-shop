using System.Collections.Generic;

namespace Nayots.LaptopShop.Common.Models.Products
{
    public record ComponentsResult(IDictionary<ProductType, ICollection<Product>> Components);
}