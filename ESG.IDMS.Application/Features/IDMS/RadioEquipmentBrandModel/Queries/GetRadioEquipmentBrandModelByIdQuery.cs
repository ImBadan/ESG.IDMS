using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Queries;

public record GetRadioEquipmentBrandModelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RadioEquipmentBrandModelState>>;

public class GetRadioEquipmentBrandModelByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, RadioEquipmentBrandModelState, GetRadioEquipmentBrandModelByIdQuery>(context), IRequestHandler<GetRadioEquipmentBrandModelByIdQuery, Option<RadioEquipmentBrandModelState>>
{
		
}
