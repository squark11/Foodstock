using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<User>> GetAllWithIncludedAsync()
    {
        var users = await _dbContext.Users
            .Include(x => x.Role)
            .Where(x => x.IsActive)
            .ToListAsync();
        return users;
    }

    public async Task<List<User>> GetAllInactiveWithIncludeAsync()
    {
        var users = await _dbContext.Users
            .Include(x => x.Role)
            .Where(x => !x.IsActive)
            .ToListAsync();
        return users;    
    }

    public async Task<User> GetUserDetailAsync(Guid id)
    {
        var user = await _dbContext.Users
            .Include(r => r.Role)
            .SingleOrDefaultAsync(u => u.Id == id);
        return user;
    }
}
