using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.SpecificationPattern;

namespace Hotels.Domain.Contracts
{
    public interface IGenaricRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification);
        Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification, bool asNoTracking = false);
        Task<TEntity?> GetAsync(ISpecification<TEntity> specification, bool asNoTracking = false);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(ICollection<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(ICollection<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);

    }
}
