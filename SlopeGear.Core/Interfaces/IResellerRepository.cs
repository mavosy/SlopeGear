using SlopeGear.Domain.Entities;

namespace SlopeGear.Domain.Interfaces;

public interface IResellerRepository
{
    Task<Reseller> GetByIdAsync(int id);
    Task<IEnumerable<Reseller>> GetByFilterAsync();
    Task AddAsync(Reseller reseller);
    void Update(Reseller resellerUpdate);
    Task DeleteAsync(int id);
}