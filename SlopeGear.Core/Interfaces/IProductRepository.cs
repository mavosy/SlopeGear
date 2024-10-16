using SlopeGear.Domain.Entities;

namespace SlopeGear.Domain.Interfaces;

public interface IProductRepository
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByFilterAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null);
    Task AddAsync(Product product);
    void Update(Product productUpdate);
    Task DeleteAsync(int id);
}