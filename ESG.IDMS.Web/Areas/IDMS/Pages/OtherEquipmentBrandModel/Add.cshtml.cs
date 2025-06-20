using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.OtherEquipmentBrandModel;

[Authorize(Policy = Permission.OtherEquipmentBrandModel.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public OtherEquipmentBrandModelViewModel OtherEquipmentBrandModel { get; set; } = new();
    [BindProperty]
    public string? RemoveSubDetailId { get; set; }
    [BindProperty]
    public string? AsyncAction { get; set; }
    public IActionResult OnGet()
    {
		
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddOtherEquipmentBrandModelCommand>(OtherEquipmentBrandModel)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", OtherEquipmentBrandModel);
    }
	
}
