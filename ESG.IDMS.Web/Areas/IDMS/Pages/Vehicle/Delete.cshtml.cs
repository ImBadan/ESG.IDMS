using ESG.IDMS.Application.Features.IDMS.Vehicle.Commands;
using ESG.IDMS.Application.Features.IDMS.Vehicle.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Vehicle;

[Authorize(Policy = Permission.Vehicle.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public VehicleViewModel Vehicle { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetVehicleByIdQuery(id)), Vehicle);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteVehicleCommand { Id = Vehicle.Id }), "Index");
    }
}
