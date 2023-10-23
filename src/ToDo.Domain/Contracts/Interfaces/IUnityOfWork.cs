namespace ToDo.Domain.Contracts.Interfaces;

public interface IUnityOfWork
{
    Task<bool> Commit();
}