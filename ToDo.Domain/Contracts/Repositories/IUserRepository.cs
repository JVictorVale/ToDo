using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}