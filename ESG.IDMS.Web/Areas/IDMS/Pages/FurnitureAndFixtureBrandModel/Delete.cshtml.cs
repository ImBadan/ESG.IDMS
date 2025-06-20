using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Queries;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FurnitureAndFixtureBrandModel;

[Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.Delete)]
public class DeleteModel : BasePageModel<DeleteModel>
{
    [BindProperty]
    public FurnitureAndFixtureBrandModelViewModel FurnitureAndFixtureBrandModel { get; set; } = new();
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
        return await PageFrom(async () => await Mediatr.Send(new GetFurnitureAndFixtureBrandModelByIdQuery(id)), FurnitureAndFixtureBrandModel);
    }

    public async Task<IActionResult> OnPost()
    {
        return await TryThenRedirectToPage(async () => await Mediatr.Send(new DeleteFurnitureAndFixtureBrandModelCommand { Id = FurnitureAndFixtureBrandModel.Id }), "Index");
    }
}
