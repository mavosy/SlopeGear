using SlopeGear.Application.Interfaces;
using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;

namespace SlopeGear.Application.Services;

public class ProductImportService : IProductImportService
{
    private readonly IProductRepository _productRepository;
    private readonly IExcelDataReader _excelDataReader;

    public ProductImportService(IProductRepository productRepository, IExcelDataReader excelDataReader)
    {
        _productRepository = productRepository;
        _excelDataReader = excelDataReader;
    }

    public async Task ImportProductsAsync(string filePath)
    {
        var productDtos = await _excelDataReader.ReadProductsFromExcelAsync(filePath);

        var products = new List<Product>();

        foreach (var dto in productDtos)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CurrentStockQuantity = dto.CurrentStockQuantity,
                CategoryId = dto.CategoryId
            };

            ValidateProduct(product);

            //if (product.CategoryId.HasValue)
            //{
            //    var categoryExists = await _categoryRepository.ExistsAsync(product.CategoryId.Value);
            //    if (!categoryExists)
            //    {
            //        throw new Exception($"Category with ID {product.CategoryId.Value} does not exist.");
            //    }
            //}

            products.Add(product);
        }

        foreach (var product in products)
        {
            await _productRepository.AddAsync(product);
        }
    }

    private static void ValidateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("Product name is required.", nameof(product));

        if (product.Price <= 0)
            throw new ArgumentOutOfRangeException(nameof(product), "Product price must be greater than zero.");

        if (product.CurrentStockQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(product), "Stock quantity cannot be negative.");
    }
}