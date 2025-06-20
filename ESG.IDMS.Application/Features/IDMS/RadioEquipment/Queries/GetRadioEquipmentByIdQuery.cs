using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.RadioEquipment.Queries;

public record GetRadioEquipmentByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RadioEquipmentState>>;

public class GetRadioEquipmentByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, RadioEquipmentState, GetRadioEquipmentByIdQuery>(context), IRequestHandler<GetRadioEquipmentByIdQuery, Option<RadioEquipmentState>>
{
	
	public override async Task<Option<RadioEquipmentState>> Handle(GetRadioEquipmentByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.RadioEquipment.Include(l=>l.Status).Include(l=>l.RadioEquipmentBrandModel).Include(l=>l.Remarks).Include(l=>l.Location)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
