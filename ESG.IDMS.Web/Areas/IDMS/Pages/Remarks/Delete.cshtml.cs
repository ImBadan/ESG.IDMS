using ESG.IDMS.Application.Features.IDMS.Remarks.Commands;
using ESG.IDMS.Application.Features.IDMS.Remarks.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Remarks;

[Authorize(Policy = Permission.Remarks.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public RemarksViewModel Remarks { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetRemarksByIdQuery(id)), Remarks);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteRemarksCommand { Id = Remarks.Id }), "Index");
    }
}
