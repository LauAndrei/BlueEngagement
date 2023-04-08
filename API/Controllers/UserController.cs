using API.Extensions.ClaimsExtensions;
using Core.Constants;
using Core.Dtos.UserDtos;
using Core.Entities;
using Core.EntityExtensions.UserExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UserController(UserManager<User> userManager)
    {
        _userManager = userManager;
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

    /// <summary>
    ///     
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetLeaderboard")]
    [Authorize]
    public async Task<List<UserDto>> GetLeaderboard()
    {
        return await _userManager.Users
            .OrderByDescending(u => u.Score)
            .Select(u => u.ToUserDto())
            .Take(50)
            .ToListAsync();
    }
}