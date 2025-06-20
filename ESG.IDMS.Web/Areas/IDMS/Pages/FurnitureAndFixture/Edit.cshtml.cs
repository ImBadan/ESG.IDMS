using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FurnitureAndFixture;

[Authorize(Policy = Permission.FurnitureAndFixture.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public FurnitureAndFixtureViewModel FurnitureAndFixture { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetFurnitureAndFixtureByIdQuery(id)), FurnitureAndFixture);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditFurnitureAndFixtureCommand>(FurnitureAndFixture)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", FurnitureAndFixture);
    }
	
}
