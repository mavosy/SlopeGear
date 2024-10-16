using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record OrderDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Id cannot be negative.")]
    public int? Id { get; set; }

    [Required]
    public int ResellerId { get; init; }

    [Required]
    [MinLength(1, ErrorMessage = "Order must contain at least one item.")]
    public ICollection<OrderItemDto> OrderItems { get; init; } = [];
}