namespace SlopeGear.Domain.Entities;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int CurrentStockQuantity { get; set; }

    public ICollection<PriceChange> PriceChanges { get; set; } = [];    // NavProp: one Product <=> many PriceChanges

    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];      // NavProp: one Product <=> many InventoryTransactions

    public ICollection<OrderItem> OrderItems { get; set; } = [];      // NavProp: one Product <=> many InventoryTransactions

    public int? CategoryId { get; set; }     // Foreign Key

    public Category? Category { get; set; }      // NavProp: many Products <=> one Category
}

// TODO : Add Weight, Dimensons, Color, Size, ImageUrl or just png, DateAdded etc.