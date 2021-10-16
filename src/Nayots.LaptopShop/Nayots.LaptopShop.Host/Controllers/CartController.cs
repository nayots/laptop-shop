using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Cart;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ICartService _cartService;

        public CartController(IUsersService usersService, ICartService cartService)
        {
            _usersService = usersService;
            _cartService = cartService;
        }

        [HttpGet()]
        public async Task<ActionResult<UserCart>> Get()
        {
            var user = _usersService.GetCurrentUser();
            var cart = await _cartService.GetUserCartAsync(user.ID);
            return Ok(cart);
        }

        [HttpPost()]
        public async Task<ActionResult<UserCart>> AddItemToCart(CartAddition cartAddition)
        {
            var user = _usersService.GetCurrentUser();
            await _cartService.AddItemToCartAsync(user.ID, cartAddition.ProductID);

            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteFromCart(CartRemoval cartRemoval)
        {
            var user = _usersService.GetCurrentUser();

            await _cartService.RemoveItemFromCartAsync(user.ID, cartRemoval.ProductID);

            return Accepted();
        }
    }
}