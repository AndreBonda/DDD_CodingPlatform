namespace CodingPlatform.Domain.Interfaces.Repositories;

public interface IRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<bool> ExistAsync(long id);
    Task<TEntity> GetByIdAsync(long id);
    Task<TEntity> InsertAsync(TEntity entity);
    Task DeleteAsync(long id);
    Task<TEntity> UpdateAsync(TEntity entity);
}