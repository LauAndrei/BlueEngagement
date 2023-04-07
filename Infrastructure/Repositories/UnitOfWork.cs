using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly DatabaseContext _context;
    
    public IBadgeRepository BadgeRepository { get; set; }
    public IProofRepository ProofRepository { get; set; }
    public IQuestRepository QuestRepository { get; set; }
    public ITakenQuestRepository TakenQuestRepository { get; set; }

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;

        BadgeRepository = new BadgeRepository(_context);
        ProofRepository = new ProofRepository(_context);
        QuestRepository = new QuestRepository(_context);
        TakenQuestRepository = new TakenQuestRepository(_context);
    }


    public void Dispose()
    {
        _context.Dispose();
    }
    
    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}