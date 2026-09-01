using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.BaseEntities;
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

        public async Task<ICollection<TEntity>> GetAllAsync(bool asNoTracking = false)
        {
            if(asNoTracking) return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            return await _dbContext.Set<TEntity>().ToListAsync();
        }
        public async Task<TEntity?> GetAsync(TKey primaryKey, bool asNoTracking = false)
        {
            if(asNoTracking) return await _dbContext.Set<TEntity>().AsNoTracking().Where(e => e.Id.Equals(primaryKey)).FirstOrDefaultAsync();
            return await _dbContext.Set<TEntity>().Where(e => e.Id.Equals(primaryKey)).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQuery() => _dbContext.Set<TEntity>();

        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public void UpdateRange(ICollection<TEntity> entities) => _dbContext.Set<TEntity>().UpdateRange(entities);
    }
}
