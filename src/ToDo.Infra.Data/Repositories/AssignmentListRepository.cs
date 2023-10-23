using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Contracts.Repositories;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Abstractions;
using ToDo.Infra.Data.Context;
using ToDo.Infra.Data.Paged;

namespace ToDo.Infra.Data.Repositories;

public sealed class AssignmentListRepository : BaseRepository<AssignmentList>, IAssignmentListRepository
{
    public AssignmentListRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }


    public async Task<IPagedResult<AssignmentList>> SearchAsync(int? userId, string name, string description, int perPage = 10, int page = 1)
    {
        var query = DbContext.AssignmentLists
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(x => x.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(x => x.Description.Contains(description));

        var result = new PagedResult<AssignmentList>
        {
            Items = await query.OrderBy(x => x.Name).Include(x => x.Assignments).Skip((page - 1) * perPage)
                .Take(perPage).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page,
            PerPage = perPage
        };

        var pageCount = (double)result.Total / perPage;
        result.PageCount = (int)Math.Ceiling(pageCount);

        return result;
    }

    public async Task<AssignmentList?> GetByIdAsync(int? id, int? userId)
    {
        return await DbContext.AssignmentLists
            .Include(x => x.Assignments)
            .FirstOrDefaultAsync(x => id != null && x.Id == id && x.UserId == userId);
    }
}