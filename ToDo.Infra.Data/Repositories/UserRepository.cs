using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> SearchByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<List<User>> SearchByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}