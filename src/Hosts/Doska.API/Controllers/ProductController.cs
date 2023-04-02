using Doska.AppServices.Services.Product;
using Doska.Contracts.ProductDto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doska.API.Controllers
{
    public class ProductController : ControllerBase
    {
        IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        

        [HttpGet("/allProducts")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(int take, int skip)
        {
            var result = await _ProductService.GetAll(take, skip);

            return Ok(result);
        }

        [HttpPost("/createProduct")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoProductResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct(CreateOrEditProductRequest request)
        {
            var result = await _ProductService.CreateProductAsync(request);

            return Created("", result);
        }

        [HttpPut("/updateProduct/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoProductResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct(Guid id, CreateOrEditProductRequest request)
        {
            var result = await _ProductService.EditProductAsync(id, request);

            return Ok(result);
        }

        [HttpDelete("/deleteProduct")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await _ProductService.DeleteAsync(id);
            return Ok();
        }
    }
}
