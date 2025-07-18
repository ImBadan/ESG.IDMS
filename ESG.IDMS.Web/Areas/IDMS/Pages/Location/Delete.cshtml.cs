using ESG.IDMS.Application.Features.IDMS.Location.Commands;
using ESG.IDMS.Application.Features.IDMS.Location.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Location;

[Authorize(Policy = Permission.Location.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public LocationViewModel Location { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetLocationByIdQuery(id)), Location);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteLocationCommand { Id = Location.Id }), "Index");
    }
}
