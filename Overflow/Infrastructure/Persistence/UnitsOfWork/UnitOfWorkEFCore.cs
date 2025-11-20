using Application.Contracts.Persistence;

namespace Persistence.UnitsOfWork;

public class UnitOfWorkEFCore(PostServerDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
    {
        await context.SaveChangesAsync();
    }

    public Task RollbackAsync()
    {
        return Task.CompletedTask;
    }
}
