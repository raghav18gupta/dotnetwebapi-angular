using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecificationParameters productParameters)
            : base(prop => 
                    (string.IsNullOrEmpty(productParameters.Search) || prop.Name.ToLower().Contains(productParameters.Search)) &&
                    (!productParameters.BrandId.HasValue || prop.ProductBrandId == productParameters.BrandId) &&
                    (!productParameters.TypeId.HasValue || prop.ProductTypeId == productParameters.TypeId))
        {
            AddInclude(prop => prop.ProductType);
            AddInclude(prop => prop.ProductBrand);
            AddOrderBy(prop => prop.Name);
            ApplyPaging(productParameters.PageSize * (productParameters.PageIndex - 1),
                        productParameters.PageSize);

            if (!string.IsNullOrEmpty(productParameters.Sort))
            {
                switch (productParameters.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(prop => prop.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(prop => prop.Price);
                        break;
                    default:
                        AddOrderBy(prop => prop.Name);
                        break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpecification(int id) : base(prop => prop.Id == id)
        {
            AddInclude(prop => prop.ProductType);
            AddInclude(prop => prop.ProductBrand);
        }
    }
}
