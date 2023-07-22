using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using oneparalyzer.ProjectManager.Auth.Api.Constants;
using oneparalyzer.ProjectManager.Auth.Api.Contracts.OperationResults;
using oneparalyzer.ProjectManager.Auth.Api.Contracts.Requests;
using oneparalyzer.ProjectManager.Auth.Api.Entities;
using oneparalyzer.ProjectManager.Auth.Api.Helpers.Interfaces;
using oneparalyzer.ProjectManager.Auth.Api.Persistance;

namespace oneparalyzer.ProjectManager.Auth.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenHelper _jwtTokenHelper;
    private readonly ILogger<AuthController> _logger;

    public AuthController(
        ILogger<AuthController> logger,
        UserManager<User> userManager, 
        IJwtTokenHelper jwtTokenHelper)
    {
        _logger = logger;
        _userManager = userManager;
        _jwtTokenHelper = jwtTokenHelper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync(RegisterRequest request)
    {
        var result = new SimpleResult();

        try
        {
            User? userByEmail = await _userManager.FindByEmailAsync(request.Email);
            User? userByName = await _userManager.FindByNameAsync(request.Email);
            if ((userByName is not null) || (userByEmail is not null))
            {
                result.AddError("Пользователь с таким именем пользователя или Email уже существует.");
                return Ok(result);
            }

            var user = new User
            {
                UserName = request.UserName,
                Email = request.Email
            };
            await _userManager.CreateAsync(user, request.Password);

            await _userManager.AddToRoleAsync(user, RoleConstants.User);

            return Ok(result);
        }
        catch (Exception ex)
        {
            result.AddError("Произошла ошибка.");
            _logger.LogError(ex.Message);
            return Ok(result);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync(LoginRequest request)
    {
        var result = new Result<string>();

        try
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);

            bool emailPasswordIsCorrect = await _userManager
                .CheckPasswordAsync(user, request.Password);
            if (!emailPasswordIsCorrect)
            {
                result.AddError("Не верный Email или пароль.");
                return Ok(result);
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            string token = _jwtTokenHelper.GenerateToken(user, roles);

            result.Data = token;
            return Ok(result);
        }
        catch (Exception ex)
        {
            result.AddError("Произошла ошибка.");
            _logger.LogError(ex.Message);
            return Ok(result);
        }
    }
}
