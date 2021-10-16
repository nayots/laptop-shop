using Nayots.LaptopShop.Common.Contracts.Products;

namespace Nayots.LaptopShop.Common.Models.Products
{
    public record Product : IPricedProduct
    {
        public Product()
        {

        }
        protected Product(int iD, string name, double price, ProductType productType)
        {
            ID = iD;
            Name = name;
            Price = price;
            ProductType = productType;
        }

        public int ID { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public ProductType ProductType { get; init; }
    }
}
