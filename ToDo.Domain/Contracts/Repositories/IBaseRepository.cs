using System.Linq.Expressions;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    void CreateAsync(T entity);
    Task<T?> GetByIdAsync(int? id);
    Task<List<T>> GetAllAsync();
    void UpdateAsync(T entity);
    void DeleteAsync(T entity);
    
    public IUnityOfWork UnityOfWork { get; }
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
}