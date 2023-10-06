using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByNameAsync(string name);
    Task<List<User>> SearchByEmailAsync(string email);
    Task<List<User>> SearchByNameAsync(string name);
}