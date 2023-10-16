using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Filter;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Abstractions;
using ToDo.Infra.Data.Context;
using ToDo.Infra.Data.Paged;

namespace ToDo.Infra.Data.Repositories;

public sealed class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public AssignmentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Assignment?> GetByIdAsync(int id, int? userId) // entender
    {
        return await _dbContext.Assignments.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<IPagedResult<Assignment>> SearchAsync(int? userId, AssignmentFilter filter, int perPage = 10, int page = 1, int? listId = null) //entender como funciona
    {
        var query = DbContext.Assignments
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .AsQueryable();

        ApplyFilter(userId, filter, ref query, listId);

        var result = new PagedResult<Assignment>
        {
            Items = await query.Skip((page - 1) * perPage).Take(perPage).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page,
            PerPage = perPage
        };

        var pageCount = (double)result.Total / perPage;
        result.PageCount = (int)Math.Ceiling(pageCount);

        return result;
    }
    
    private static void ApplyFilter(int? userId, AssignmentFilter filter, ref IQueryable<Assignment> query, int? listId = null) //entender como funciona
    {
        if (!string.IsNullOrWhiteSpace(filter.Description))
            query = query.Where(c => c.Description.Contains(filter.Description));

        if (filter.Concluded.HasValue)
            query = query.Where(c => c.Concluded == filter.Concluded.Value);

        if (filter.StartDeadline.HasValue)
            query = query.Where(c => c.Deadline != null && c.Deadline.Value >= filter.StartDeadline.Value);

        if (filter.EndDeadline.HasValue)
            query = query.Where(c => c.Deadline != null && c.Deadline.Value <= filter.EndDeadline.Value);

        if (listId.HasValue)
        {
            query = query
                .Where(c => c.AssignmentListId == listId)
                .Where(c => c.AssignmentList.UserId == userId);
        }
    }
}