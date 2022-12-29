namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface ILeaderboardRepository
{
    Task<Leaderboard> GetLeaderboard(long tournamentId);
}