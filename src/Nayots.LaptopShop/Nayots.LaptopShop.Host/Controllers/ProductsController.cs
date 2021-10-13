using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;

namespace Nayots.LaptopShop.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductsController: ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [AllowAnonymous]
        [HttpGet("all/{productType:int}")]
        public ActionResult<ProductsResult> GetForType([FromRoute] ProductType productType)
        {
            var products = _productsService.GetAllProductsOfType(productType);

            return Ok(products);
        }
    }
}
