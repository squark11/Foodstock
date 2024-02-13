using FoodStock.Core.Entities;

namespace FoodStock.Application.Repositories;

public interface IOrderItemRepository : IAsyncRepository<OrderItem>
{
    Task<IEnumerable<OrderItem>> GetOrderItemsIncludedAsync();
    Task<IEnumerable<OrderItem>> GetOrderItemsByOrderId(Guid orderId);

    
}
