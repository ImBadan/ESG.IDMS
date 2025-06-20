using ESG.IDMS.Application.Features.IDMS.RadioEquipment.Commands;
using ESG.IDMS.Application.Features.IDMS.RadioEquipment.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.RadioEquipment;

[Authorize(Policy = Permission.RadioEquipment.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public RadioEquipmentViewModel RadioEquipment { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetRadioEquipmentByIdQuery(id)), RadioEquipment);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteRadioEquipmentCommand { Id = RadioEquipment.Id }), "Index");
    }
}
