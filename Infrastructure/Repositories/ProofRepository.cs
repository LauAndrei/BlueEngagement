using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class ProofRepository : GenericRepository<Proof>, IProofRepository
{
    public ProofRepository(DatabaseContext context) : base(context)
    {
    }
}