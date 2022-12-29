namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface ITournamentRepository : IRepository<Tournament>
{
    Task<IEnumerable<Tournament>> GetFilteredAsync(string tournamentName);
    Task<bool> TournamentNameExist(string name);
    /// <summary>
    /// Given a user id, returns all subscribed tournaments by the user.
    /// </summary>
    Task<IEnumerable<Tournament>> GetSubscribedTournamentsByUserAsync(long userId);
    /// <summary>
    /// Given a challenge id, returns the related tournament.
    /// </summary>
    Task<Tournament> GetTournamentByChallengeAsync(long challengeId);
}