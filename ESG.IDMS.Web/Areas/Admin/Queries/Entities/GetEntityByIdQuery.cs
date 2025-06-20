using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ESG.Common.Utility.Helpers.OptionHelper;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Entities;

public record GetEntityByIdQuery(string Id) : IRequest<Option<Entity>>;

public class GetEntityByIdQueryHandler(IdentityContext context) : IRequestHandler<GetEntityByIdQuery, Option<Entity>>
{
    public async Task<Option<Entity>> Handle(GetEntityByIdQuery request, CancellationToken cancellationToken) =>
       ToOption(await context.Entities.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}