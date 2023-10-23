using ToDo.Application.DTOs.Auth;
using ToDo.Application.DTOs.User;

namespace ToDo.Application.Contracts;

public interface IAuthService
{
    Task<TokenDto?> Login(LoginDto dto);
    Task<UserDto?> Register(RegisterDto dto);
}