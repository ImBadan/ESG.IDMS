using ESG.IDMS.Core.Identity;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static ESG.Common.Utility.Helpers.OptionHelper;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Roles;

public record GetRoleByIdQuery(string Id) : IRequest<Option<ApplicationRole>>;

public class GetRoleByIdQueryHandler(IdentityContext context) : IRequestHandler<GetRoleByIdQuery, Option<ApplicationRole>>
{
    public async Task<Option<ApplicationRole>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken) =>
       ToOption(await context.Roles.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}