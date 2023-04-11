using System.ComponentModel.DataAnnotations;

namespace Core.Dtos.ProofDto;

public class ProofDto
{
    public string Description { get; set; }
    
    [Required]
    public string PictureUrl { get; set; }
}