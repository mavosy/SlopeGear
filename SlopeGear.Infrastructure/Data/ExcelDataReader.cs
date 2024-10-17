using ClosedXML.Excel;
using SlopeGear.Application.Interfaces;
using SlopeGear.Contracts.Dtos;

namespace SlopeGear.Infrastructure.Data;

public class ExcelDataReader : IExcelDataReader
{
    // Example class of reading data from Excel file
    public async Task<IEnumerable<ProductDto>> ReadProductsFromExcelAsync(string filePath)
    {
        var products = new List<ProductDto>();

        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException("File path must be provided.", nameof(filePath));
        }

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("The Excel file was not found.", filePath);
        }

        // Using Task.Run to offload work to background thread.
        await Task.Run(() =>
        {
            using var workbook = new XLWorkbook(filePath);
            var worksheet = workbook.Worksheet(1); // Selects first sheet in excel doc

            // Starts from second row, first row contains headers
            IEnumerable<IXLRangeRow> rows = worksheet.RangeUsed().RowsUsed().Skip(1);

            foreach (var row in rows)
            {
                var productDto = new ProductDto
                {
                    Name = row.Cell(1).GetString(),
                    Description = row.Cell(2).GetString(),
                    Price = row.Cell(3).GetValue<decimal>(),
                    CurrentStockQuantity = row.Cell(4).GetValue<int>(),
                    CategoryId = row.Cell(5).GetValue<int?>()
                };

                products.Add(productDto);
            }
        });

        return products;
    }
}