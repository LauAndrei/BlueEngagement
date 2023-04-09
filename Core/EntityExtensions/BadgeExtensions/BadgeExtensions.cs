using Core.Dtos.BadgeDtos;
using Core.Entities;

namespace Core.EntityExtensions.BadgeExtensions;

public static class BadgeExtensions
{
    public static BadgeDto? ToBadgeDto(this Badge badge)
    {
        return new BadgeDto
        {
            Id = badge.Id,
            Description = badge.Description,
            PictureUrl = badge.PictureUrl
        };
    }

    public static Badge ToNewBadge(this BadgeDto badgeDto)
    {
        return new Badge
        {
            Id = 0,
            Description = badgeDto.Description,
            PictureUrl = badgeDto.PictureUrl,
        };
    }
}