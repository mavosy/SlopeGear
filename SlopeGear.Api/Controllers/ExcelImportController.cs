using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SlopeGear.Application.Interfaces;

namespace SlopeGear.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExcelImportController : ControllerBase
    {
        private readonly IProductImportService _productImportService;

        public ExcelImportController(IProductImportService productImportService)
        {
            _productImportService = productImportService;
        }

        [HttpPost("products")]
        public async Task<IActionResult> ImportProducts([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

            await using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            try
            {
                await _productImportService.ImportProductsAsync(filePath);
                return Ok("Products imported successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while importing products: {ex.Message}");
            }
            finally
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
