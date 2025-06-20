using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Queries;

public record GetFireArmsBrandModelByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<FireArmsBrandModelState>>;

public class GetFireArmsBrandModelByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, FireArmsBrandModelState, GetFireArmsBrandModelByIdQuery>(context), IRequestHandler<GetFireArmsBrandModelByIdQuery, Option<FireArmsBrandModelState>>
{
		
}
