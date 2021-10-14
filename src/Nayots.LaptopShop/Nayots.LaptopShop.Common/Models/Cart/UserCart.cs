using System.Collections.Generic;
using System.Linq;

namespace Nayots.LaptopShop.Common.Models.Cart
{
    public record UserCart(ICollection<CartItem> CartItems)
    {
        public double TotalPrice => CartItems?.Aggregate(0d, (sum, curr) => sum += curr.Price) ?? 0d;
    }
}