using ESG.Common.Identity.Abstractions;
using ESG.IDMS.Application.DTOs;
using ESG.IDMS.Infrastructure.Data;
using LanguageExt;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace ESG.IDMS.Application.Features.IDMS.Report.Queries;

public record GetReportListQuery() : IRequest<IList<Dictionary<string, string>>>;
public class GetReportListQueryHandler(ApplicationContext context, IAuthenticatedUser authenticatedUser, IdentityContext identityContext) : IRequestHandler<GetReportListQuery, IList<Dictionary<string, string>>>
{
    private readonly ApplicationContext _context = context;
    private readonly IAuthenticatedUser _authenticatedUser = authenticatedUser;
    private readonly IdentityContext _identityContext = identityContext;

    public async Task<IList<Dictionary<string, string>>> Handle(GetReportListQuery request, CancellationToken cancellationToken = default)
    {
        var roleList = await (from ur in _identityContext.UserRoles
                              join r in _identityContext.Roles on ur.RoleId equals r.Id
                              where ur.UserId == _authenticatedUser.UserId
                              select r.Name).Distinct().ToListAsync(cancellationToken);
        var reportList = await _context.Report
            .Include(l => l.ReportRoleAssignmentList).AsNoTracking()
			.Where(l => l.ReportRoleAssignmentList!.Any(ra => roleList.Contains(ra.RoleName)) && l.DisplayOnReportModule == true)
            .OrderBy(l => l.Sequence).ToListAsync(cancellationToken: cancellationToken);
        return reportList.Select(report => new Dictionary<string, string>
        {
            { "ReportId", report.Id },
            { "ReportName", report.ReportName },
            { "ReportClass", Helpers.StringHelper.Sanitize(report.ReportName) },
        }).ToList();
    }

}
