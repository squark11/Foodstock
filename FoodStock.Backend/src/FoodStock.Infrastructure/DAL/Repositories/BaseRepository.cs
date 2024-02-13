using FoodStock.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodStock.Infrastructure.DAL.Repositories;

internal class BaseRepository<T> : IAsyncRepository<T> where T : class
{
    protected readonly FoodStockDbContext _dbContext;

    public BaseRepository(FoodStockDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().AsNoTracking()
            .ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbContext.Set<T>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
