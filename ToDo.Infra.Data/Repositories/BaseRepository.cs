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
        throw new NotImplementedException();
    }

    public async Task<T> UpdateAsync(T obj)
    {
        throw new NotImplementedException();
    }

    public async Task<T> RemoveAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<T>> GetAsync()
    {
        throw new NotImplementedException();
    }
}