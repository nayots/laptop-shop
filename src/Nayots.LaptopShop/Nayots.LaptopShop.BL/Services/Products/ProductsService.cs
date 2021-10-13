using Nayots.LaptopShop.Common.Contracts.Products;
using Nayots.LaptopShop.Common.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nayots.LaptopShop.BL.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public ProductsResult GetAllProductsOfType(ProductType productType)
            => new(productType, _productsRepository.GetAllOfType(productType));
    }
}
