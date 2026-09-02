using Hotels.Domain.Contracts;
using Hotels.Domain.Entities.BaseEntities;
using Hotels.Infrastructure.Persistence.Data.Contexts;
using Hotels.Infrastructure.Persistence.GenaricRepo;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Concurrent;

namespace Hotels.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
    {
        private IDbContextTransaction? _transaction;
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
        public async Task<IDbContextTransaction> BeginTransactionAsync() => _transaction = await context.Database.BeginTransactionAsync();
        public async Task CommitAsync()
        {
            if(_transaction is null) throw new InvalidOperationException("No Active Transactions");
            try
            {
                await CompleteAsync();
                await _transaction.CommitAsync();
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
        public async Task<int> CompleteAsync() => await context.SaveChangesAsync();
        public async ValueTask DisposeAsync()
        {
            if (_transaction is not null) await _transaction.DisposeAsync();
            await context.DisposeAsync();
        }
        public IGenaricRepository<TEntity, TKey> GenerateRepo<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
            => (IGenaricRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name!, _ => new GenaricRepository<TEntity, TKey>(context));
        public async Task RollbackAsync()
        {
            if (_transaction is null) throw new InvalidOperationException("No Active Transaction");
            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
