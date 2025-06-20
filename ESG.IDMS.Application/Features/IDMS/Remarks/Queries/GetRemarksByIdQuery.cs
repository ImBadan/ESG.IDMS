using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Remarks.Queries;

public record GetRemarksByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<RemarksState>>;

public class GetRemarksByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, RemarksState, GetRemarksByIdQuery>(context), IRequestHandler<GetRemarksByIdQuery, Option<RemarksState>>
{
		
}
