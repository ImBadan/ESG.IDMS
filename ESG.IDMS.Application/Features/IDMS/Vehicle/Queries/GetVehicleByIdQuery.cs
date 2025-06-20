using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Vehicle.Queries;

public record GetVehicleByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<VehicleState>>;

public class GetVehicleByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, VehicleState, GetVehicleByIdQuery>(context), IRequestHandler<GetVehicleByIdQuery, Option<VehicleState>>
{
	
	public override async Task<Option<VehicleState>> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.Vehicle.Include(l=>l.Status).Include(l=>l.VehicleBrandModel).Include(l=>l.Remarks).Include(l=>l.Location)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
