namespace Nayots.LaptopShop.Common.Models.Cart
{
    public record CartAddition(int ProductID) : CartUpdate(ProductID);
}