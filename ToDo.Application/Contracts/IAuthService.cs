using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.Contracts;

public interface IAuthService
{
    Task<TokenViewModel?> Login(LoginInputModel inputModel);
    Task<UserViewModel?> Register(RegisterInputModel inputModel);
}