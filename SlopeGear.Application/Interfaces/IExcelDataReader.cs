using SlopeGear.Contracts.Dtos;

namespace SlopeGear.Application.Interfaces;

public interface IExcelDataReader
{
    Task<IEnumerable<ProductDto>> ReadProductsFromExcelAsync(string filePath);
}