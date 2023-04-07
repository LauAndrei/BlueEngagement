namespace Core.Entities;
/// <summary>
///     This class represents the proof that a user completed a task
/// </summary>
public class Proof : BaseEntity
{
    public string? Text { get; set; }
    
    public string? PictureUrl { get; set; }
    
    public Quest Quest { get; set; }
    public int QuestId { get; set; }
    
    public User Owner { get; set; }
    public int OwnerId { get; set; }
    
    public DateTime DatePosted { get; set; }
}