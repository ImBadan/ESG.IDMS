using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Core.Oidc;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Applications;

public record GetApplicationsQuery : BaseQuery, IRequest<PagedListResponse<OidcApplication>>
{
}

public class GetApplicationsQueryHandler(IdentityContext context) : IRequestHandler<GetApplicationsQuery, PagedListResponse<OidcApplication>>
{
    public Task<PagedListResponse<OidcApplication>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.Set<OidcApplication>()
                      .AsNoTracking()
                      .ToPagedResponse(request.SearchColumns, request.SearchValue, request.SortColumn,
                                       request.SortOrder, request.PageNumber, request.PageSize));
}