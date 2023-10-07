using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Context;

namespace ToDo.Infra.Data.Repositories;

public class AssignmentListRepository : BaseRepository<AssignmentList>, IAssignmentListRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public AssignmentListRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<AssignmentList> GetByIdAsync(int id, int userId)
    {
        return await _dbContext.AssignmentLists
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id && a.UserId == userId);
    }

    public async Task<List<AssignmentList>> SearchByNameAsync(string name)
    {
        return await _dbContext.AssignmentLists.
            AsNoTracking()
            .Where(a => a.Name == name)
            .ToListAsync();
    }

    public async Task<AssignmentList> GetByNameAsync(string name, int userId)
    {
        return await _dbContext.AssignmentLists
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Name == name && a.UserId == userId);
    }
    
    public async Task<List<AssignmentList>> GetAllAsync(int userId)
    {
        return await _dbContext.AssignmentLists
            .AsNoTracking()
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }
}