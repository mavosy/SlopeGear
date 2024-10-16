using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record ResellerUpdateDto
{
    [MaxLength(100)]
    public string? Name { get; init; }

    [EmailAddress]
    [MaxLength(150)]
    public string? Email { get; init; }

    [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
    public decimal? DiscountPercentage { get; init; }

    public bool? IsActive { get; init; }
}