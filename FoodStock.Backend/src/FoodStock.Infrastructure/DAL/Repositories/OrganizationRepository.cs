using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class OrganizationRepository : BaseRepository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }
}
