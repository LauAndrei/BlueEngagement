namespace Core.Dtos.TakenQuestDtos;

public class TakenQuestDto
{
    public int Id { get; set; }

    public string Description { get; set; }
    
    public int Reward { get; set; }

    public int QuestId { get; set; }
    
    public string Status { get; set; }
}