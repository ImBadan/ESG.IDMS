using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.FireArms.Queries;

public record GetFireArmsQuery : BaseQuery, IRequest<PagedListResponse<FireArmsListDto>>;

public class GetFireArmsQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, FireArmsListDto, GetFireArmsQuery>(context), IRequestHandler<GetFireArmsQuery, PagedListResponse<FireArmsListDto>>
{
	public override Task<PagedListResponse<FireArmsListDto>> Handle(GetFireArmsQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<FireArmsState>().Include(l=>l.Remarks).Include(l=>l.Location).Include(l=>l.Status).Include(l=>l.FireArmsBrandModel)
			.AsNoTracking().Select(e => new FireArmsListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				ItemQty = e.ItemQty,
				ItemDescription = e.ItemDescription,
				SerialNo = e.SerialNo,
				FireArmsBrandModelId = e.FireArmsBrandModel == null ? "" : e.FireArmsBrandModel!.Description,
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
