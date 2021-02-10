using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Specifications
{
    public class StockReceiveSpecification : BaseSpecification<Entities.StockReceive>
    {
        public StockReceiveSpecification(SpecParams specParams) : base(x =>
             (string.IsNullOrEmpty(specParams.Search) || x.DocumentNo.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.CrossReference.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.ReceiverName.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.CreatedBy.ToLower().Contains(specParams.Search)))
        {
            AddOrderBy(x => x.DocumentNo);
            ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "TransactionDateAsc":
                        AddOrderBy(p => p.TransactionDate);
                        break;
                    case "TransactionDateDesc":
                        AddOrderByDescending(p => p.TransactionDate);
                        break;
                    default:
                        AddOrderBy(n => n.DocumentNo);
                        break;
                }
            }
        }
        public StockReceiveSpecification(string id) : base(x => x.Id == id)
        {  }
    }

    public class StockReceiveCountSpecificication : BaseSpecification<Entities.StockReceive>
    {
        public StockReceiveCountSpecificication(SpecParams specParams) : base(x =>
             (string.IsNullOrEmpty(specParams.Search) || x.DocumentNo.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.CrossReference.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.ReceiverName.ToLower().Contains(specParams.Search)) &&
             (string.IsNullOrEmpty(specParams.Search) || x.CreatedBy.ToLower().Contains(specParams.Search)))
        {
        }
    }
}
