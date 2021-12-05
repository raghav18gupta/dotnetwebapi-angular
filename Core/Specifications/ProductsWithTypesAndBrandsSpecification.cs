using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(prop => prop.ProductType);
            AddInclude(prop => prop.ProductBrand);
        }
        public ProductsWithTypesAndBrandsSpecification(int id) : base(prop => prop.Id == id)
        {
            AddInclude(prop => prop.ProductType);
            AddInclude(prop => prop.ProductBrand);
        }
    }
}
