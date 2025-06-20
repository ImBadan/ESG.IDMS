using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Queries;

public record GetVehicleBrandModelQuery : BaseQuery, IRequest<PagedListResponse<VehicleBrandModelListDto>>;

public class GetVehicleBrandModelQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, VehicleBrandModelListDto, GetVehicleBrandModelQuery>(context), IRequestHandler<GetVehicleBrandModelQuery, PagedListResponse<VehicleBrandModelListDto>>
{
	public override Task<PagedListResponse<VehicleBrandModelListDto>> Handle(GetVehicleBrandModelQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<VehicleBrandModelState>()
			.AsNoTracking().Select(e => new VehicleBrandModelListDto()
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
