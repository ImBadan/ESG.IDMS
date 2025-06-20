using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Queries;

public record GetFurnitureAndFixtureByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<FurnitureAndFixtureState>>;

public class GetFurnitureAndFixtureByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, FurnitureAndFixtureState, GetFurnitureAndFixtureByIdQuery>(context), IRequestHandler<GetFurnitureAndFixtureByIdQuery, Option<FurnitureAndFixtureState>>
{
	
	public override async Task<Option<FurnitureAndFixtureState>> Handle(GetFurnitureAndFixtureByIdQuery request, CancellationToken cancellationToken = default)
	{
		return await Context.FurnitureAndFixture.Include(l=>l.Remarks).Include(l=>l.Location).Include(l=>l.FurnitureAndFixtureBrandModel).Include(l=>l.Status)
			.Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
	}
	
}
