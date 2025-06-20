using ESG.IDMS.Application.Features.IDMS.FireArms.Commands;
using ESG.IDMS.Application.Features.IDMS.FireArms.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FireArms;

[Authorize(Policy = Permission.FireArms.Edit)]
public class EditModel : BasePageModel<EditModel>
{
    [BindProperty]
    public FireArmsViewModel FireArms { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetFireArmsByIdQuery(id)), FireArms);
    }

    public async Task<IActionResult> OnPost()
    {		
        if (!ModelState.IsValid)
        {
            return Page();
        }
		
        return await TryThenRedirectToPage(async () => await Mediatr.Send(Mapper.Map<EditFireArmsCommand>(FireArms)), "Details", true);
    }	
	public PartialViewResult OnPostChangeFormValue()
    {
        ModelState.Clear();
		
        return Partial("_InputFieldsPartial", FireArms);
    }
	
}
