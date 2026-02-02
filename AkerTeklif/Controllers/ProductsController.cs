using AkerTeklif.Features.Products;
using AkerTeklif.Features.Products.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AkerTeklif.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO productDTO)
        {
            await productService.CreateProductHandler(productDTO);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await productService.GetProductHandler();
            return Ok(products);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await productService.DeleteProductHandler(id);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            await productService.UpdateProductHandler(updateProductDTO);
            return Ok();
        }

    }
}
