using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Queries;

public record GetFireArmsBrandModelQuery : BaseQuery, IRequest<PagedListResponse<FireArmsBrandModelListDto>>;

public class GetFireArmsBrandModelQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, FireArmsBrandModelListDto, GetFireArmsBrandModelQuery>(context), IRequestHandler<GetFireArmsBrandModelQuery, PagedListResponse<FireArmsBrandModelListDto>>
{
	public override Task<PagedListResponse<FireArmsBrandModelListDto>> Handle(GetFireArmsBrandModelQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<FireArmsBrandModelState>()
			.AsNoTracking().Select(e => new FireArmsBrandModelListDto()
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
