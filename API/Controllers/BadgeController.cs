using Core.Constants;
using Core.Dtos.BadgeDtos;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BadgeController : ControllerBase
{
    private readonly IBadgeService _badgeService;

    public BadgeController(IBadgeService badgeService)
    {
        _badgeService = badgeService;
    }

    [HttpGet]
    [Route("GetAllBadges")]
    [Authorize]
    public async Task<List<BadgeDto>> GetAllBadges()
    {
        return await _badgeService.GetAllBadges();
    }

    /// <summary>
    ///     Method tested;
    ///     Checks if the badgeDto object is not null, and then calls the
    ///   createBadge method from IBadgeService
    /// </summary>
    /// <param name="badgeDto">The badge dto obj to add</param>
    /// <returns>The newly created badge object</returns>
    /// <exception cref="ArgumentNullException"></exception>
    [HttpPost]
    [Route("CreateBadge")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MANAGER)]
    public async Task<BadgeDto?> CreateBadge(BadgeDto badgeDto)
    {
        if (badgeDto is null)
        {
            throw new ArgumentNullException(nameof(badgeDto));
        }

        return await _badgeService.CreateBadge(badgeDto);
    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">The id of the badge to be deleted</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("DeleteBadge/{id:int}")]
    [Authorize(Roles = ROLES_CONSTANTS.ROLES.MANAGER)]
    public async Task<bool> DeleteBadge(int id)
    {
        return await _badgeService.DeleteBadge(id);
    }
}