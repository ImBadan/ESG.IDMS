using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Users;

public record GetUsersQuery : BaseQuery, IRequest<PagedListResponse<ApplicationUser>>
{
}

public class GetUsersQueryHandler(IdentityContext context) : IRequestHandler<GetUsersQuery, PagedListResponse<ApplicationUser>>
{
    public Task<PagedListResponse<ApplicationUser>> Handle(GetUsersQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.Users.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                            request.SortColumn, request.SortOrder,
                                                            request.PageNumber, request.PageSize));
}
