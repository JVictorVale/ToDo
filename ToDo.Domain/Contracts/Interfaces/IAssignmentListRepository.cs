using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IAssignmentListRepository : IBaseRepository<AssignmentList>
{
    Task<AssignmentList> GetByIdAsync(int id, int userId);
    Task<List<AssignmentList>> SearchByNameAsync(string name);
    Task<AssignmentList> GetByNameAsync(string name);
    Task<List<AssignmentList>> GetAllAsync(int userId);
}