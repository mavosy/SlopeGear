using Microsoft.AspNetCore.Mvc;
using SlopeGear.Contracts.Dtos;
using SlopeGear.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace SlopeGear.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Gets a product by it's ID", Description = "Requires admin privileges")]
    public async Task<IActionResult> GetProductById(int id)
    {
        _logger.LogInformation("Fetching product with ID: {ProductId}", id);
        var product = await _productService.GetByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning("Product with ID {ProductId} was not found", id);
            return NotFound();
        }

        _logger.LogInformation("Successfully retrieved product with ID: {ProductId}", id);
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts([FromQuery] string? name = null, [FromQuery] string? category = null, [FromQuery] decimal? minPrice = null, [FromQuery] decimal? maxPrice = null)
    {
        _logger.LogInformation("Fetching products with optional filters - Name: {Name}, Category: {Category}, MinPrice: {MinPrice}, MaxPrice: {MaxPrice}", name, category, minPrice, maxPrice);
        var products = await _productService.GetByFilterAsync(name, category, minPrice, maxPrice);
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid) { return BadRequest(ModelState); }

        _logger.LogInformation("Creating a new product: {@ProductDto}", productDto);
        await _productService.AddAsync(productDto);
        try
        {
            await _productService.AddAsync(productDto);
        return Ok("Product created successfully");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        //var createdProductResult = await _productService.AddAsync(productDto);
        //return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
    {
        _logger.LogInformation("Updating product with ID: {ProductId}", id);

        var updatedProduct = _productService.UpdateAsync(id, productDto);
        if (updatedProduct is null)
        {
            _logger.LogWarning("Product with ID {ProductId} was not found", id);
            return NotFound();
        }

        return Ok(updatedProduct);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteProductAsync(int id)
    {
        _logger.LogInformation("Deleting product with ID: {ProductId}", id);

        await _productService.DeleteAsync(id);

        return NoContent();
    }
}
