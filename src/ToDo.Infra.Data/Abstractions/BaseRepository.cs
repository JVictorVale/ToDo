using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Abstractions;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
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
    
    public  async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).FirstOrDefaultAsync();
    }

    public virtual void Create(T entity)
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

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            DbContext.Dispose();
        }

        _isDisposed = true;
    }
}