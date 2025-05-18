using System.Linq.Expressions;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<T?> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        
        return await query.SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task<ICollection<T>> GetAllAsync() 
        => await _dbSet.ToListAsync();

    public async Task<ICollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        
        foreach (var include in includes)
            query = query.Include(include);
        
        return await query.ToListAsync();
    }

    public async Task<ICollection<T>> GetListBy(Expression<Func<T, bool>> predicate) 
        => await _dbSet.Where(predicate).ToListAsync();

    public async Task<ICollection<T>> GetListBy(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();
        
        foreach (var include in includes)
            query = query.Include(include);
        
        return await query.Where(predicate).ToListAsync();
    }

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

    public async Task<T?> GetBy(Expression<Func<T, bool>> predicate) 
        => await _dbSet.FirstOrDefaultAsync(predicate);
}
