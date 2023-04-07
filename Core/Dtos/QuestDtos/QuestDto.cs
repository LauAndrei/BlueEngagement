namespace Core.Dtos.QuestDtos;

public class QuestDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int Reward { get; set; }
    public string OwnerUsername { get; set; }
    
    public DateTime DatePosted { get; set; }
}