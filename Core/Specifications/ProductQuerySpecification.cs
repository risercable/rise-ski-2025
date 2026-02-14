using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductQuerySpecification : BaseSpecification<Product>
{
    public ProductQuerySpecification(ProductSpecParams specParams)
        : base(x =>
            (string.IsNullOrEmpty(specParams.Search) || x.Name.ToLower().Contains(specParams.Search)) &&
            (!specParams.Brands.Any() || specParams.Brands.Contains(x.Brand)) &&
            (!specParams.Types.Any() || specParams.Types.Contains(x.Type))
        )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

            switch (specParams.Sort)
            {
                case "priceAsc":
                    AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price);
                    break;
                default:
                    AddOrderBy(x => x.Name);
                    break;
            }
        
        if (!string.IsNullOrWhiteSpace(specParams.Search))
        {
            AddSearch(x => x.Name.Contains(specParams.Search));
        }
    }
}
