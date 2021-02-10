namespace Core.Specifications
{
    public class ProductTypeSpecification : BaseSpecification<Entities.ProductType>
    {
        public ProductTypeSpecification(SpecParams specParams) : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public ProductTypeSpecification(string id) : base(x => x.Id == id)
        {
        }
    }

    public class ProductTypeCountSpecificication : BaseSpecification<Entities.ProductType>
    {
        public ProductTypeCountSpecificication(SpecParams specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
        }
    }
}
