using AutoMapper;
using ToDo.Application.Notification;

namespace ToDo.Application.Services;

public abstract class BaseService
{
    protected readonly IMapper _mapper;
    protected readonly INotificator _notificator;

    protected BaseService(IMapper mapper, INotificator notificator)
    {
        _mapper = mapper;
        _notificator = notificator;
    }
}