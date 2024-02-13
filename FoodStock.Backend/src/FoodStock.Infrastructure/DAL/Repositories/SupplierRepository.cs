using FoodStock.Application.Repositories;
using FoodStock.Core.Entities;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal sealed class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(FoodStockDbContext dbContext) : base(dbContext)
    {
    }
}
