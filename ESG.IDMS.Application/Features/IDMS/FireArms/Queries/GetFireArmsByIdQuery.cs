using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.FireArms.Queries;

public record GetFireArmsByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<FireArmsState>>;

public class GetFireArmsByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, FireArmsState, GetFireArmsByIdQuery>(context), IRequestHandler<GetFireArmsByIdQuery, Option<FireArmsState>>
{
	
	public override async Task<Option<FireArmsState>> Handle(GetFireArmsByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.FireArms.Include(l=>l.Remarks).Include(l=>l.Location).Include(l=>l.Status).Include(l=>l.FireArmsBrandModel)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
