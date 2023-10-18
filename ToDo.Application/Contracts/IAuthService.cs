using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.DTOs.ViewModel;

namespace ToDo.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto dto);
    Task<UserDto?> Register(RegisterDto dto);
}