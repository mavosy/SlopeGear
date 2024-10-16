using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;
using SlopeGear.Infrastructure.Data;

namespace SlopeGear.Infrastructure.Repositories;

public class ResellerRepository : RepositoryBase<Reseller>, IResellerRepository
{
    public ResellerRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }
}