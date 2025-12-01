using Application.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class Repository<T>(PostServerDbContext context) : IRepository<T>
    where T : class
{
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public Task<T> AddAsync(T entity)
    {
        context.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(T entity)
    {
        context.Update(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        context.Remove(entity);
        return Task.CompletedTask;
    }
}
