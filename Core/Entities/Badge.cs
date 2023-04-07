namespace Core.Entities;

public class Badge : BaseEntity
{
    public string Description { get; set; }
    
    // for the badge (medal) image
    public string? PictureUrl { get; set; }

    public virtual List<User> Users { get; set; } = new();
}