using Hotels.Domain.Entities.BaseEntities;

namespace Hotels.Domain.Contracts
{
    public interface IGenaricRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetQuery();
        Task<ICollection<TEntity>> GetAllWithNoTrackingAsync();
        Task<ICollection<TEntity>> GetAllWithTrackingAsync();
        Task<TEntity?> GetAsync(TKey primaryKey);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);

    }
}
