using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sirius.Application;
using Sirius.Infrastructure.Data;

namespace Sirius.Infrastructure.Repositories;

public class BaseRepository<TEntity>(ApplicationDbContext context): IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(object? id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet
            .Where(filter)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task CreateAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public async Task Delete(object? id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) _dbSet.Remove(entity);
    }
}