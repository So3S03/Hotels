using Hotels.Domain.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
namespace Hotels.Domain.SpecificationPattern
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GenerateQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification)
            where TEntity : class
        {
            var startPoit = query;
            if(specification is not null)
            {
                if(specification.Critria is not null)
                {
                    startPoit = startPoit.Where(specification.Critria);
                }
                if(specification.Includes is not null && specification.Includes.Count > 0)
                {
                    startPoit = specification.Includes.Aggregate(startPoit, (currentQuery, include) => currentQuery.Include(include));
                }
                if(specification.OrderByAsc is not null && specification.OrderByDesc is null)
                {
                    startPoit = startPoit.OrderBy(specification.OrderByAsc);
                }
                else if(specification.OrderByAsc is null && specification.OrderByDesc is not null)
                {
                    startPoit = startPoit.OrderByDescending(specification.OrderByDesc);
                }
                if(specification.isPagination)
                {
                    startPoit = startPoit.Skip(specification.Skip).Take(specification.Take);
                }

            }
            return startPoit;
        }
    }
}
