namespace SlopeGear.Domain.Entities;

public class Order
{
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int ResellerId { get; set; }     // Foreign Key

    public Reseller Reseller { get; set; }      // NavProp: many Orders <=> one Reseller

    public ICollection<OrderItem> OrderItems { get; set; } = [];    // NavProp: one Order <=> many OrderItems
}