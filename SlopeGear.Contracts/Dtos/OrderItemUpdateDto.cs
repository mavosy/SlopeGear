using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record OrderItemUpdateDto
{
    [Required]
    public int Id { get; init; }

    public int? ProductId { get; init; }

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int? Quantity { get; init; }
}