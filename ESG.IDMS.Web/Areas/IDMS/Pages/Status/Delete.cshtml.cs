using ESG.IDMS.Application.Features.IDMS.Status.Commands;
using ESG.IDMS.Application.Features.IDMS.Status.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Status;

[Authorize(Policy = Permission.Status.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public StatusViewModel Status { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetStatusByIdQuery(id)), Status);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteStatusCommand { Id = Status.Id }), "Index");
    }
}
