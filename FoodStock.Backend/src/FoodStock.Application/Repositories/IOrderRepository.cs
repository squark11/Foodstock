using FoodStock.Core.Entities;

namespace FoodStock.Application.Repositories;

public interface IOrderRepository : IAsyncRepository<Order>
{
    Task<IEnumerable<Order>> GetAllIncludedAsync();
    Task<Order> GetOrderDetailAsync(Guid id);
}
