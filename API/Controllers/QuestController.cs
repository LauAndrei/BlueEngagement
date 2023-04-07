using API.Extensions.ClaimsExtensions;
using Core.Constants;
using Core.Dtos.QuestDtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuestController : ControllerBase
{
    private readonly IQuestService _questService;
    private readonly UserManager<User> _userManager;

    public QuestController(IQuestService questService, UserManager<User> userManager)
    {
        _questService = questService;
        _userManager = userManager;
    }
    
    /// <summary>
    ///     Method tested;
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("GetAllQuests")]
    public Task<List<QuestDto>> GetAllQuests()
    {
        return _questService.GetAllQuests();
    }

    /// <summary>
    ///     Method tested;
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetQuestDetails/{id:int}")]
    public Task<FullQuestDetailsDto?> GetQuestDetails(int id)
    {
        return _questService.GetQuestDetails(id);
    }

    /// <summary>
    ///     Method Tested;
    ///     This method first check the input so it's not null,
    ///     then gets the author (user) who made the request.
    ///     Checks if the user has enough points and then
    ///   asynchronously calls the method from the service.
    /// </summary>
    /// <param name="newQuestDto"></param>
    /// <returns>A boolean indicating if the operation was successfully or not</returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="NotFoundException"></exception>
    /// <exception cref="NotEnoughPointsException"></exception>
    [HttpPost]
    [Route("PostNewQuest")]
    public async Task<bool> PostNewQuest(NewQuestDto newQuestDto)
    {
        if (newQuestDto is null)
        {
            throw new ArgumentNullException(nameof(newQuestDto));
        }

        var authorName = HttpContext.User.Identity.Name;

        if (authorName is null)
        {
            throw new NotFoundException("Author Username not found");
        }

        var author = await _userManager.FindByNameAsync(authorName);

        var pointsNeeded = newQuestDto.Reward * newQuestDto.Capacity;

        if (author.Score < pointsNeeded)
        {
            throw new NotEnoughPointsException();
        }

        return await _questService.PostNewQuest(newQuestDto, author);
    }

    /// <summary>
    ///     Method tested;
    ///     Async checks if the user is the author or a manager and if yes,
    ///  calls the delete method from the service.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    [HttpDelete]
    [Route("DeleteQuest/{id:int}")]
    public async Task<ActionResult<bool>> DeleteQuest(int id)
    {
        var loggedInUserId = User.GetUserId();

        var isAuthor = await _questService.CheckIfUserIsQuestsAuthor(loggedInUserId, id);

        if (isAuthor)
        {
            return await _questService.DeleteQuest(id);
        }

        var user = await _userManager.FindByIdAsync(loggedInUserId.ToString());

        if (user is null)
        {
            throw new NotFoundException("User was not found!");
        }

        var isManager = await _userManager.IsInRoleAsync(user, ROLES_CONSTANTS.ROLES.MANAGER);

        if (isManager)
        {
            return await _questService.DeleteQuest(id);
        }
        
        return Unauthorized();
    }
}