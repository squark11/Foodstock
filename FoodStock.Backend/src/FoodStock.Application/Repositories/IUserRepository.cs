using FoodStock.Core.Entities;

namespace FoodStock.Application.Repositories;

public interface IUserRepository : IAsyncRepository<User>
{
    Task<List<User>> GetAllWithIncludedAsync();
    Task<List<User>> GetAllInactiveWithIncludeAsync();
    Task<User> GetUserDetailAsync(Guid id);
}
