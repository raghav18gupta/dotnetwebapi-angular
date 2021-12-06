using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecificationParameters productParameters)
            : base(prop =>
                    (string.IsNullOrEmpty(productParameters.Search) || prop.Name.ToLower().Contains(productParameters.Search)) &&
                    (!productParameters.BrandId.HasValue || prop.ProductBrandId == productParameters.BrandId) &&
                    (!productParameters.TypeId.HasValue || prop.ProductTypeId == productParameters.TypeId))
        {

        }
    }
}
