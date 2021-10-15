using FluentAssertions;
using Moq;
using Nayots.LaptopShop.BL.Services.Cart;
using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Models.Cart;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Nayots.LaptopShop.BL.Tests.Services
{
    public class CartServiceTests
    {
        private readonly List<CartItem> _cartItems;
        private readonly ICartRespository _cartRespository;

        public CartServiceTests()
        {
            _cartItems = new List<CartItem>();
            var cartRepo = new Mock<ICartRespository>();
            cartRepo.Setup(x => x.GetUserCartItemsAsync(It.IsAny<int>()))
                .Returns(Task.FromResult<ICollection<CartItem>>(_cartItems));
            cartRepo
                .Setup(x => x.InsertItemInCartAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((userId, productId) => _cartItems.Add(new CartItem() { ProductID = productId }));
            cartRepo
                .Setup(x => x.DeleteUserCartItemAsync(It.IsAny<int>(), It.IsAny<int>()))
                .Callback<int, int>((_, productId) => _cartItems.Remove(_cartItems.Single(x => x.ProductID.Equals(productId))));

            _cartRespository = cartRepo.Object;
        }

        [Theory]
        [Trait("Service", nameof(CartService))]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task Add_Cart_Item_Should_Update_Items(int productID)
        {
            var sut = new CartService(_cartRespository);

            var userCart = await sut.AddItemToCartAsync(1, productID);

            userCart.Should().NotBeNull();
            userCart.CartItems.Should().Contain(x => x.ProductID.Equals(productID));
        }

        [Theory]
        [Trait("Service", nameof(CartService))]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        public async Task GetCart_Items_Should_Return_Items(int productID)
        {
            _cartItems.Add(new CartItem() { ProductID = productID });
            var sut = new CartService(_cartRespository);

            var userCart = await sut.GetUserCartAsync(1);

            userCart.Should().NotBeNull();
            userCart.CartItems.Should().Contain(x => x.ProductID.Equals(productID));
        }

        [Theory]
        [Trait("Service", nameof(CartService))]
        [InlineData(1, new int[] { 1, 2, 3, 4 })]
        [InlineData(2, new int[] { 1, 2, 3, 4 })]
        [InlineData(3, new int[] { 1, 2, 3, 4 })]
        [InlineData(4, new int[] { 1, 2, 3, 4 })]
        [InlineData(5, new int[] { 1, 2, 3, 4, 5 })]
        [InlineData(6, new int[] { 1, 2, 3, 4, 5, 6 })]
        public async Task Remove_Item_Should_Remove_Items(int productToRemove, int[] products)
        {
            foreach (var userProductId in products)
            {
                _cartItems.Add(new CartItem() { ProductID = userProductId });

            }

            var sut = new CartService(_cartRespository);

            _cartItems.Should().Contain(x => x.ProductID == productToRemove);

            await sut.RemoveItemFromCartAsync(1, productToRemove);

            _cartItems.Should().NotContain(x => x.ProductID == productToRemove);
        }
    }
}