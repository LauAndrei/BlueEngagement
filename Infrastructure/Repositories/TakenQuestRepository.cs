using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class TakenQuestRepository : GenericRepository<TakenQuest>, ITakenQuestRepository
{
    public TakenQuestRepository(DatabaseContext context) : base(context)
    {
    }
}