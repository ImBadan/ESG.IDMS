using ESG.IDMS.Application.Features.IDMS.Report.Commands;
using ESG.IDMS.Application.Features.IDMS.Report.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.ReportSetup;

[Authorize(Policy = Permission.ReportSetup.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public ReportViewModel Report { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await PageFrom(async () => await Mediatr.Send(new GetReportByIdQuery(id)), Report);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditReportCommand>(Report)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();		
		if (AsyncAction == "AddReportQueryFilter")
		{
			return AddReportQueryFilter();
		}
		if (AsyncAction == "RemoveReportQueryFilter")
		{
			return RemoveReportQueryFilter();
		}
		
		
        return Partial("_InputFieldsPartial", Report);
    }

	private PartialViewResult AddReportQueryFilter()
	{
		ModelState.Clear();
		if (Report!.ReportQueryFilterList == null) { Report!.ReportQueryFilterList = []; }
		Report!.ReportQueryFilterList!.Add(new ReportQueryFilterViewModel() { ReportId = Report.Id });
		return Partial("_InputFieldsPartial", Report);
	}
	private PartialViewResult RemoveReportQueryFilter()
	{
		ModelState.Clear();
		Report.ReportQueryFilterList = Report!.ReportQueryFilterList!.Where(l => l.Id != RemoveSubDetailId).ToList();
		return Partial("_InputFieldsPartial", Report);
	}
	
}
