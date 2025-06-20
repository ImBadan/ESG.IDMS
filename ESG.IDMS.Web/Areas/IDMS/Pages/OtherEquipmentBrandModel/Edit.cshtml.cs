using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.OtherEquipmentBrandModel;

[Authorize(Policy = Permission.OtherEquipmentBrandModel.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public OtherEquipmentBrandModelViewModel OtherEquipmentBrandModel { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetOtherEquipmentBrandModelByIdQuery(id)), OtherEquipmentBrandModel);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditOtherEquipmentBrandModelCommand>(OtherEquipmentBrandModel)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", OtherEquipmentBrandModel);
    }
	
}
