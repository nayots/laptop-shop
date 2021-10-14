using FluentValidation;
using FluentValidation.Results;
using Nayots.LaptopShop.Common.Contracts.Cart;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Cart;

namespace Nayots.LaptopShop.BL.Validation
{
    public class CartAdditionValidator : AbstractValidator<CartAddition>
    {
        private readonly IUsersService _usersService;
        private readonly IProductsService _productsService;
        private readonly ICartService _cartService;

        public CartAdditionValidator(IUsersService usersService, IProductsService productsService, ICartService cartService)
        {
            _usersService = usersService;
            _productsService = productsService;
            _cartService = cartService;

            RuleFor(x => x).NotNull().WithMessage("Cart item canot be empty");
            RuleFor(x => x.ProductID)
                .GreaterThan(0)
                .MustAsync(async (productId, _) => await _productsService.DoesProductExistAsync(productId))
                .WithMessage("Invalid ProductID")
                .MustAsync(async (productId, _) => !await _cartService.UserHasProductTypeInCartAsync(_usersService.GetCurrentUser().ID, productId))
                .WithMessage("Product of the same type already in cart");
        }

        public override ValidationResult Validate(ValidationContext<CartAddition> context)
        {
            var user = _usersService.GetCurrentUser();

            if (user != default)
                return base.Validate(context);

            return new ValidationResult(new[] { new ValidationFailure("User", "Invalid User") });
        }
    }
}