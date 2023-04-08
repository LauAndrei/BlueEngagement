using Core.Dtos.ProofDto;
using Core.Dtos.QuestDtos;
using Core.Entities;

namespace Core.Interfaces.ServiceInterfaces;

public interface IQuestService
{
    public Task<List<QuestDto>> GetAllQuests();

    public Task<FullQuestDetailsDto?> GetQuestDetails(int questId);

    public Task<QuestDto> PostNewQuest(NewQuestDto newQuestionDto, User author);

    public Task<bool> DeleteQuest(int id);

    public Task<bool> CheckIfUserIsQuestsAuthor(int userId, int questId);

    public Task<Quest> GetQuestRewardAndCapacity(int questId);
}