using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record ProductDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Id cannot be negative.")]
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; init; }

    [MaxLength(500)]
    public string Description { get; init; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; init; }

    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
    public int CurrentStockQuantity { get; init; }

    public int? CategoryId { get; init; }  // Optional
}