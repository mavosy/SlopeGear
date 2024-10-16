using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record OrderItemDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Id cannot be negative.")]
    public int? Id { get; set; }

    [Required]
    public int ProductId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; init; }
}