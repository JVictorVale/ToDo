using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Abstractions;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<User?> GetByEmailAsync(string email) //entender
    {
        var user = await DbContext.Users.Where(x => x.Email.ToLower() == email.ToLower()).AsNoTracking()
            .FirstOrDefaultAsync();

        return user;
    }
}