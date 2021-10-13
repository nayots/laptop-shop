using Nayots.LaptopShop.Common.Contracts.Products;

namespace Nayots.LaptopShop.Common.Models.Products
{
    public record Product : IPricedProduct
    {
        protected Product(int iD, string name, decimal price, ProductType productType)
        {
            ID = iD;
            Name = name;
            Price = price;
            ProductType = productType;
        }

        public int ID { get; init; }
        public string Name { get; init; }
        public decimal Price { get; init; }
        public ProductType ProductType { get; init; }
    }
}
