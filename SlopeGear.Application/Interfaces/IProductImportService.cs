namespace SlopeGear.Application.Interfaces;

public interface IProductImportService
{
    Task ImportProductsAsync(string filePath);
}