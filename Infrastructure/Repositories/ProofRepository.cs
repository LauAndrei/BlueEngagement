using Core.Entities;

namespace Infrastructure.Repositories;

public class ProofRepository : GenericRepository<Proof>
{
    public ProofRepository(DatabaseContext context) : base(context)
    {
    }
}