using ESG.IDMS.Application.Features.IDMS.OtherEquipment.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.OtherEquipment;

[Authorize(Policy = Permission.OtherEquipment.View)]
public class DetailsModel : BasePageModel<DetailsModel>
{
    public OtherEquipmentViewModel OtherEquipment { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOtherEquipmentByIdQuery(id)), OtherEquipment);
    }
}
