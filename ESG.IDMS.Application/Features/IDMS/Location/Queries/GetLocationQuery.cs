using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.Location.Queries;

public record GetLocationQuery : BaseQuery, IRequest<PagedListResponse<LocationListDto>>;

public class GetLocationQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, LocationListDto, GetLocationQuery>(context), IRequestHandler<GetLocationQuery, PagedListResponse<LocationListDto>>
{
	public override Task<PagedListResponse<LocationListDto>> Handle(GetLocationQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<LocationState>()
			.AsNoTracking().Select(e => new LocationListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				Description = e.Description,
			})
			.ToPagedResponse(request.SearchColumns, request.SearchValue,
				request.SortColumn, request.SortOrder,
				request.PageNumber, request.PageSize));
	}	
}
