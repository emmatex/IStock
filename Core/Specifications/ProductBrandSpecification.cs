namespace Core.Specifications
{
    public class ProductBrandSpecification : BaseSpecification<Entities.ProductBrand>
    {
        public ProductBrandSpecification(SpecParams specParams) : base(x =>
              (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public ProductBrandSpecification(string id) : base(x => x.Id == id)
        {
        }
    }

    public class ProductBandCountSpecificication : BaseSpecification<Entities.ProductBrand>
    {
        public ProductBandCountSpecificication(SpecParams specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
        }
    }
}
