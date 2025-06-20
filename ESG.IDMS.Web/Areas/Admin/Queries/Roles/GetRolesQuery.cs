using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Roles;

public record GetRolesQuery : BaseQuery, IRequest<PagedListResponse<ApplicationRole>>
{
}

public class GetRolesQueryHandler(IdentityContext context) : IRequestHandler<GetRolesQuery, PagedListResponse<ApplicationRole>>
{
    public Task<PagedListResponse<ApplicationRole>> Handle(GetRolesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.Roles.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                            request.SortColumn, request.SortOrder,
                                                            request.PageNumber, request.PageSize));
}
