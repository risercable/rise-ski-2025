using API.RequestHelpers;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repo, ISpecification<T> specParams,
        int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repo.ListAsync(specParams);
        var count = await repo.CountAsync(specParams);
        var pagination = new Pagination<Product>(pageIndex, pageSize, count, items as IReadOnlyList<Product>);

        return Ok(pagination);
    }
}