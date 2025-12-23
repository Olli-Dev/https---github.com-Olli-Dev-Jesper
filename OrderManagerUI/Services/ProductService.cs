using Microsoft.EntityFrameworkCore;
using OrderManagerUI.Data;

namespace OrderManagerUI.Services;

public class ProductService
{
    private readonly IDbContextFactory<OrderManagerDbContext> _dbContextFactory;

    public ProductService(IDbContextFactory<OrderManagerDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Products
            .OrderBy(p => p.INTERNAL_NAME)
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int ldbId, int productId)
    {
        using var context = _dbContextFactory.CreateDbContext();
        return await context.Products
            .Include(p => p.Configs)
            .Include(p => p.Metadata)
            .Include(p => p.InterestTiers)
            .FirstOrDefaultAsync(p => p.LDB_ID == ldbId && p.PRODUCT_ID == productId);
    }

    public async Task SaveProductAsync(Product product)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var existing = await context.Products
            .FirstOrDefaultAsync(p => p.LDB_ID == product.LDB_ID && p.PRODUCT_ID == product.PRODUCT_ID);

        if (existing == null)
        {
            context.Products.Add(product);
        }
        else
        {
            context.Entry(existing).CurrentValues.SetValues(product);
        }

        await context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int ldbId, int productId)
    {
        using var context = _dbContextFactory.CreateDbContext();
        var product = await context.Products
            .FirstOrDefaultAsync(p => p.LDB_ID == ldbId && p.PRODUCT_ID == productId);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}

