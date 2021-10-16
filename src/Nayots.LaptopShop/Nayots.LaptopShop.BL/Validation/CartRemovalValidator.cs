using FluentValidation;
using FluentValidation.Results;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Contracts.Users;
using Nayots.LaptopShop.Common.Models.Cart;

namespace Nayots.LaptopShop.BL.Validation
{
    public class CartRemovalValidator : AbstractValidator<CartRemoval>
    {
        private readonly IUsersService _usersService;
        private readonly IProductsService _productsService;

        public CartRemovalValidator(IUsersService usersService, IProductsService productsService)
        {
            _usersService = usersService;
            _productsService = productsService;

            RuleFor(x => x).NotNull().WithMessage("Cart item canot be empty");
            RuleFor(x => x.ProductID)
                .GreaterThan(0)
                .MustAsync(async (productId, _) => await _productsService.DoesProductExistAsync(productId))
                .WithMessage("Invalid ProductID");
        }

        public override ValidationResult Validate(ValidationContext<CartRemoval> context)
        {
            var user = _usersService.GetCurrentUser();

            if (user != default)
                return base.Validate(context);

            return new ValidationResult(new[] { new ValidationFailure("User", "Invalid User") });
        }
    }
}