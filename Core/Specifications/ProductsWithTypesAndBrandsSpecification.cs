namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Entities.Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams) : base(x =>
              (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
              (string.IsNullOrEmpty(productParams.Search) || x.MeasurementName.ToLower().Contains(productParams.Search)) &&
              (string.IsNullOrEmpty(productParams.BrandId) || x.ProductBrandId == productParams.BrandId) &&
              (string.IsNullOrEmpty(productParams.TypeId) || x.ProductTypeId == productParams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
        public ProductsWithTypesAndBrandsSpecification(string id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
