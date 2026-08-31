using Hotels.Domain.Entities.BaseEntities;

namespace Hotels.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IGenaricRepository<TEntity, TKey> GenerateRepo<TEntity, TKey>()
            where TEntity: BaseEntity<TKey>
            where TKey: IEquatable<TKey>;
        public Task CompleteAsync();
    }
}
