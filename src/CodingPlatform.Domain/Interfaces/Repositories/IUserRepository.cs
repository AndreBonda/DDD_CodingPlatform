namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmailAsync(string email);
    Task<bool> ExistUserByEmailAsync(string email);
    Task<User> GetUserByUsernameAsync(string username);
    Task<bool> ExistUserByUsernameAsync(string username);
}