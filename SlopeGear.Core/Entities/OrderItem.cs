namespace SlopeGear.Domain.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int OrderId { get; set; }    // Foreign Key

    public Order Order { get; set; }    // NavProp: many OrderItems <=> one Order

    public int ProductId { get; set; }      // Foreign Key

    public Product Product { get; set; }    // NavProp: many OrderItems <=> one Product

    public decimal TotalPrice => UnitPrice * Quantity;
}