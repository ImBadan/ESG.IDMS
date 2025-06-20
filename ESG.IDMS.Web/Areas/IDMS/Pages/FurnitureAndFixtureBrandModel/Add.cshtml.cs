using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FurnitureAndFixtureBrandModel;

[Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.Create)]
public class AddModel : BasePageModel<AddModel>
{
    [BindProperty]
    public FurnitureAndFixtureBrandModelViewModel FurnitureAndFixtureBrandModel { get; set; } = new();
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
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<AddFurnitureAndFixtureBrandModelCommand>(FurnitureAndFixtureBrandModel)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", FurnitureAndFixtureBrandModel);
    }
	
}
