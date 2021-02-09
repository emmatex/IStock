namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecificication : BaseSpecification<Entities.Product>
    {
        public ProductWithFiltersForCountSpecificication(ProductSpecParams productParams) : base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
                (string.IsNullOrEmpty(productParams.Search) || x.MeasurementName.ToLower().Contains(productParams.Search)) &&
                (string.IsNullOrEmpty(productParams.BrandId) || x.ProductBrandId == productParams.BrandId) &&
                (string.IsNullOrEmpty(productParams.TypeId) || x.ProductTypeId == productParams.TypeId))
        {
        }
    }
}
