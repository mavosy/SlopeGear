using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;

namespace SlopeGear.Application.Interfaces;

public interface IResellerService
{
    Task AddAsync(ResellerDto resellerDto);
    Task<Reseller> GetByIdAsync(int id);
    Task<IEnumerable<Reseller>> GetByFilterAsync();
    Task UpdateAsync(int id, ResellerUpdateDto resellerUpdate);
    Task DeleteAsync(int id);
}