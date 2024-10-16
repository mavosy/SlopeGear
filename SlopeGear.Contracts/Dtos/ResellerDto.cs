using System.ComponentModel.DataAnnotations;

namespace SlopeGear.Contracts.Dtos;

public record ResellerDto
{
    [Range(0, int.MaxValue, ErrorMessage = "Id cannot be negative.")]
    public int? Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; init; }

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; init; }

    [Required]
    [Range(0, 100, ErrorMessage = "Discount percentage must be between 0 and 100.")]
    public decimal DiscountPercentage { get; init; }
}