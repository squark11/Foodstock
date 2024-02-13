using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class ProducentRepository : BaseRepository<Producent>, IProducentRepository
{
    public ProducentRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }
}
