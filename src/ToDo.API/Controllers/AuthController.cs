using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.API.Responses;
using ToDo.Application.Contracts;
using ToDo.Application.DTOs.Auth;
using ToDo.Application.DTOs.User;
using ToDo.Application.Notification;

namespace ToDo.API.Controllers;

[Route("auth")]
public class AuthController : MainController
{
    private readonly IAuthService _authService;
    
    public AuthController(INotificator notificator, IAuthService authService) : base(notificator)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    [SwaggerOperation(Summary = "Register account")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var registerUser = await _authService.Register(dto);
        return OkResponse(registerUser);
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login in a account")]
    [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await _authService.Login(dto);
        return OkResponse(token);
    }
}