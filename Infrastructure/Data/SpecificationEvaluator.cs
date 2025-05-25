using System;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria); // query.Where(p => p.Brand == brand)
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy); // query.OrderBy(p => p.Price)
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending); // query.OrderByDescending(p => p.Price)
        }

        if (spec.Search != null)
        {
            query = query.Where(spec.Search); // query.Where(p => p.Name.Contains(searchTerm))
        }

        return query;
    }
}
