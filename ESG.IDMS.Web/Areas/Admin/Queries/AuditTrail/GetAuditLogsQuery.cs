using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using LanguageExt;
using ESG.Common.Data;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.Common.Utility.Models;
using ESG.Common.Core.Queries;
namespace ESG.IDMS.Web.Areas.Admin.Queries.AuditTrail;
public record GetAuditLogsQuery : BaseQuery, IRequest<PagedListResponse<AuditLogViewModel>>;
public class GetAuditLogsQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, AuditLogViewModel, GetAuditLogsQuery>(context), IRequestHandler<GetAuditLogsQuery, PagedListResponse<AuditLogViewModel>>
{
    public override Task<PagedListResponse<AuditLogViewModel>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(Context.Set<Audit>()
            .AsNoTracking().Select(e => new AuditLogViewModel()
            {
                Id = e.Id,
                UserId = e.UserId,
                Type = e.Type,
                TableName = e.TableName,
                DateTime = e.DateTime,
                PrimaryKey = e.PrimaryKey,
                TraceId = e.TraceId,
            })
            .ToPagedResponse(request.SearchColumns, request.SearchValue,
                request.SortColumn, request.SortOrder,
                request.PageNumber, request.PageSize));
    }
}
