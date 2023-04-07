using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Score { get; set; } = 0;

    public virtual List<Quest> OwnedQuests { get; set; } = new();

    public virtual List<Proof> Proofs { get; set; } = new();

    public virtual List<TakenQuest> TakenQuests { get; set; } = new();

    public virtual List<Badge> Badges { get; set; } = new();
}