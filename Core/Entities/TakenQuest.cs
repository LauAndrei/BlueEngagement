namespace Core.Entities;

public class TakenQuest : BaseEntity
{
    public User Owner { get; set; }
    public int OwnerId { get; set; }
    
    public Quest Quest { get; set; }
    public int QuestId { get; set; }

    public QuestStatus Status { get; set; } = QuestStatus.Accepted;

    public TakenQuest(int ownerId, int questId)
    {
        OwnerId = ownerId;
        QuestId = questId;
    }
}