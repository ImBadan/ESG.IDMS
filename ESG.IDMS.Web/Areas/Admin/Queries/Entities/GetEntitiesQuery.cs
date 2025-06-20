using ESG.Common.Utility.Extensions;
using ESG.Common.Utility.Models;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ESG.Common.Core.Queries;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Queries.Entities;

public record GetEntitiesQuery : BaseQuery, IRequest<PagedListResponse<Entity>>
{
}

public class GetEntitiesQueryHandler(IdentityContext context) : IRequestHandler<GetEntitiesQuery, PagedListResponse<Entity>>
{
    public Task<PagedListResponse<Entity>> Handle(GetEntitiesQuery request, CancellationToken cancellationToken) =>
        Task.FromResult(context.Entities.AsNoTracking().ToPagedResponse(request.SearchColumns, request.SearchValue,
                                                               request.SortColumn, request.SortOrder,
                                                               request.PageNumber, request.PageSize));
}