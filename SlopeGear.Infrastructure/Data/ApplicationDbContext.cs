using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using SlopeGear.Domain.Entities;

namespace SlopeGear.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<PriceChange> PriceChanges { get; set; }
    public DbSet<Reseller> Resellers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Category> Categorys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            // PK
            entity.HasKey(p => p.Id);

            // Property config
            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(p => p.Description)
                .HasMaxLength(500);

            entity.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(p => p.CurrentStockQuantity)
                .IsRequired();

            // FK
            entity.Property(p => p.CategoryId)
                .IsRequired(false);  // Nullable FK

            // Relationships

            // One Product <=> Many PriceChanges
            entity.HasMany(p => p.PriceChanges)
                .WithOne(pc => pc.Product)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Product <=> Many InventoryTransactions
            entity.HasMany(p => p.InventoryTransactions)
                .WithOne(it => it.Product)
                .HasForeignKey(it => it.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Product <=> Many OrderItems
            entity.HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // One Category <=> Many Products
            entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            // PK
            entity.HasKey(c => c.Id);

            // Property config
            entity.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(c => c.Description)
                .HasMaxLength(500);

            // Relationships

            // One Category <=> Many Products
            entity.HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<PriceChange>(entity =>
        {
            // PK
            entity.HasKey(pc => pc.Id);

            // Property config
            entity.Property(pc => pc.OldPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(pc => pc.NewPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            entity.Property(pc => pc.DateChanged)
                .HasColumnType("datetime2")
                .IsRequired();

            // FK
            entity.Property(pc => pc.ProductId)
                .IsRequired();

            // Relationships

            // One Product <=> Many PriceChanges
            entity.HasOne(pc => pc.Product)
                .WithMany(p => p.PriceChanges)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<InventoryTransaction>(entity =>
        {
            // PK
            entity.HasKey(it => it.Id);

            entity.Property(it => it.QuantityChange)
                .IsRequired();

            entity.Property(it => it.TransactionDate)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(it => it.TransactionType)
                .HasConversion<int>() // Store enum as integer
                .IsRequired();

            // FK
            entity.Property(it => it.ProductId)
                .IsRequired();

            // FK
            entity.Property(it => it.ResellerId)
                .IsRequired(false);  // Nullable

            // Relationships

            // One Product <=> Many InventoryTransactions
            entity.HasOne(it => it.Product)
                .WithMany(p => p.InventoryTransactions)
                .HasForeignKey(it => it.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Reseller <=> Many InventoryTransactions
            entity.HasOne(it => it.Reseller)
                .WithMany(r => r.InventoryTransactions)
                .HasForeignKey(it => it.ResellerId)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<Reseller>(entity =>
        {
            // PK
            entity.HasKey(r => r.Id);

            // Property config
            entity.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(r => r.ApiKey)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(r => r.Email)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(r => r.IsActive)
                .IsRequired();

            entity.Property(r => r.DiscountPercentage)
                .HasColumnType("decimal(5,2)")  // 5 total digits, 2 after the decimal
                .IsRequired();

            // Relationships

            // One Reseller <=> Many Orders
            entity.HasMany(r => r.Orders)
                .WithOne(o => o.Reseller)
                .HasForeignKey(o => o.ResellerId)
                .OnDelete(DeleteBehavior.SetNull);  

            // One Reseller <=> Many InventoryTransactions
            entity.HasMany(r => r.InventoryTransactions)
                .WithOne(it => it.Reseller)
                .HasForeignKey(it => it.ResellerId)
                .OnDelete(DeleteBehavior.Cascade);  
        });

        modelBuilder.Entity<Order>(entity =>
        {
            // PK
            entity.HasKey(o => o.Id);

            // Property config
            entity.Property(o => o.OrderDate)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // FK
            entity.Property(o => o.ResellerId)
                .IsRequired();

            // Relationships

            // One Reseller <=> Many Orders
            entity.HasOne(o => o.Reseller)
                .WithMany(r => r.Orders)
                .HasForeignKey(o => o.ResellerId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Order <=> Many OrderItems
            entity.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            // PK
            entity.HasKey(oi => oi.Id);

            // Property config
            entity.Property(oi => oi.Quantity)
                .IsRequired();

            entity.Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // TotalPrice is not stored in db, derived from UnitPrice * Quantity

            // FK
            entity.Property(oi => oi.OrderId)
                .IsRequired();

            // FK
            entity.Property(oi => oi.ProductId)
                .IsRequired();

            // Relationships

            // One Order <=> Many OrderItems
            entity.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // One Product <=> Many OrderItems
            entity.HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}