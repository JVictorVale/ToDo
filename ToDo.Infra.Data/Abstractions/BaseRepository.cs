using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Abstractions;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private bool _isDisposed;
    private readonly DbSet<T> _dbSet;
    protected readonly ApplicationDbContext DbContext;

    protected BaseRepository(ApplicationDbContext context)
    {
        DbContext = context;
        _dbSet = context.Set<T>();
    }

    public virtual IUnityOfWork UnityOfWork => DbContext;
    
    public  async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression) //entender
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).FirstOrDefaultAsync();
    }

    public virtual void CreateAsync(T entity) //não é async
    {
        _dbSet.Add(entity);
    }

    public virtual async Task<T?> GetByIdAsync(int? id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual void UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }
}