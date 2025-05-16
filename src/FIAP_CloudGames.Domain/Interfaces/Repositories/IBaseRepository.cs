using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;

namespace FIAP_CloudGames.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<ICollection<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsBy(Expression<Func<T, bool>> predicate);
    Task<T?> GetBy(Expression<Func<T, bool>> predicate);
}
