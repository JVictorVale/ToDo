using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Filter;
using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Repositories;

public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    Task<Assignment?> GetByIdAsync(int id, int? userId);
    
    Task<IPagedResult<Assignment>> SearchAsync(int? userId, AssignmentFilter filter, int perPage = 10,
        int page = 1, int? listId = null);
}