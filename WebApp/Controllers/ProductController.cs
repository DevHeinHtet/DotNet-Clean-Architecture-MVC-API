using Application.Products.Commands.Create;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        private readonly ISender _sender;

        public ProductController(ProductService productService, ISender sender)
        {
            _productService = productService;
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CreateProductCommand productCommand)
        {
            var productId = await _sender.Send(productCommand);
            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] Product product)
        {
            product.ID = id;
            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
