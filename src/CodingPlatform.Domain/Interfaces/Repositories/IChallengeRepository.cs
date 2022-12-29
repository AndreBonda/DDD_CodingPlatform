using CodingPlatform.Domain;

namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IChallengeRepository : IRepository<Challenge>
{
    Task<IEnumerable<Challenge>> GetChallengesByUser(long userId, bool onlyActive);
}