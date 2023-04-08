namespace Core.Dtos.QuestDtos;

public class FullQuestDetailsDto
{
    public int Id { get; set; }
    
    public string Description { get; set; }
    
    public int Reward { get; set; }
    
    public int RewardsLeft { get; set; }
    
    public string OwnerUsername { get; set; } 
    
    public int NumberOfCompletions { get; set; }
}