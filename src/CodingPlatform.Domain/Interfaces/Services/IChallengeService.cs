namespace CodingPlatform.Domain.Interfaces.Services;

public interface IChallengeService
{
    Task<Challenge> CreateChallenge(long tournamentId, long userId, string title, string description, int hours, IEnumerable<string> tips);
    Task<IEnumerable<Challenge>> GetChallengesByUserAsync(long userId, bool onlyActive);

    Task<Submission> StartChallengeAsync(long challengeId, long userId);
    Task<Submission> GetSubmissionStatusAsync(long challengeId, long userId);
    /// <summary>
    /// Requests for a new tip.
    /// </summary>
    /// <param name="challengeId"></param>
    /// <param name="userId"></param>
    /// <returns>Returns the new tip</returns>
    Task<Tip> RequestNewTipAsync(long challengeId, long userId);
    Task<Submission> EndChallengeAsync(long challengeId, string content, long userId);
}