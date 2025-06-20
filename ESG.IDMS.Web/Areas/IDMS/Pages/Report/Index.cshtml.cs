using ESG.IDMS.Application.DTOs;
using ESG.IDMS.Application.Features.IDMS.Report.Commands;
using ESG.IDMS.Application.Features.IDMS.ReportBuilder.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Report
{
    [Authorize(Policy = Permission.Report.View)]
    public class IndexModel : BasePageModel<IndexModel>
    {
        [BindProperty]
        public ReportResultViewModel Report { get; set; } = new ReportResultViewModel();
        [BindProperty]
        public IList<ReportQueryFilterViewModel> Filters { get; set; } = [];
        [BindProperty]
        public string ReportId { get; set; } = "";
        public async Task<IActionResult> OnGet(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ReportId = id;
            var reportResult = new ReportResultModel();
            _ = (await Mediatr.Send(new GetReportSetupAndResultByIdQuery(id))).Select(l => reportResult = l);
            Mapper.Map(reportResult, Report);
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var reportResult = new ReportResultModel();
            var query = new GetReportSetupAndResultByIdQuery(ReportId);
            Mapper.Map(Filters, query.Filters);
            _ = (await Mediatr.Send(query)).Select(l => reportResult = l);
            Mapper.Map(reportResult, Report);
            return Page();
        }
    }
}
