using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Queries;

public record GetOtherEquipmentBrandModelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OtherEquipmentBrandModelState>>;

public class GetOtherEquipmentBrandModelByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, OtherEquipmentBrandModelState, GetOtherEquipmentBrandModelByIdQuery>(context), IRequestHandler<GetOtherEquipmentBrandModelByIdQuery, Option<OtherEquipmentBrandModelState>>
{
		
}
