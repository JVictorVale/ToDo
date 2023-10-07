using ToDo.Domain.Models;

namespace ToDo.Domain.Contracts.Interfaces;

public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    Task<Assignment> GetByIdAsync(int id, int userId);
}