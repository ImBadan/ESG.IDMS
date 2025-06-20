using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Queries;

public record GetOtherEquipmentBrandModelQuery : BaseQuery, IRequest<PagedListResponse<OtherEquipmentBrandModelListDto>>;

public class GetOtherEquipmentBrandModelQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, OtherEquipmentBrandModelListDto, GetOtherEquipmentBrandModelQuery>(context), IRequestHandler<GetOtherEquipmentBrandModelQuery, PagedListResponse<OtherEquipmentBrandModelListDto>>
{
	public override Task<PagedListResponse<OtherEquipmentBrandModelListDto>> Handle(GetOtherEquipmentBrandModelQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<OtherEquipmentBrandModelState>()
			.AsNoTracking().Select(e => new OtherEquipmentBrandModelListDto()
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
