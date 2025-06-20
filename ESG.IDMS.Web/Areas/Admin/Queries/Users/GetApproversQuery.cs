using ESG.IDMS.Core.Identity;
using ESG.IDMS.Infrastructure.Data;
using ESG.Common.Core.Queries;
using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace ESG.IDMS.Web.Areas.Admin.Queries.Users;

public record GetApproversQuery(string CurrentSelectedApprover, IList<string> AllSelectedApprovers) : BaseQuery, IRequest<PagedListResponse<ApplicationUser>>
{
}

public class GetApproversQueryHandler(IdentityContext context) : IRequestHandler<GetApproversQuery, PagedListResponse<ApplicationUser>>
{
    public Task<PagedListResponse<ApplicationUser>> Handle(GetApproversQuery request, CancellationToken cancellationToken)
    {
        var excludedUsers = request.AllSelectedApprovers.Where(l => l != request.CurrentSelectedApprover);
        var query = context.Users.Where(l => !excludedUsers.Contains(l.Id)).AsNoTracking();
        return Task.FromResult(query.ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                       request.SortColumn, request.SortOrder,
                                                       request.PageNumber, request.PageSize));
    }
}
