using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.Status.Queries;

public record GetStatusQuery : BaseQuery, IRequest<PagedListResponse<StatusListDto>>;

public class GetStatusQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, StatusListDto, GetStatusQuery>(context), IRequestHandler<GetStatusQuery, PagedListResponse<StatusListDto>>
{
	public override Task<PagedListResponse<StatusListDto>> Handle(GetStatusQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<StatusState>()
			.AsNoTracking().Select(e => new StatusListDto()
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
