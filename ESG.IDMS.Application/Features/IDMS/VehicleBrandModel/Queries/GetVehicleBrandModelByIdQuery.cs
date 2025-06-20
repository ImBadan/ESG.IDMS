using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Queries;

public record GetVehicleBrandModelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<VehicleBrandModelState>>;

public class GetVehicleBrandModelByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, VehicleBrandModelState, GetVehicleBrandModelByIdQuery>(context), IRequestHandler<GetVehicleBrandModelByIdQuery, Option<VehicleBrandModelState>>
{
		
}
