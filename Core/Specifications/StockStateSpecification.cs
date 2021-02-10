namespace Core.Specifications
{
    public class StockStateSpecification : BaseSpecification<Entities.StockState>
    {
        public StockStateSpecification(SpecParams specParams) : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
            AddOrderBy(x => x.Name);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
        }
        public StockStateSpecification(string id) : base(x => x.Id == id)
        {
        }
    }

    public class StockStateCountSpecificication : BaseSpecification<Entities.StockState>
    {
        public StockStateCountSpecificication(SpecParams specParams) : base(x =>
                (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)))
        {
        }
    }
}
