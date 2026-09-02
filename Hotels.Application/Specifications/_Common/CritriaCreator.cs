using System.Linq.Expressions;

namespace Hotels.Application.Specifications._Common
{
    internal static class CritriaCreator
    {
        public static Expression<Func<TEntity, bool>>? CreateCriteria<TEntity>(params Expression<Func<TEntity, bool>>[]? expressions)
            where TEntity : class
        {
            if(expressions is null || expressions.Count() == 0) return null;
            var validExprs = expressions.Where(e => e is not null).ToList();
            if (validExprs.Count == 0) return null;
            if (validExprs.Count == 1) return validExprs[0];
            var parameter = Expression.Parameter(typeof(TEntity), "E");
            var invocationExprs = validExprs.Select(e => Expression.Invoke(e, parameter)).ToList();
            var accumelateExpers = Expression.AndAlso(invocationExprs[0], invocationExprs[1]);
            for ( var i = 2; i < invocationExprs.Count; i++ )
                 accumelateExpers = Expression.AndAlso(accumelateExpers, invocationExprs[i]);
            return Expression.Lambda<Func<TEntity, bool>>(accumelateExpers, parameter);
        }
    }
}
