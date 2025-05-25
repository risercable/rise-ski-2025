using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductQuerySpecification : BaseSpecification<Product>
{
    public ProductQuerySpecification(string? brand, string? type, string? sort, string? search = null)
        : base(x =>
            (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
            (string.IsNullOrWhiteSpace(type) || x.Type == type)
        )
    {
        if (string.IsNullOrWhiteSpace(sort))
        {
            AddOrderBy(x => x.Name);
        }
        else
        {
            switch (sort)
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
        }
        
        if (!string.IsNullOrWhiteSpace(search))
        {
            AddSearch(x => x.Name.Contains(search));
        }
    }
}
