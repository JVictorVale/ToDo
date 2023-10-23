using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Repositories;

public interface IAssignmentListRepository : IBaseRepository<AssignmentList>
{
    Task<AssignmentList?> GetByIdAsync(int? id, int? userId);
    
    Task<IPagedResult<AssignmentList>> SearchAsync(int? userId, string name, string description, int perPage = 10, int page = 1);
}