using ESG.Common.Core.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Report.Queries;

public record GetReportByIdQuery(string Id) : BaseQueryById(Id), IRequest<Option<ReportState>>;

public class GetReportByIdQueryHandler(ApplicationContext context) : BaseQueryByIdHandler<ApplicationContext, ReportState, GetReportByIdQuery>(context), IRequestHandler<GetReportByIdQuery, Option<ReportState>>
{
    public override async Task<Option<ReportState>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken = default)
    {
        return await Context.Report         
            .Include(l => l.ReportQueryFilterList)
            .Include(l => l.ReportRoleAssignmentList)
            .Where(e => e.Id == request.Id).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
    }

}
