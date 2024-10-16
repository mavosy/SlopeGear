using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record OrderUpdateDto
{
    [Required]
    public int Id { get; init; }

    public int? ResellerId { get; init; }

    public ICollection<OrderItemUpdateDto>? UpdatedOrderItems { get; init; }

    public ICollection<OrderItemDto>? NewOrderItems { get; init; }

    public ICollection<int>? OrderItemIdsToRemove { get; init; }
}