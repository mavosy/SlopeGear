using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;

namespace SlopeGear.Application.Interfaces;

public interface IOrderService
{
    Task AddAsync(OrderDto orderDto);
    Task<Order> GetByIdAsync(int id);
    Task<IEnumerable<Order>> GetByFilterAsync();
    Task UpdateAsync(int id, OrderUpdateDto orderUpdate);
    Task DeleteAsync(int id);
}