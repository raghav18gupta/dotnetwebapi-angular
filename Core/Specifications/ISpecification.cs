using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // Mimics .Where(x => x.Id == id)
        Expression<Func<T, bool>> Criteria { get; }
        // Mimics .Include(x => x.ProductType).Include(...
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
