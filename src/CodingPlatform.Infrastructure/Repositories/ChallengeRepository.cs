using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Domain;
using Microsoft.EntityFrameworkCore;
using CodingPlatform.Infrastructure.Extensions;

namespace CodingPlatform.Infrastructure.Repositories;

public class ChallengeRepository : BaseRepository<Challenge>, IChallengeRepository
{
    public ChallengeRepository(AppDbContext dbCtx) : base(dbCtx)
    {
    }

    public override async Task<Challenge> GetByIdAsync(long id) => 
        await _dbCtx.Challenges
        .StandardInclude()
        .FirstOrDefaultAsync(c => c.Id == id);

    public async Task<IEnumerable<Challenge>> GetChallengesByUser(long userId, bool onlyActive)
    {
        var challenges = await _dbCtx.Tournaments
            .StandardInclude()
            .Where(t => t.SubscribedUser.Any(s => s.User.Id == userId))
            .SelectMany(t => t.Challenges)
            .ToListAsync();

        if (onlyActive)
            challenges = challenges.Where(c => c.IsActive()).ToList();

        return challenges;
    }
}