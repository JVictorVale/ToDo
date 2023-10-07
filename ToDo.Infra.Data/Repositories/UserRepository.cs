using Microsoft.EntityFrameworkCore;
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

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users
            .FirstOrDefaultAsync(u => u != null && u.Email.ToLower() == email);
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<List<User?>> SearchByEmailAsync(string email)
    {
        return await _dbContext.Users
            .Where(u => u != null && u.Email.ToLower().Contains(email.ToLower()))
            .ToListAsync();
    }

    public async Task<List<User?>> SearchByNameAsync(string name)
    {
        return await _dbContext.Users
            .Where(u => u != null && u.Name.ToLower().Contains(name.ToLower()))
            .ToListAsync();
    }
}