using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsIncludedAsync()
    {
        var orderItems = await _dbContext.OrderItems
            .Include(x => x.Category)
            .Include(x => x.Producent)
            .Include(x => x.Supplier)
            .Include(x => x.Order)
            .ToListAsync();
        return orderItems;
    }

    public async Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId)
    {
        var orderItems = await _dbContext.OrderItems
            .Include(x => x.Category)
            .Include(x => x.Producent)
            .Include(x => x.Supplier)
            .Include(x => x.Order)
            .Where(x => x.OrderId == orderId)
            .ToListAsync();
        return orderItems;
    }
}
