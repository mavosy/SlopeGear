using SlopeGear.Domain.Enums;

namespace SlopeGear.Domain.Entities;

public class InventoryTransaction
{
    public int Id { get; set; }

    public int QuantityChange { get; set; }

    public DateTime TransactionDate { get; set; }

    public TransactionType TransactionType { get; set; }

    public int ProductId { get; set; }      // Foreign Key

    public Product Product { get; set; }    // NavProp: many InventoryTransactions <=> one Product

    public int? ResellerId { get; set; }    // Foreign Key

    public Reseller? Reseller { get; set; }     // NavProp: many InventoryTransactions <=> one Reseller
}