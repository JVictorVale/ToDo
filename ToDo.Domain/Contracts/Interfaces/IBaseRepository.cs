using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T obj);
    Task<T> UpdateAsync(T obj);
    Task<T> RemoveAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAsync();
}