using Hotels.Domain.SpecificationPattern;
using System.Linq.Expressions;

namespace Hotels.Application.Specifications.Base
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Critria { get; private set; }

        public int Skip { get; private set; }

        public int Take { get; private set; }

        public bool isPagination { get; private set; }

        public BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<TEntity, bool>>? criteria)
        {
            Critria = criteria;
        }

        protected void Pagination(int pageNum, int pageSize)
        {
            isPagination = true;
            Take = pageSize;
            Skip = (pageNum - 1) * pageSize;
        }
    }
}
