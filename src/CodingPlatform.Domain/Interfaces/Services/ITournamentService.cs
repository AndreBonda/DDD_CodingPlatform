namespace CodingPlatform.Domain.Interfaces.Services;

public interface ITournamentService
{
    Task<Tournament> GetTournamentByIdAsync(long tournamentId);
    Task<Tournament> Create(string tournamentName, int maxParticipants, long userId);
    Task<IEnumerable<Tournament>> GetTournaments(string tournamentName);
    Task SubscribeUserRefactor(long tournamentId, long userId);
}