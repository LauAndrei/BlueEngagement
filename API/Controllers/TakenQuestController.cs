using API.Extensions.ClaimsExtensions;
using Core.Dtos.ProofDto;
using Core.Dtos.QuestDtos;
using Core.Dtos.TakenQuestDtos;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TakenQuestController : ControllerBase
{
    private readonly ITakenQuestService _takenQuestService;

    public TakenQuestController(ITakenQuestService takenQuestService)
    {
        _takenQuestService = takenQuestService;
    }

    /// <summary>
    ///     Method tested;
    ///     Gets all the quests a user has taken (accepted)
    /// </summary>
    /// <returns>
    ///     A list of TakenQuestDto representing the quests object a user has accepted / taken
    /// </returns>
    [HttpGet]
    [Route("GetAllAcceptedQuestsForUser")]
    public async Task<List<QuestDto>> GetAllAcceptedQuestsForUser()
    {
        var loggedInUserId = User.GetUserId();

        return await _takenQuestService.GetAllAcceptedQuestForUser(loggedInUserId);
    }

    /// <summary>
    ///     Method tested;
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("GetTakenQuestById/{id:int}")]
    public async Task<TakenQuest?> GetTakenQuestById(int id)
    {
        return await _takenQuestService.GetTakenQuestById(id);
    }

    /// <summary>
    ///     Method tested;
    ///     Search and returns if found a taken quest if found having as owner the logged in user id,
    ///     and having the quest the questId.
    /// </summary>
    /// <param name="questId"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("FindTakenQuestByUserIdAndQuestId/{questId}")]
    public async Task<TakenQuest?> FindTakenQuestByUserIdAndQuestId(int questId)
    {
        var loggedInUserId = User.GetUserId();

        return await _takenQuestService.FindTakenQuestByUserIdAndQuestId(loggedInUserId, questId);
    }

    /// <summary>
    ///     Method tested;
    ///     Checks if the user can or cannot accept a quest.
    ///     A user can accept a quest if it wasn't previously accepted by them.
    /// </summary>
    /// <param name="id">The quest id to accept</param>
    /// <returns>A boolean flag indicating if the user can or cannot accept the quest</returns>
    [HttpGet]
    [Route("CheckIfUserCanAcceptQuest/{id:int}")]
    public async Task<bool> CheckIfUserCanAcceptQuest(int id)
    {
        var loggedInUser = User.GetUserId();
        
        return await _takenQuestService.CheckIfUserCanAcceptQuest(loggedInUser, id);
    }

    /// <summary>
    ///     Method tested;
    ///     Gets the logged in user id and then calls createTakenQuest method
    ///   implemented in TakenQuestService
    /// </summary>
    /// <param name="id">The id of the quest to accept</param>
    /// <returns>A boolean indicating if the operation was successful or not</returns>
    [HttpPost]
    [Route("AcceptQuest/{id:int}")]
    public async Task<bool> AcceptQuest(int id)
    {
        // User.GetUserId() will throw an error if user is not found
        var loggedInUser = User.GetUserId();

        var canAccept = await _takenQuestService.CheckIfUserCanAcceptQuest(loggedInUser,id);

        if (!canAccept)
        {
            throw new QuestAlreadyAcceptedException("Quest already accepted or completed!");
        }

        return await _takenQuestService.CreateTakenQuest(id, loggedInUser);
    }

    /// <summary>
    ///     Method tested;
    ///     Gets the logged in user and deletes the taken / accepted quest by its id;
    /// </summary>
    /// <param name="id">The id of the taken quest to remove</param>
    /// <returns>A boolean flag indicating if the operation was successful or not</returns>
    [HttpDelete]
    [Route("DeleteTakenQuest/{id:int}")]
    public async Task<bool> DeleteTakenQuest(int id)
    {
        var loggedInUser = User.GetUserId();

        return await _takenQuestService.DeleteTakenQuest(id);
    }

    [HttpGet]
    [Route("GetAllCompletedQuestsForLoggedInUser")]
    public async Task<List<QuestDto>> GetAllCompletedQuestsForLoggedInUser()
    {
        var loggedInUser = User.GetUserId();

        return await _takenQuestService.GetAllCompletedQuestsForUser(loggedInUser);
    }
    
    /// <summary>
    ///     Method tested;
    ///     Gets the logged in user and the taken quest details.
    ///  If it isn't null nor completed, and if it didn't reach the
    ///  maximum number of rewards, call the CompleteTakenQuest from service;
    ///  Throw an error otherwise
    /// </summary>
    /// <param name="id">The id of the taken quest to complete</param>
    /// <param name="proofDto">The proof dto object containing proof's info</param>
    /// <returns></returns>
    /// <exception cref="NotFoundException">Occurs when taken quest was not found</exception>
    /// <exception cref="QuestAlreadyCompletedException">Occurs when the taken quest was already completed</exception>
    /// <exception cref="QuestMaxCapacityReachedException">
    ///     Occurs when the quest has already reached the maximum number of rewards
    /// </exception>
    [HttpPost]
    [Route("CompleteTakenQuest/{id:int}")]
    public async Task<bool> CompleteTakenQuest(int id, ProofDto proofDto)
    {
        var loggedInUserId = User.GetUserId();

        var takenQuest = await _takenQuestService.FindTakenQuestByUserIdAndQuestId(loggedInUserId, id);

        if (takenQuest is null)
        {
            throw new NotFoundException("Taken quest was not found!");
        }

        if (takenQuest.Status == QuestStatus.Completed)
        {
            throw new QuestAlreadyCompletedException();
        }

        if (takenQuest.Quest.Capacity <= 0)
        {
            throw new QuestMaxCapacityReachedException();
        }

        return await _takenQuestService.CompleteTakenQuest(loggedInUserId, proofDto, takenQuest);
    }
}