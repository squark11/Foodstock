using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }
}
