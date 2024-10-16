using SlopeGear.Application.Interfaces;
using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;

namespace SlopeGear.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IOrderRepository orderRepo, IUnitOfWork unitOfWork)
    {
        _orderRepo = orderRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(OrderDto orderNew)
    {
        //await _orderRepo.AddAsync(orderNew);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        return await _orderRepo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Order>> GetByFilterAsync()
    {
        return await _orderRepo.GetByFilterAsync();
    }

    public async Task UpdateAsync(int id, OrderUpdateDto orderUpdate)
    {
        //await _orderRepo.Update(id, orderUpdate);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _orderRepo.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}