using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Queries;

public record GetFurnitureAndFixtureQuery : BaseQuery, IRequest<PagedListResponse<FurnitureAndFixtureListDto>>;

public class GetFurnitureAndFixtureQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, FurnitureAndFixtureListDto, GetFurnitureAndFixtureQuery>(context), IRequestHandler<GetFurnitureAndFixtureQuery, PagedListResponse<FurnitureAndFixtureListDto>>
{
	public override Task<PagedListResponse<FurnitureAndFixtureListDto>> Handle(GetFurnitureAndFixtureQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<FurnitureAndFixtureState>().Include(l=>l.Remarks).Include(l=>l.Location).Include(l=>l.FurnitureAndFixtureBrandModel).Include(l=>l.Status)
			.AsNoTracking().Select(e => new FurnitureAndFixtureListDto()
			{
				Id = e.Id,
				LastModifiedDate = e.LastModifiedDate,
				ItemQty = e.ItemQty,
				ItemDescription = e.ItemDescription,
				SerialNo = e.SerialNo,
				FurnitureAndFixtureBrandModelId = e.FurnitureAndFixtureBrandModel == null ? "" : e.FurnitureAndFixtureBrandModel!.Description,
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
