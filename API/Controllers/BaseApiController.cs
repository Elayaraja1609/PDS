using API.RequestHelpers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	#if DEBUG
	[AllowAnonymous]
	#endif
	[Route("api/v1/[controller]")]
	[ApiController]
	[Authorize]
	public class BaseApiController : ControllerBase
	{
		protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepo<T> repo,
		ISpecification<T> spec, int pageIndex, int pageSize) where T : class
		{
			var items = await repo.GetAllWithSpec(spec);
			var count = await repo.CountAsync(spec);
			var pagination = new Pagination<T>(pageIndex, pageSize, count, items);

			return Ok(pagination);

		}
	}
}
