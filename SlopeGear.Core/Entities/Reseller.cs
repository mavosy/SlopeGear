namespace SlopeGear.Domain.Entities;

public class Reseller
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string ApiKey { get; set; }

    public string Email { get; set; }

    public bool IsActive { get; set; }

    public decimal DiscountPercentage { get; set; }

    public ICollection<Order> Orders { get; set; } = [];        // NavProp: one Reseller <=> many Orders

    public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = [];      // NavProp: one Reseller <=> many InventoryTransactions
}

    // TODO : Add reseller discount for specific category or product, and tier based discount