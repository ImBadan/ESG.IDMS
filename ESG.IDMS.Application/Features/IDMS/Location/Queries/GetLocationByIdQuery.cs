using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Location.Queries;

public record GetLocationByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<LocationState>>;

public class GetLocationByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, LocationState, GetLocationByIdQuery>(context), IRequestHandler<GetLocationByIdQuery, Option<LocationState>>
{
		
}
