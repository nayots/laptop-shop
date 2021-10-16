using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Nayots.LaptopShop.Common.Models.Products
{
    public record ProductsResult([property: JsonConverter(typeof(JsonStringEnumConverter))] ProductType ProductType, ICollection<Product> Products)
    {
        public int Count => Products?.Count ?? 0;
    }
}
