using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Core.Oidc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Scopes;

public record GetScopesQuery : BaseQuery, IRequest<PagedListResponse<OidcScope>>
{
}

public class GetScopesQueryHandler(IdentityContext context) : IRequestHandler<GetScopesQuery, PagedListResponse<OidcScope>>
{
    public Task<PagedListResponse<OidcScope>> Handle(GetScopesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.Set<OidcScope>()
                      .AsNoTracking()
                      .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                       request.SortOrder, request.PageNumber, request.PageSize));
}