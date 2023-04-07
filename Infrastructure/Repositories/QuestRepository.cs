using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class QuestRepository : GenericRepository<Quest>, IQuestRepository
{
    public QuestRepository(DatabaseContext context) : base(context)
    {
        
    }
}