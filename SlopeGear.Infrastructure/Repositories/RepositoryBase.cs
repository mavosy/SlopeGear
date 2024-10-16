using Microsoft.EntityFrameworkCore;
using SlopeGear.Infrastructure.Data;

namespace SlopeGear.Infrastructure.Repositories;

public abstract class RepositoryBase<T> where T : class
{
    protected readonly ApplicationDbContext _dbContext;

    protected RepositoryBase(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetByFilterAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
    }

    public virtual void Update(T entityUpdate) => _dbContext.Set<T>().Update(entityUpdate);

    public virtual async Task DeleteAsync(int id)
    {
        T? entity = await _dbContext.Set<T>().FindAsync(id);

        if (entity is not null)
        {
            _dbContext.Set<T>().Remove(entity);
        }
    }
}