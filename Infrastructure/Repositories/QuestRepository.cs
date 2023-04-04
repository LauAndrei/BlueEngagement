using Core.Entities;

namespace Infrastructure.Repositories;

public class QuestRepository : GenericRepository<Quest>
{
    public QuestRepository(DatabaseContext context) : base(context)
    {
        
    }
}