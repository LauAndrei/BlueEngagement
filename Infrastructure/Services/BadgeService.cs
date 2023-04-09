using Core.Dtos.BadgeDtos;
using Core.EntityExtensions.BadgeExtensions;
using Core.Interfaces.RepositoryInterfaces;
using Core.Interfaces.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class BadgeService : IBadgeService
{
    private readonly IUnitOfWork _unitOfWork;

    public BadgeService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<BadgeDto>> GetAllBadges()
    {
        return await _unitOfWork.BadgeRepository
            .GetAll()
            .Select(b => b.ToBadgeDto())
            .ToListAsync();
    }
    
    public async Task<BadgeDto?> CreateBadge(BadgeDto badgeDto)
    {
        var badgeToAdd = badgeDto.ToNewBadge();

        var addedBadge = (await _unitOfWork.BadgeRepository.AddAsync(badgeToAdd)).Entity;

        if (await _unitOfWork.SaveChangesAsync())
        {
            return addedBadge.ToBadgeDto();
        }

        return null;
    }

    public async Task<bool> DeleteBadge(int badgeId)
    {
        await _unitOfWork.BadgeRepository.RemoveByIdAsync(badgeId);

        return await _unitOfWork.SaveChangesAsync();
    }
}