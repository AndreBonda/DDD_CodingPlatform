using CodingPlatform.Domain;
using CodingPlatform.Domain.Interfaces.Repositories;
using CodingPlatform.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CodingPlatform.Infrastructure.Repositories;

public class TournamentRepository : BaseRepository<Tournament>, ITournamentRepository
{
    public TournamentRepository(AppDbContext dbCtx) : base(dbCtx)
    {
    }

    //TODO: indeciso. Non passo dal costruttore. Prendere in carico la costruzione dell'oggetto?
    public override async Task<Tournament> GetByIdAsync(long id) =>
        await _dbCtx.Tournaments
        .StandardInclude()
        .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<IEnumerable<Tournament>> GetFilteredAsync(string tournamentName)
    {
        //f ??= new TournamentSearch();

        IQueryable<Tournament> query = _dbCtx.Tournaments
            .StandardInclude();

        if (!string.IsNullOrWhiteSpace(tournamentName))
            query = query.Where(t => t.Name.ToLower().Contains(tournamentName.ToLower()));

        return await query.ToListAsync();
    }

    public async Task<bool> TournamentNameExist(string name)
    {
        if (string.IsNullOrEmpty(name)) return false;

        return await _dbCtx.Tournaments
            .AnyAsync(t => t.Name.ToLower() == name.ToLower());
    }

    public async Task<Tournament> GetTournamentByChallengeAsync(long challengeId) =>
        await _dbCtx.Tournaments
            .StandardInclude()
            .FirstOrDefaultAsync(t => t.Challenges.Any(c => c.Id == challengeId));

    public async Task<IEnumerable<Tournament>> GetSubscribedTournamentsByUserAsync(long userId)
    {
        IQueryable<Tournament> query = _dbCtx.Tournaments
            .StandardInclude()
            .Where(t => t.SubscribedUser.Any(su => su.User.Id == userId));

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Tournament>> GetTournamentsByAdmin(long userId) =>
        await _dbCtx.Tournaments
        .StandardInclude()
        .Where(t => t.Admin.Id == userId)
        .OrderByDescending(t => t.CreateDate)
        .ToListAsync();
}