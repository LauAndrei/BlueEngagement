using System.Security.Claims;
using Core.Constants;
using Core.Dtos.UserDtos;
using Core.Entities;
using Core.EntityExtensions.UserExtensions;
using Core.Exceptions;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenService _tokenService;
    
    
    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    /// <summary>
    ///     Method tested;
    ///     Gets the logged in user having the token renewed
    /// Url path from root: url/Account/
    /// </summary>
    /// <returns>The current user's details having the token renewed</returns>
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<LoggedInUserDto>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        var user = await _userManager.FindByEmailAsync(email);

        return new LoggedInUserDto
        {
            Email = user.Email,
            UserName = user.UserName,
            Score = user.Score,
            Token = await _tokenService.CreateToken(user)
        };
    }
    
    /// <summary>
    ///     Method tested;
    ///     The method search if a user exists with provided email / username and password
    ///     If yes, creates a token for the user
    ///     If not, it means that the user either entered wrong credentials or doesn't exist at all
    ///  so we will return unauthorized.
    /// </summary>
    /// <param name="loginDto">The dto containing user's email / username and the password</param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoggedInUserDto>> Login(LoginDto loginDto)
    {
        User user;
        if (loginDto.UserNameOrEmail.Contains('@'))
        {
            user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
        }
        else
        {
            user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
        }

        if (user == null)
        {
            return Unauthorized("Unauthorized!");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized(RESPONSE_CONSTANTS.USER.INCORRECT_CREDENTIALS);
        }
        
        return user.ToLoggedInUserDto(await _tokenService.CreateToken(user));
    }

    /// <summary>
    ///     Creates and saves a new account, and automatically log in the user
    /// </summary>
    /// <param name="register">
    ///     The object containing the user's necessary info for registering
    /// </param>
    /// <returns></returns>
    /// <exception cref="EmailAlreadyInUseException"></exception>
    /// <exception cref="UserNameAlreadyInUseException"></exception>
    [HttpPost]
    [Route("register")]
    public async Task<ActionResult<LoggedInUserDto>> Register(RegisterDto register)
    {
        var findUser = await _userManager.FindByEmailAsync(register.Email);
        if (findUser is not null)
        {
            throw new EmailAlreadyInUseException();
        }

        findUser = await _userManager.FindByNameAsync(register.UserName);
        if (findUser is not null)
        {
            throw new UserNameAlreadyInUseException();
        }
        
        var user = register.ToUser();

        var result = await _userManager.CreateAsync(user, register.Password);
        
        if (!result.Succeeded)
        {
            return BadRequest("Error creating an account");
        }

        var result2 = await _userManager.AddToRoleAsync(user, ROLES_CONSTANTS.ROLES.EMPLOYEE);
        
        if (!result2.Succeeded)
        {
            return BadRequest("Error assigning role");
        }
        
        return user.ToLoggedInUserDto(await _tokenService.CreateToken(user));
    }

    /// <summary>
    ///     Gets the list of users for manager
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllUsers")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MANAGER)]
    public async Task<List<UserDto>> GetAllUsers()
    {
       return await _userManager.Users.Select(u => u.ToUserDto()).ToListAsync();
    }
}