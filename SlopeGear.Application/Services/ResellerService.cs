using SlopeGear.Application.Interfaces;
using SlopeGear.Contracts.Dtos;
using SlopeGear.Domain.Entities;
using SlopeGear.Domain.Interfaces;

namespace SlopeGear.Application.Services;

public class ResellerService : IResellerService
{
    private readonly IResellerRepository _resellerRepo;
    private readonly IUnitOfWork _unitOfWork;

    public ResellerService(IResellerRepository resellerRepo, IUnitOfWork unitOfWork)
    {
        _resellerRepo = resellerRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task AddAsync(ResellerDto resellerNew)
    {
        //await _resellerRepo.AddAsync(resellerNew);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<Reseller> GetByIdAsync(int id)
    {
        return await _resellerRepo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Reseller>> GetByFilterAsync()
    {
        return await _resellerRepo.GetByFilterAsync();
    }

    public async Task UpdateAsync(int id, ResellerUpdateDto resellerUpdate)
    {
        //await _resellerRepo.Update(id, resellerUpdate);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _resellerRepo.DeleteAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }
}