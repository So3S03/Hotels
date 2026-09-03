using System.Linq.Expressions;

namespace Hotels.Domain.SpecificationPattern
{
    public interface ISpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Critria { get; }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; }
        public Expression<Func<TEntity, object>>? OrderByAsc { get; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; }
        public int Skip { get; }
        public int Take { get; }
        public bool isPagination { get; }
    }
}
