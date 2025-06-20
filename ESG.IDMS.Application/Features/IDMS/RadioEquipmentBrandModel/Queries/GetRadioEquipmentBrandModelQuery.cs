using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Application.DTOs;
using LanguageExt;

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Queries;

public record GetRadioEquipmentBrandModelQuery : BaseQuery, IRequest<PagedListResponse<RadioEquipmentBrandModelListDto>>;

public class GetRadioEquipmentBrandModelQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, RadioEquipmentBrandModelListDto, GetRadioEquipmentBrandModelQuery>(context), IRequestHandler<GetRadioEquipmentBrandModelQuery, PagedListResponse<RadioEquipmentBrandModelListDto>>
{
	public override Task<PagedListResponse<RadioEquipmentBrandModelListDto>> Handle(GetRadioEquipmentBrandModelQuery request, CancellationToken cancellationToken = default)
	{
		return Task.FromResult(Context.Set<RadioEquipmentBrandModelState>()
			.AsNoTracking().Select(e => new RadioEquipmentBrandModelListDto()
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
