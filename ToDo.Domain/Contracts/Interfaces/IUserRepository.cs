using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}