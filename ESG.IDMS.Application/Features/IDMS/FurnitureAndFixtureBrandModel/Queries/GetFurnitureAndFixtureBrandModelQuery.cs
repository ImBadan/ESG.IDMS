using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Queries;

public record GetFurnitureAndFixtureBrandModelQuery : BaseQuery, IRequest<PagedListResponse<FurnitureAndFixtureBrandModelListDto>>;

public class GetFurnitureAndFixtureBrandModelQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, FurnitureAndFixtureBrandModelListDto, GetFurnitureAndFixtureBrandModelQuery>(context), IRequestHandler<GetFurnitureAndFixtureBrandModelQuery, PagedListResponse<FurnitureAndFixtureBrandModelListDto>>
{
	public override Task<PagedListResponse<FurnitureAndFixtureBrandModelListDto>> Handle(GetFurnitureAndFixtureBrandModelQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<FurnitureAndFixtureBrandModelState>()
			.AsNoTracking().Select(e => new FurnitureAndFixtureBrandModelListDto()
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
