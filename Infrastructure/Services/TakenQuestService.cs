using Core.Dtos.ProofDto;
using Core.Dtos.TakenQuestDto;
using Core.Entities;
using Core.EntityExtensions.ProofExtensions;
using Core.EntityExtensions.TakenQuestExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TakenQuestService : ITakenQuestService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;

    public TakenQuestService(IUnitOfWork unitOfWork, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
    }

    public async Task<List<TakenQuestDto>> GetAllTakenQuestForUser(int userId)
    {
        return await _unitOfWork.TakenQuestRepository
            .GetAll()
            .Include(tq => tq.Quest)
            .Select(tq => tq.ToTakenQuestDto())
            .ToListAsync();
    }

    public async Task<TakenQuest?> GetTakenQuestById(int id)
    {
        return await _unitOfWork.TakenQuestRepository
            .GetAll()
            .Include(tq => tq.Quest)
            .Where(tq => tq.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<bool> CreateTakenQuest(int questId, int userId)
    {
        var questToAdd = new TakenQuest(userId, questId);
        
        await _unitOfWork.TakenQuestRepository.AddAsync(questToAdd);

        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> DeleteTakenQuest(int takenQuestId)
    {
        await _unitOfWork.TakenQuestRepository.RemoveByIdAsync(takenQuestId);

        return await _unitOfWork.SaveChangesAsync();
    }

    /// <summary>
    ///     A user can accept a quest only if he hasn't accepted it already
    /// </summary>
    /// <param name="questId"></param>
    /// <param name="userId"></param>
    /// <returns>A boolean indicating if the user can or cannot accept the specific quest</returns>
    public async Task<bool> CheckIfUserCanAcceptQuest(int userId, int questId)
    {
        var foundTakenQuest = await _unitOfWork.TakenQuestRepository
            .GetAll()
            .Where(tq => tq.QuestId == questId && tq.OwnerId == userId)
            .FirstOrDefaultAsync();

        return foundTakenQuest is null;
    }
    
    public async Task<bool> CompleteTakenQuest(int userId, ProofDto proofDto, TakenQuest takenQuest)
    {
        var proof = proofDto.ToProof(takenQuest.Quest.Id, userId);

        await _unitOfWork.ProofRepository.AddAsync(proof);

        var user = await _userManager.FindByIdAsync(userId.ToString());

        user.Score += takenQuest.Quest.Reward;
        
        takenQuest.Quest.Capacity--;

        takenQuest.Status = QuestStatus.Completed;
        
        return await _unitOfWork.SaveChangesAsync();
    }
}