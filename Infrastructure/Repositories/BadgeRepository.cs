using Core.Entities;
using Core.Interfaces.RepositoryInterfaces;

namespace Infrastructure.Repositories;

public class BadgeRepository : GenericRepository<Badge>, IBadgeRepository
{
    public BadgeRepository(DatabaseContext context) : base(context)
    {
    }
}