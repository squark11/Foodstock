using FoodStock.Core.Entities;

namespace FoodStock.Application.Repositories;

public interface IProductRepository : IAsyncRepository<Product>
{
    Task<List<Product>> GetAllWithIncludesAsync();
    Task<List<Product>> GetAllOrderByExpirationDateAscAsync();
    Task<Product> GetProductDetailAsync(Guid id);
}
