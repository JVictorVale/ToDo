using FluentValidation.Results;

namespace ToDo.Application.Notification;

public class Notificator : INotificator
{
    private bool _notFoundResource;
    private readonly List<string> _notifications = new();
    
    public void Handle(string message)
    {
        throw new NotImplementedException();
    }

    public void Handle(List<ValidationFailure> failures)
    {
        throw new NotImplementedException();
    }

    public void HandleNotFoundResource()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<string> GetNotifications()
    {
        throw new NotImplementedException();
    }

    public bool HasNotification { get; }
    public bool IsNotFoundResource { get; }
}