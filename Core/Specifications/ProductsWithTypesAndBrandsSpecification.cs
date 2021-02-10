namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Entities.Product>
    {
        public ProductsWithTypesAndBrandsSpecification(SpecParams specParams) : base(x =>
              (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
              (string.IsNullOrEmpty(specParams.Search) || x.MeasurementName.ToLower().Contains(specParams.Search)) &&
              (string.IsNullOrEmpty(specParams.BrandId) || x.ProductBrandId == specParams.BrandId) &&
              (string.IsNullOrEmpty(specParams.TypeId) || x.ProductTypeId == specParams.TypeId))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
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

    public class ProductWithFiltersForCountSpecificication : BaseSpecification<Entities.Product>
    {
        public ProductWithFiltersForCountSpecificication(SpecParams specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
                (string.IsNullOrEmpty(specParams.Search) || x.MeasurementName.ToLower().Contains(specParams.Search)) &&
                (string.IsNullOrEmpty(specParams.BrandId) || x.ProductBrandId == specParams.BrandId) &&
                (string.IsNullOrEmpty(specParams.TypeId) || x.ProductTypeId == specParams.TypeId))
        {
        }
    }

}
