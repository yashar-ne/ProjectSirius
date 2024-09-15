using System.Linq.Expressions;

namespace Sirius.Application;

public interface IBaseRepository<T> where T : class
{
    public Task<T?> GetByIdAsync(object? id);
    public Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>> filter);
    public Task CreateAsync(T entity);
    public void Update(T entity);
    public Task Delete(object? id);
    
}