using SlopeGear.Domain.Entities;

namespace SlopeGear.Domain.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByFilterAsync();
    Task AddAsync(Order order);
    void Update(Order orderUpdate);
    Task DeleteAsync(int id);
}