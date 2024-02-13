using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(FoodStockDbContext dbContext) : base(dbContext)
    { }


    public async Task<List<Product>> GetAllWithIncludesAsync()
    {
        var products = await _dbContext
            .Products
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(p => p.Producent)
            .Include(s => s.Supplier)
            .ToListAsync();
        return products;
    }

    public async Task<List<Product>> GetAllOrderByExpirationDateAscAsync()
    {
        var products = await _dbContext
            .Products
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(p => p.Producent)
            .Include(s => s.Supplier)
            .OrderBy(p => p.ExpirationDate)
            .ToListAsync();
        return products;
    }

    public async Task<Product> GetProductDetailAsync(Guid id)
    {
        var product = await _dbContext
            .Products
            .Include(c => c.Category)
            .Include(u => u.User)
            .Include(p => p.Producent)
            .Include(s => s.Supplier)
            .SingleOrDefaultAsync(x => x.Id == id);
        return product;
    }
}
