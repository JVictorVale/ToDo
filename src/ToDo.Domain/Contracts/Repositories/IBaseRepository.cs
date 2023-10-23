using System.Linq.Expressions;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Repositories;

public interface IBaseRepository<T> : IDisposable where T : BaseEntity
{
    void Create(T entity);
    Task<T?> GetByIdAsync(int? id);
    Task<List<T>> GetAllAsync();
    void Update(T entity);
    void Delete(T entity);
    
    public IUnityOfWork UnityOfWork { get; }
    public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
}