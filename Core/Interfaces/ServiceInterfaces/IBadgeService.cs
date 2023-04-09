using Core.Dtos.BadgeDtos;

namespace Core.Interfaces.ServiceInterfaces;

public interface IBadgeService
{
    public Task<List<BadgeDto>> GetAllBadges();

    public Task<BadgeDto?> CreateBadge(BadgeDto badgeDto);

    public Task<bool> DeleteBadge(int badgeId);
}