using System.Linq.Expressions;

namespace DomainLogic.Queries
{
    /// <summary>
    ///     Allows constructing a query string.
    /// </summary>
    public static class QueryBuilder
    {
        #region Internals

        private static Expression<Func<TEntity, bool>> ComposePredicate<TEntity>(
            string name, object? value, Func<Expression, Expression, BinaryExpression> fnCompare)
        {
            var type = Expression.Parameter(typeof(TEntity));
            var left = Expression.Property(type, name);
            var right = Expression.Constant(value);

            var body = fnCompare(left, right);

            var predicate = Expression.Lambda<Func<TEntity, bool>>(body, type);

            return predicate;
        }

        private static Expression<Func<TEntity, bool>> ComposeEqualPredicate<TEntity>(
            string name, object? value)
        {
            return ComposePredicate<TEntity>(name, value, Expression.Equal);
        }

        private static Expression<Func<TEntity, bool>> ComposeNotEqualPredicate<TEntity>(
            string name, object? value)
        {
            return ComposePredicate<TEntity>(name, value, Expression.NotEqual);
        }

        #endregion

        /// <summary>
        ///     Filters a sequence of the TEntity based on composed predicate.
        /// </summary>
        /// <typeparam name="TEntity">
        ///     The domain type the repository manages.
        /// </typeparam>
        /// <param name="query">
        ///     An IQueryable to filter.
        /// </param>
        /// <param name="propName">
        ///     The name of the member to validate.
        /// </param>
        /// <param name="propValue">
        ///     The value of the member to validate.
        /// </param>
        /// <param name="idName">
        ///     The name of the object ID to validate.
        /// </param>
        /// <param name="idValue">
        ///     The value of the object ID to validate.
        /// </param>
        /// <returns>
        ///     An IQueryable that contains elements from the input sequence that 
        ///     satisfy the condition specified by composed predicate.
        /// </returns>
        public static IQueryable<TEntity> IsUnique<TEntity>(
            this IQueryable<TEntity> query, string propName, object? propValue,
            string idName, object? idValue)
        {
            var predicate = ComposeEqualPredicate<TEntity>(propName, propValue);

            query = query.Where(predicate);

            if (idValue is not null)
            { 
                var idType = idValue?.GetType();
            
                if (idType is not null && !Convert.ChangeType(0, idType).Equals(idValue))
                {
                    predicate = ComposeNotEqualPredicate<TEntity>(idName, idValue);

                    query = query.Where(predicate);
                }
            }

            return query;
        }
    }
}
