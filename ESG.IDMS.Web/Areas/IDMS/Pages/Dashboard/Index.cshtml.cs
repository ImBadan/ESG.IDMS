using ESG.IDMS.Application.Features.IDMS.Report.Commands;
using ESG.IDMS.Application.Features.IDMS.ReportBuilder.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
namespace ESG.IDMS.Web.Areas.IDMS.Pages.Dashboard
{
    public class IndexModel(AIDataAnalyticsServices aiDataAnalyticsServices) : BasePageModel<IndexModel>
    {    
        [BindProperty]
        public IList<ReportResultViewModel> ReportList { get; set; } = [];
        [BindProperty]
        public string SelectedReportId { get; set; } = "";
        public async Task<IActionResult> OnGet()
        {
            Mapper.Map(await Mediatr.Send(new GetDashboardReportsQuery()), ReportList);
            return Page();
        }
        public async Task<PartialViewResult> OnPostDataAnalytics()
        {
            ModelState.Clear();
            var report = ReportList.Where(l => l.ReportId == SelectedReportId).FirstOrDefault();
            var chatGPTResult = await aiDataAnalyticsServices.AIDrivenAnalysis(report!.ReportName!, report.Results!, report.ColumnHeaders!, token: new CancellationToken());
            await Mediatr.Send(new AddReportAnalyticsCommand()
            { 
                ReportId = report.ReportId!,
                Input = $"Report Data : {report.Results} / Report Column Headers : {report.ColumnHeaders}",
                Output = chatGPTResult == null ? "" : chatGPTResult!,
            });
            JObject chatGPTJson = JObject.Parse(chatGPTResult!);
            return Partial("_DataAnalytics", chatGPTJson);
        }
    }
}
