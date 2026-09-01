using Hotels.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Hotels.Domain.Contracts
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenaricRepository<TEntity, TKey> GenerateRepo<TEntity, TKey>()
            where TEntity: BaseEntity<TKey>
            where TKey: IEquatable<TKey>;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task CompleteAsync();
    }
}
