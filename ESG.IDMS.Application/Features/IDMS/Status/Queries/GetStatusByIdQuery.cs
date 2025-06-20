using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Status.Queries;

public record GetStatusByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<StatusState>>;

public class GetStatusByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, StatusState, GetStatusByIdQuery>(context), IRequestHandler<GetStatusByIdQuery, Option<StatusState>>
{
		
}
