using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Queries;
using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Roles;

public record GetApproverRolesQuery(string CurrentSelectedApprover, IList<string> AllSelectedApprovers) : BaseQuery, IRequest<PagedListResponse<ApplicationRole>>
{
}

public class GetApproverRolesQueryHandler(IdentityContext context) : IRequestHandler<GetApproverRolesQuery, PagedListResponse<ApplicationRole>>
{
    public Task<PagedListResponse<ApplicationRole>> Handle(GetApproverRolesQuery request, CancellationToken cancellationToken)
    {
        var excludedUsers = request.AllSelectedApprovers.Where(l => l != request.CurrentSelectedApprover);
        var query = context.Roles.Where(l => !excludedUsers.Contains(l.Id)).AsNoTracking();
        return Task.FromResult(query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                       request.SortColumn, request.SortOrder,
                                                       request.PageNumber, request.PageSize));
    }
}