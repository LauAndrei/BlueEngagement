using Core.Dtos.ProofDto;
using Core.Dtos.QuestDtos;
using Core.Dtos.TakenQuestDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITakenQuestService
{
    public Task<List<QuestDto>> GetAllAcceptedQuestForUser(int userId);

    public Task<TakenQuest?> GetTakenQuestById(int id);

    public Task<TakenQuest?> FindTakenQuestByUserIdAndQuestId(int userId, int questId);
    
    public Task<bool> CreateTakenQuest(int questId, int userId);
    
    public Task<bool> DeleteTakenQuest(int takenQuestId);

    public Task<bool> CheckIfUserCanAcceptQuest(int userId, int questId);

    public Task<List<QuestDto>> GetAllCompletedQuestsForUser(int userId);

    public Task<bool> CompleteTakenQuest(int userId, ProofDto proofDto, TakenQuest takenQuest);
}