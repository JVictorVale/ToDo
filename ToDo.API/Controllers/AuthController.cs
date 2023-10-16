using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using ToDo.API.Responses;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
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
    [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterInputModel inputModel)
    {
        var registerUser = await _authService.Register(inputModel);
        return OkResponse(registerUser);
    }

    [HttpPost("login")]
    [SwaggerOperation(Summary = "Login in a account")]
    [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Login([FromBody] LoginInputModel inputModel)
    {
        var token = await _authService.Login(inputModel);
        return OkResponse(token);
    }
}