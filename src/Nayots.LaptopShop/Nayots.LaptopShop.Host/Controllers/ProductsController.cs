using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;
using System.Threading.Tasks;

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
        public async Task<ActionResult<ProductsResult>> GetForType([FromRoute] ProductType productType)
        {
            var products = await _productsService.GetAllProductsOfTypeAsync(productType);

            return Ok(products);
        }

        [AllowAnonymous]
        [HttpGet("all/components")]
        public async Task<ActionResult<ComponentsResult>> GetComponents()
        {
            var products = await _productsService.GetAllComponentsAsync();

            return Ok(products);
        }
    }
}
