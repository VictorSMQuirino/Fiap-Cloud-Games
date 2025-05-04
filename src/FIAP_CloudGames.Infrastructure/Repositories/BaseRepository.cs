using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FIAP_CloudGames.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly FiapCloudGamesDbContext _context;
    private readonly DbSet<T> _dbSet;

    public BaseRepository(FiapCloudGamesDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id) 
        => await _dbSet.FindAsync(id);

    public async Task<ICollection<T>> GetAllAsync() 
        => await _dbSet.ToListAsync();

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsBy(Expression<Func<T, bool>> predicate) 
        => await _dbSet.AnyAsync(predicate);
}
