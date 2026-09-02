using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Domain.SpecificationPattern;
using Hotels.Infrastructure.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Hotels.Infrastructure.Persistence.GenaricRepo
{
    public class GenaricRepository<TEntity, TKey>(ApplicationDbContext _dbContext) : IGenaricRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);

        public async Task AddRangeAsync(ICollection<TEntity> entities) => await _dbContext.Set<TEntity>().AddRangeAsync(entities);

        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        public void DeleteRange(ICollection<TEntity> entities) => _dbContext.Set<TEntity>().RemoveRange(entities);

        public async Task<ICollection<TEntity>> GetAllAsync(ISpecification<TEntity> specification, bool asNoTracking = false)
            => await (asNoTracking ?  SpecificationEvaluator.GenerateQuery<TEntity>(_dbContext.Set<TEntity>(), specification)
                .AsNoTracking().ToListAsync() : SpecificationEvaluator.GenerateQuery<TEntity>(_dbContext.Set<TEntity>(), specification).ToListAsync());
        public async Task<TEntity?> GetAsync(ISpecification<TEntity> specification, bool asNoTracking = false)
            => await (asNoTracking ? SpecificationEvaluator.GenerateQuery<TEntity>(_dbContext.Set<TEntity>(), specification)
                .AsNoTracking().FirstOrDefaultAsync() : SpecificationEvaluator.GenerateQuery<TEntity>(_dbContext.Set<TEntity>(), specification).FirstOrDefaultAsync());

        public IQueryable<TEntity> GetQuery(ISpecification<TEntity> specification) => SpecificationEvaluator.GenerateQuery<TEntity>(_dbContext.Set<TEntity>(), specification);

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public void UpdateRange(ICollection<TEntity> entities) => _dbContext.Set<TEntity>().UpdateRange(entities);
    }
}
