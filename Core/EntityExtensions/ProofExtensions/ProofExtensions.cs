using Core.Dtos.ProofDto;
using Core.Entities;

namespace Core.EntityExtensions.ProofExtensions;

public static class ProofExtensions
{
    public static Proof ToProof(this ProofDto proofDto, int questId, int userId)
    {
        return new Proof
        {
            Text = proofDto.Description,
            PictureUrl = proofDto.PictureUrl,
            QuestId = questId,
            OwnerId = userId,
            DatePosted = DateTime.Now
        };
    }
}