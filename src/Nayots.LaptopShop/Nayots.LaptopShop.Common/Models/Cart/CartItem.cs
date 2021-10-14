using Nayots.LaptopShop.Common.Models.Products;

namespace Nayots.LaptopShop.Common.Models.Cart
{
    public record CartItem
    {
        public CartItem()
        {

        }

        public CartItem(int productID, string productName, double price, ProductType productType)
        {
            ProductID = productID;
            ProductName = productName;
            Price = price;
            ProductType = productType;
        }

        public int ProductID { get; init; }
        public string ProductName { get; init; }
        public double Price { get; init; }
        public ProductType ProductType { get; init; }
    }
}