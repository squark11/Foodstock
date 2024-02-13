using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<Order>> GetAllIncludedAsync()
    {
        var orders = await _dbContext.Orders
            .Include(x => x.Organization)
            .Include(x => x.Supplier)
            .Include(x => x.User)
            .Include(x => x.OrderItems)
                .ThenInclude(oi => oi.Category)
            .Include(x => x.OrderItems)
                .ThenInclude(oi => oi.Producent)
            .ToListAsync();
        return orders;
    }

    public async Task<Order> GetOrderDetailAsync(Guid id)
    {
        var order = await _dbContext.Orders
            .Include(x => x.Organization)
            .Include(x => x.Supplier)
            .Include(x => x.User)
            .Include(x => x.OrderItems)
            .ThenInclude(oi => oi.Category)
            .Include(x => x.OrderItems)
            .ThenInclude(oi => oi.Producent)
            .FirstOrDefaultAsync(o => o.Id == id);
        return order;
    }
}
