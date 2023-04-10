using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.QuestDtos;

public class NewQuestDto
{
    [Required]
    public string Title { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value larger than 1")]
    public int Reward { get; set; }
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a value larger than 1")]
    public int Capacity { get; set; }
}