namespace Nayots.LaptopShop.Common.Models.Cart
{
    public record CartRemoval(int ProductID) : CartUpdate(ProductID);
}