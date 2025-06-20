using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipment.Queries;

public record GetOtherEquipmentQuery : BaseQuery, IRequest<PagedListResponse<OtherEquipmentListDto>>;

public class GetOtherEquipmentQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, OtherEquipmentListDto, GetOtherEquipmentQuery>(context), IRequestHandler<GetOtherEquipmentQuery, PagedListResponse<OtherEquipmentListDto>>
{
	public override Task<PagedListResponse<OtherEquipmentListDto>> Handle(GetOtherEquipmentQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<OtherEquipmentState>().Include(l=>l.Remarks).Include(l=>l.OtherEquipmentBrandModel).Include(l=>l.Status).Include(l=>l.Location)
			.AsNoTracking().Select(e => new OtherEquipmentListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				ItemQty = e.ItemQty,
				ItemDescription = e.ItemDescription,
				SerialNo = e.SerialNo,
				OtherEquipmentBrandModelId = e.OtherEquipmentBrandModel == null ? "" : e.OtherEquipmentBrandModel!.Description,
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
