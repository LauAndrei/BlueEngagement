using Core.Dtos.QuestDtos;
using Core.Entities;

namespace Core.EntityExtensions.QuestExtensions;

public static class QuestExtensions
{
    public static QuestDto ToQuestDto(this Quest quest)
    {
        return new QuestDto
        {
            Id = quest.Id,
            Description = quest.Description,
            Reward = quest.Reward,
            OwnerUsername = quest.Owner.UserName,
            DatePosted = quest.DatePosted
        };
    }

    public static Quest ToQuest(this NewQuestDto newQuestDto, int authorId)
    {
        return new Quest
        {
            Id = 0,
            Description = newQuestDto.Description,
            Reward = newQuestDto.Reward,
            Capacity = newQuestDto.Capacity,
            OwnerId = authorId,
            DatePosted = DateTime.Now
        };
    }

    public static FullQuestDetailsDto ToFullDetailsQuest(this Quest quest)
    {
        return new FullQuestDetailsDto
        {
            Id = quest.Id,
            Description = quest.Description,
            Reward = quest.Reward,
            MaxRewards = quest.Capacity,
            OwnerUsername = quest.Owner.UserName,
            NumberOfCompletions = quest.Proofs.Count
        };
    }
}