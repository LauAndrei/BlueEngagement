using Core.Dtos.ProofDto;
using Core.Dtos.TakenQuestDto;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface ITakenQuestService
{
    public Task<List<TakenQuestDto>> GetAllTakenQuestForUser(int userId);

    public Task<TakenQuest?> GetTakenQuestById(int id);
    
    public Task<bool> CreateTakenQuest(int questId, int userId);
    
    public Task<bool> DeleteTakenQuest(int takenQuestId);

    public Task<bool> CheckIfUserCanAcceptQuest(int userId, int questId);

    public Task<bool> CompleteTakenQuest(int userId, ProofDto proofDto, TakenQuest takenQuest);
}