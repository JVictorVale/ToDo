using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Abstractions;
using ToDo.Infra.Data.Context;
using ToDo.Infra.Data.Paged;

namespace ToDo.Infra.Data.Repositories;

public sealed class AssignmentListRepository : BaseRepository<AssignmentList>, IAssignmentListRepository
{
    public AssignmentListRepository(ApplicationDbContext dbContext) : base(dbContext)
    { }


    public async Task<IPagedResult<AssignmentList>> Search(int? userId, string name, string description, int perPage = 10, int page = 1) // entender
    {
        var query = DbContext.AssignmentLists
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(name))
            query = query.Where(c => c.Name.Contains(name));

        if (!string.IsNullOrWhiteSpace(description))
            query = query.Where(c => c.Description.Contains(description));

        var result = new PagedResult<AssignmentList>
        {
            Items = await query.OrderBy(c => c.Name).Include(c => c.Assignments).Skip((page - 1) * perPage)
                .Take(perPage).ToListAsync(),
            Total = await query.CountAsync(),
            Page = page,
            PerPage = perPage
        };

        var pageCount = (double)result.Total / perPage;
        result.PageCount = (int)Math.Ceiling(pageCount);

        return result;
    }

    public async Task<AssignmentList?> GetById(int? id, int? userId) // entender
    {
        return await DbContext.AssignmentLists
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => id != null && c.Id == id && c.UserId == userId);
    }
}