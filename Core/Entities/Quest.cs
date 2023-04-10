namespace Core.Entities;

public class Quest : BaseEntity
{
    public string Title { get; set; }
    
    public string Slug { get; set; }
    public string Description { get; set; }
    
    public int Reward { get; set; }
    
    public int Capacity { get; set; }
    
    public User Owner { get; set; }
    public int OwnerId { get; set; }
    
    public DateTime DatePosted { get; set; }

    public virtual List<Proof> Proofs { get; set; } = new();
}