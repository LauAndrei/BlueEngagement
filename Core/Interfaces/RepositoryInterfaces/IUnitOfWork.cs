namespace Core.Interfaces.RepositoryInterfaces;

public interface IUnitOfWork
{
    IBadgeRepository BadgeRepository { get; set; }
    IProofRepository ProofRepository { get; set; }
    IQuestRepository QuestRepository { get; set; }
    ITakenQuestRepository TakenQuestRepository { get; set; }

    Task<bool> SaveChangesAsync();
}