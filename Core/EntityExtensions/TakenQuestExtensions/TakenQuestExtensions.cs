using Core.Dtos.TakenQuestDtos;
using Core.Entities;

namespace Core.EntityExtensions.TakenQuestExtensions;

public static class TakenQuestExtensions
{
    public static TakenQuestDto ToTakenQuestDto(this TakenQuest takenQuest)
    {
        return new TakenQuestDto
        {
            Id = takenQuest.Id,
            Description = takenQuest.Quest.Description,
            Reward = takenQuest.Quest.Reward,
            QuestId = takenQuest.QuestId,
            Status = Enum.GetName(takenQuest.Status)
        };
    }
}