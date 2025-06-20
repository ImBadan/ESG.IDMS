using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipment.Queries;

public record GetRadioEquipmentQuery : BaseQuery, IRequest<PagedListResponse<RadioEquipmentListDto>>;

public class GetRadioEquipmentQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, RadioEquipmentListDto, GetRadioEquipmentQuery>(context), IRequestHandler<GetRadioEquipmentQuery, PagedListResponse<RadioEquipmentListDto>>
{
	public override Task<PagedListResponse<RadioEquipmentListDto>> Handle(GetRadioEquipmentQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<RadioEquipmentState>().Include(l=>l.Status).Include(l=>l.RadioEquipmentBrandModel).Include(l=>l.Remarks).Include(l=>l.Location)
			.AsNoTracking().Select(e => new RadioEquipmentListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				ItemQty = e.ItemQty,
				ItemDescription = e.ItemDescription,
				SerialNo = e.SerialNo,
				RadioEquipmentBrandModelId = e.RadioEquipmentBrandModel == null ? "" : e.RadioEquipmentBrandModel!.Description,
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
