using Core.Dtos.ProofDto;
using Core.Dtos.QuestDtos;
using Core.Entities;
using Core.EntityExtensions.ProofExtensions;
using Core.EntityExtensions.QuestExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuestService : IQuestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public QuestService(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }
    
    /// <summary>
    ///     Method tested;
    ///     Use this for testing purposes / demo only
    ///  - In production a better method, with pagination and filters will be used -
    /// </summary>
    /// <returns></returns>
    public async Task<List<QuestDto>> GetAllQuests()
    {
        return await _unitOfWork.QuestRepository.GetAll()
            .Include(q => q.Owner)
            .Select(q => q.ToQuestDto())
            .ToListAsync();
    }
    
    /// <summary>
    ///     Gets the quests posted by an user.
    ///     Here the result should also be paginated, but for demo purposes
    ///   should be fine
    /// </summary>
    /// <param name="ownerUsername"></param>
    /// <returns></returns>
    public async Task<List<QuestDto>> GetQuestsFromUser(string ownerUsername)
    {
        return await _unitOfWork.QuestRepository.GetAll()
            .Include(q => q.Owner)
            .Where(q => q.Owner.UserName == ownerUsername)
            .Select(q => q.ToQuestDto())
            .ToListAsync();
    }

    /// <summary>
    ///     Get the status of the quest for a user.
    /// </summary>
    /// <param name="questId">The id of the quest</param>
    /// <param name="userId">The id of the logged in user</param>
    /// <returns>The string representing the value of the quest status</returns>
    public async Task<string> GetQuestStatusForUser(int questId, int userId)
    {
        var foundTakenQuest = await _unitOfWork.TakenQuestRepository
            .GetAll()
            .Where(tq => tq.OwnerId == userId && tq.QuestId == questId)
            .FirstOrDefaultAsync();

        if (foundTakenQuest is null)
        {
            return Enum.GetName(QuestStatus.NotAccepted);
        }

        return Enum.GetName(foundTakenQuest.Status);
    }


    /// <summary>
    ///     Gets the details of the quest with id questId
    /// </summary>
    /// <param name="questId">The id of the quest's details to retrieve</param>
    /// <returns></returns>
    public async Task<FullQuestDetailsDto?> GetQuestDetails(int questId)
    {
        return await _unitOfWork.QuestRepository
            .GetAll()
            .Include(q => q.Proofs)
            .Include(q => q.Owner)
            .Where(q => q.Id == questId)
            .Select(q => q.ToFullDetailsQuest())
            .FirstOrDefaultAsync();
    }

    /// <summary>
    ///     Converts newQuestDto to quest and then adds it to the database;
    ///     If successfully added, subtract the user score needed in order to
    ///   reward all the users who completed the task
    ///     At the end, try to save the changes. If SaveChanges will fail, due to
    ///   Unit of work pattern, all the changes will be rolled back.
    /// </summary>
    /// <param name="newQuestDto">Object containing the newQuestion details</param>
    /// <param name="author">Author retrieved from controller</param>
    /// <returns>The new quest converted to dto</returns>
    public async Task<QuestDto> PostNewQuest(NewQuestDto newQuestDto, User author)
    {
        var questToAdd = newQuestDto.ToQuest(author.Id);

        var addedQuest = await _unitOfWork.QuestRepository.AddAsync(questToAdd);
        
        author.Score -= newQuestDto.Reward * newQuestDto.Capacity;

        await _unitOfWork.SaveChangesAsync();

        return addedQuest.Entity.ToQuestDto(author.UserName);
    }

    /// <summary>
    ///     Asynchronously calls the method remove by id async implemented by the
    ///   generic repository and then saves changes
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A boolean indicating if the operation was successfully or not</returns>
    public async Task<bool> DeleteQuest(int id)
    {
        await _unitOfWork.QuestRepository.RemoveByIdAsync(id);
        return await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    ///     Gets the quest with the provided Id and returns true
    ///   if its owner has id equal to the userId
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="questId"></param>
    /// <returns>
    ///     A boolean flag indicating if the owner of the quest is the user who made the request
    /// </returns>
    public async Task<bool> CheckIfUserIsQuestsAuthor(int userId, int questId)
    {
        var quest = await _unitOfWork.QuestRepository.FindAsync(questId);

        return quest?.OwnerId == userId;
    }

    /// <summary>
    ///     Gets the reward and the maximum number of rewards (capacity) for a quest
    ///   and returns them
    /// </summary>
    /// <param name="questId">The id of the quest to retrieve</param>
    /// <returns>An object containing the quest reward and its capacity (number of rewards left)</returns>
    public async Task<Quest> GetQuestRewardAndCapacity(int questId)
    {
        return await _unitOfWork.QuestRepository
            .GetAll()
            .Include(q => q.Proofs)
            .Where(q => q.Id == questId)
            .FirstAsync();
    }
}