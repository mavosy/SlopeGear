using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;

namespace SlopeGear.Application.Interfaces;

public interface IProductService
{
    Task AddAsync(ProductDto productDto);
    Task<ProductDto> GetByIdAsync(int id);
    Task<IEnumerable<ProductDto>> GetByFilterAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null);
    Task UpdateAsync(int id, ProductUpdateDto productUpdate);
    Task DeleteAsync(int id);
}