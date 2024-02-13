using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }
}
