namespace Hotels.Domain.SpecificationPattern
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> GenerateQuery<TEntity>(IQueryable<TEntity> query, ISpecification<TEntity> specification)
        {
            var startPoit = query;
            if(specification is not null)
            {
                if(specification.Critria is not null)
                {
                    startPoit = startPoit.Where(specification.Critria);
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
