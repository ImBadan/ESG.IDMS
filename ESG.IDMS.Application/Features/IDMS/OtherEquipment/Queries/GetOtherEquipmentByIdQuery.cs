using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.OtherEquipment.Queries;

public record GetOtherEquipmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<OtherEquipmentState>>;

public class GetOtherEquipmentByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, OtherEquipmentState, GetOtherEquipmentByIdQuery>(context), IRequestHandler<GetOtherEquipmentByIdQuery, Option<OtherEquipmentState>>
{
	
	public override async Task<Option<OtherEquipmentState>> Handle(GetOtherEquipmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.OtherEquipment.Include(l=>l.Remarks).Include(l=>l.OtherEquipmentBrandModel).Include(l=>l.Status).Include(l=>l.Location)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
