using ESG.Common.Core.Queries;
using ESG.Common.Utility.Models;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using MediatR;
using ESG.Common.Utility.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ESG.IDMS.Application.Features.IDMS.Report.Queries;

public record GetReportQuery : BaseQuery, IRequest<PagedListResponse<ReportState>>;

public class GetReportQueryHandler(ApplicationContext context) : BaseQueryHandler<ApplicationContext, ReportState, GetReportQuery>(context), IRequestHandler<GetReportQuery, PagedListResponse<ReportState>>
{
}
