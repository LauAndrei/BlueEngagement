using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.BadgeDtos;

public class BadgeDto
{
    public int Id { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    public string PictureUrl { get; set; }
}