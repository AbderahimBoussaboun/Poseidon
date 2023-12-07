using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Poseidon.Api.Models.ResourceMaps.Requests.Products;
using Poseidon.ApplicationServices.ResourceMaps.Interfaces.Products;
using Poseidon.Entities.ResourceMaps.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poseidon.Api.Controllers.Products
{
    [ApiController]
    [Route("api/ResourceMaps/[controller]")]
    public class ProductsController : ControllerBase
    {
    
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductsService _productsService;


        public ProductsController(ILogger<ProductsController> logger, IProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }

        #region GET
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Product>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllProducts()
        {
            var result= await _productsService.GetProducts();
            return Ok(result);
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var result= await _productsService.GetProductById(productId);
            return Ok(result);
        }


        #endregion

        #region POST
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> InsertProduct([FromBody] ProductRequest product)
        {
            var result= await _productsService.InsertProduct(product);
            return Ok(result);
        }

        #endregion

        #region PUT
        [HttpPut("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProduct(Guid productId,[FromBody] ProductRequest product)
        {
            var result= await _productsService.UpdateProduct(productId, product);
            return Ok(result);
        }
        #endregion

        #region DELETE
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            var result= await _productsService.DeleteProduct(productId);
            return Ok(result);
        }
        #endregion

        #region OTHER
        #endregion

    }
}
