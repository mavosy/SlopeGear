namespace SlopeGear.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}