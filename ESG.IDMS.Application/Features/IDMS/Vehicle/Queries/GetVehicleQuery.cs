using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.Vehicle.Queries;

public record GetVehicleQuery : BaseQuery, IRequest<PagedListResponse<VehicleListDto>>;

public class GetVehicleQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, VehicleListDto, GetVehicleQuery>(context), IRequestHandler<GetVehicleQuery, PagedListResponse<VehicleListDto>>
{
	public override Task<PagedListResponse<VehicleListDto>> Handle(GetVehicleQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<VehicleState>().Include(l=>l.Status).Include(l=>l.VehicleBrandModel).Include(l=>l.Remarks).Include(l=>l.Location)
			.AsNoTracking().Select(e => new VehicleListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				ItemQty = e.ItemQty,
				ItemDescription = e.ItemDescription,
				SerialNo = e.SerialNo,
				VehicleBrandModelId = e.VehicleBrandModel == null ? "" : e.VehicleBrandModel!.Description,
				LocationId = e.Location == null ? "" : e.Location!.Description,
				StatusId = e.Status == null ? "" : e.Status!.Description,
				RemarksId = e.Remarks == null ? "" : e.Remarks!.Description,
				FreeTextRemarks = e.FreeTextRemarks,
				ExpirationDate = e.ExpirationDate,
				IssuedBy = e.IssuedBy,
				IssuedTo = e.IssuedTo,
				IssueDate = e.IssueDate,
			})
			.ToPagedResponse(request.SearchColumns, request.SearchValue,
				request.SortColumn, request.SortOrder,
				request.PageNumber, request.PageSize));
	}	
}
