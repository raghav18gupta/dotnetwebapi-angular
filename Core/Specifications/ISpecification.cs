using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // Mimics .Where(x => x.Id == id)
        Expression<Func<T, bool>> Criteria { get; }
        // Mimics .Include(x => x.ProductType).Include(...
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T,object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDesc { get; }
        bool IsPagingEnabled { get; }
        int Take { get; }
        int Skip { get; }
    }
}
