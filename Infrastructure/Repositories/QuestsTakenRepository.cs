using Core.Entities;

namespace Infrastructure.Repositories;

public class QuestsTakenRepository : GenericRepository<TakenQuest>
{
    public QuestsTakenRepository(DatabaseContext context) : base(context)
    {
    }
}