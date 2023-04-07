using Core.Dtos.QuestDtos;
using Core.Entities;
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
    ///     Use this for testing purposes only
    ///  - In production a better method will be used -
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
    /// <returns>True or false, representing if the operation was successfully or not</returns>
    public async Task<bool> PostNewQuest(NewQuestDto newQuestDto, User author)
    {
        var questToAdd = newQuestDto.ToQuest(author.Id);

        await _unitOfWork.QuestRepository.AddAsync(questToAdd);
        
        author.Score -= newQuestDto.Reward * newQuestDto.Capacity;

        return await _unitOfWork.SaveChangesAsync();
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
}