using Microsoft.EntityFrameworkCore;
using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;
using SlopeGear.Infrastructure.Data;

namespace SlopeGear.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetByFilterAsync(string? name = null, string? category = null, decimal? minPrice = null, decimal? maxPrice = null)
    {
        var query = _dbContext.Set<Product>().AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(p => p.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Include(p => p.Category)
                         .Where(p => p.Category != null && p.Category.Name.Contains(category));
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        return await query.ToListAsync();
    }
}