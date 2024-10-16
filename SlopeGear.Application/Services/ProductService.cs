using SlopeGear.Application.Interfaces;
using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;

namespace SlopeGear.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IProductRepository productRepo, IUnitOfWork unitOfWork)
    {
        _productRepo = productRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CurrentStockQuantity = productDto.CurrentStockQuantity,
            CategoryId = productDto.CategoryId
        };

        //if (product.CategoryId.HasValue)
        //{
        //    var categoryExists = await _categoryRepo.ExistsAsync(product.CategoryId.Value);
        //    if (!categoryExists)
        //    {
        //        throw new ArgumentException("Invalid CategoryId.");
        //    }
        //}

        await _productRepo.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            return null;

        // Manually map Product to ProductDto
        var productDto = MapToProductDto(product);
        return productDto;
    }

    public async Task<IEnumerable<ProductDto>> GetByFilterAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        IEnumerable<Product> products = await _productRepo.GetByFilterAsync(name, category, minPrice, maxPrice);
        var productDtos = new List<ProductDto>();

        foreach (var product in products)
        {
            productDtos.Add(MapToProductDto(product));
        }
        return productDtos;
    }

    public async Task UpdateAsync(int id, ProductUpdateDto productUpdateDto)
    {
        var existingProduct = await _productRepo.GetByIdAsync(id);
        if (existingProduct == null)
            return;

        if (productUpdateDto.Name is not null)
            existingProduct.Name = productUpdateDto.Name;

        if (productUpdateDto.Description is not null)
            existingProduct.Description = productUpdateDto.Description;

        if (productUpdateDto.Price.HasValue)
            existingProduct.Price = productUpdateDto.Price.Value;

        if (productUpdateDto.CurrentStockQuantity.HasValue)
            existingProduct.CurrentStockQuantity = productUpdateDto.CurrentStockQuantity.Value;

        //if (productUpdateDto.CategoryId.HasValue)
        //{
        //    var categoryExists = await _categoryRepo.ExistsAsync(productUpdateDto.CategoryId.Value);
        //    if (!categoryExists)
        //    {
        //        throw new ArgumentException("Invalid CategoryId.");
        //    }
        //    existingProduct.CategoryId = productUpdateDto.CategoryId.Value;
        //}

        _productRepo.Update(existingProduct);
        return;
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepo.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    private ProductDto MapToProductDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CurrentStockQuantity = product.CurrentStockQuantity,
            CategoryId = product.CategoryId
        };
    }
}