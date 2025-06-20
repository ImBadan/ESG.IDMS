using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static ESG.Common.Utility.Helpers.OptionHelper;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Users;

public record GetUserByIdQuery(string Id) : IRequest<Option<ApplicationUser>>;

public class GetUserByIdQueryHandler(IdentityContext context) : IRequestHandler<GetUserByIdQuery, Option<ApplicationUser>>
{
    public async Task<Option<ApplicationUser>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) =>
        ToOption(await context.Users.FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken: cancellationToken));
}