using static CodingPlatform.Domain.Leaderboard;

namespace CodingPlatform.Domain.Interfaces.Services;

public interface ILeaderboardService
{
    Task<Dictionary<int, Placement>> GetLeaderboardPlacements(long tournamentId, Sorting sortingType, bool sortingAsc = false);
}