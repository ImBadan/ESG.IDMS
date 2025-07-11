using ESG.IDMS.Application.DTOs;

namespace ESG.IDMS.Web.Areas.IDMS.Models
{
    public class ReportResultViewModel
    {
        public string? ReportId { get; set; }
        public string? ReportName { get; set; }
        public string? Results { get; set; }
        public string? ColumnHeaders { get; set; }
        public string ReportOrChartType { get; set; } = "";
        public bool DisplayLegend { get; set; }
	    public bool IsPercentage => ReportName != null && ReportName.Contains('%');
        public IList<ReportQueryFilterViewModel> Filters { get; set; } = [];
        public string? DomId
        {
            get { return this.ReportId?.Replace("-",""); }
        }
    }
}
