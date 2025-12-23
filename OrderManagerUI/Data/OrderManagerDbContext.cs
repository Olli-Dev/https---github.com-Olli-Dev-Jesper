using Microsoft.EntityFrameworkCore;

namespace OrderManagerUI.Data;

public class OrderManagerDbContext : DbContext
{
    public OrderManagerDbContext(DbContextOptions<OrderManagerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductConfig> ProductConfigs { get; set; }
    public DbSet<ProductMetadata> ProductMetadata { get; set; }
    public DbSet<ProductInterestTier> ProductInterestTiers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure composite keys
        builder.Entity<Product>()
            .HasKey(p => new { p.LDB_ID, p.PRODUCT_ID });

        builder.Entity<ProductConfig>()
            .HasKey(pc => new { pc.LDB_ID, pc.CONFIG_ID });

        builder.Entity<ProductMetadata>()
            .HasKey(pm => new { pm.LDB_ID, pm.PRODUCT_ID, pm.LANGUAGE_CODE });

        builder.Entity<ProductInterestTier>()
            .HasKey(pit => new { pit.LDB_ID, pit.PRODUCT_ID, pit.INTEREST_TYPE, pit.LIMIT });

        // Configure relationships
        builder.Entity<ProductConfig>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.Configs)
            .HasForeignKey(pc => new { pc.LDB_ID, pc.PRODUCT_ID });

        builder.Entity<ProductMetadata>()
            .HasOne(pm => pm.Product)
            .WithMany(p => p.Metadata)
            .HasForeignKey(pm => new { pm.LDB_ID, pm.PRODUCT_ID });

        builder.Entity<ProductInterestTier>()
            .HasOne(pit => pit.Product)
            .WithMany(p => p.InterestTiers)
            .HasForeignKey(pit => new { pit.LDB_ID, pit.PRODUCT_ID });
    }
}

