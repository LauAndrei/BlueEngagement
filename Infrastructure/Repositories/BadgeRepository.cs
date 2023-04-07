using Core.Entities;

namespace Infrastructure.Repositories;

public class BadgeRepository : GenericRepository<Badge>
{
    public BadgeRepository(DatabaseContext context) : base(context)
    {
    }
}