using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using ToDo.Application.Contracts;
using ToDo.Application.DTO.ViewModel;
using ToDo.Application.DTOs.InputModel;
using ToDo.Application.Notification;
using ToDo.Domain.Contracts.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Application.Services;

public class AuthService : BaseService, IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthService(IMapper mapper, INotificator notificator, IConfiguration configuration, IUserRepository userRepository, IPasswordHasher<User> passwordHasher) : base(mapper, notificator)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<TokenViewModel?> Login(LoginInputModel inputModel)
    {
        if (!inputModel.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return null;
        }

        var user = await _userRepository.GetByEmailAsync(inputModel.Email);

        if (user != null && _passwordHasher.VerifyHashedPassword(user, user.Password!, inputModel.Password) ==
            PasswordVerificationResult.Success)
        {
            return GenerateToken(user);
        }

        Notificator.Handle("E-mail ou senha estão incorretos.");
        return null;
    }

    public async Task<UserViewModel?> Register(RegisterInputModel inputModel)
    {
        var user = Mapper.Map<User>(inputModel);

        if (!inputModel.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
            return null;
        }

        var getUser = await _userRepository.GetByEmailAsync(inputModel.Email);

        if (getUser != null)
        {
            Notificator.Handle("Já existe um usuário cadastrado com o email informado.");
            return null;
        }

        user.Password = _passwordHasher.HashPassword(user, inputModel.Password);

        _userRepository.CreateAsync(user);

        if (await _userRepository.UnityOfWork.Commit())
            return Mapper.Map<UserViewModel>(user);

        Notificator.Handle("Não foi possível cadastrar o usuário");
        return null;
    }
    
    private TokenViewModel GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"] ?? string.Empty);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new(ClaimTypes.Role, "User"),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                ),
            Expires =
                DateTime.UtcNow.AddHours(int.Parse(_configuration["AppSettings:ExpirationHours"] ?? string.Empty)),
            Issuer = _configuration["AppSettings:Issuer"],
            Audience = _configuration["AppSettings:ValidOn"]
        });

        var encodedToken = tokenHandler.WriteToken(token);

        return new TokenViewModel
        {
            AccessToken = encodedToken
        };
    }
}