using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> CreateAsync(T obj)
    {
        _dbContext.Add(obj);
        await _dbContext.SaveChangesAsync();
        return obj;
    }

    public async Task<T> UpdateAsync(T obj)
    {
        _dbContext.Update(obj);
        await _dbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<T?> RemoveAsync(int id)
    {
        var obj = await GetByIdAsync(id);

        _dbContext.Remove(obj);
        await _dbContext.SaveChangesAsync();

        return obj;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return  await _dbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<T>> GetAsync()
    {
        return await _dbContext.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}