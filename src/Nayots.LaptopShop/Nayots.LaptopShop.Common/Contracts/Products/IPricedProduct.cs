using Nayots.LaptopShop.Common.Models.Products;

namespace Nayots.LaptopShop.Common.Contracts.Products
{
    public interface IPricedProduct
    {
        public int ID { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }

        public ProductType ProductType { get; init; }
    }
}
