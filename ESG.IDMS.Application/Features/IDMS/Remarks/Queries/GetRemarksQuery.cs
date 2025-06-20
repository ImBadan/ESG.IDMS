using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.Remarks.Queries;

public record GetRemarksQuery : BaseQuery, IRequest<PagedListResponse<RemarksListDto>>;

public class GetRemarksQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, RemarksListDto, GetRemarksQuery>(context), IRequestHandler<GetRemarksQuery, PagedListResponse<RemarksListDto>>
{
	public override Task<PagedListResponse<RemarksListDto>> Handle(GetRemarksQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<RemarksState>()
			.AsNoTracking().Select(e => new RemarksListDto()
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
