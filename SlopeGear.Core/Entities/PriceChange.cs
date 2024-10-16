namespace SlopeGear.Domain.Entities;

public class PriceChange
{
    public int Id { get; set; }

    public decimal OldPrice { get; set; }

    public decimal NewPrice { get; set; }

    public DateTime DateChanged { get; set; }

    public int ProductId { get; set; }      // Foreign Key

    public Product Product { get; set; }    // NavProp: many PriceChanges <=> one Product
}