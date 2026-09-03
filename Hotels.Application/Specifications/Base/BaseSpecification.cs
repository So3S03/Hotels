using Hotels.Domain.SpecificationPattern;
using System.Linq.Expressions;

namespace Hotels.Application.Specifications.Base
{
    public class BaseSpecification<TEntity> : ISpecification<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Critria { get; private set; }
        public ICollection<Expression<Func<TEntity, object>>> Includes { get; } = [];
        public Expression<Func<TEntity, object>>? OrderByAsc { get; private set; }
        public Expression<Func<TEntity, object>>? OrderByDesc { get; private set; }

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

        protected void addIncludes(Expression<Func<TEntity, object>> include)
        {
            Includes.Add(include);
        }
        protected void setOrderBy(Expression<Func<TEntity, object>>? orderByExper, bool isAsc)
        {
            if (isAsc) OrderByAsc = orderByExper;
            else OrderByDesc = orderByExper;
        }
        protected void Pagination(int pageNum, int pageSize)
        {
            isPagination = true;
            Take = pageSize;
            Skip = (pageNum - 1) * pageSize;
        }
    }
}
