using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;
using SlopeGear.Infrastructure.Data;

namespace SlopeGear.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}