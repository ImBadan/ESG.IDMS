using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Queries;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.AuditTrail;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FurnitureAndFixtureBrandModel;

[Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.History)]
public class HistoryModel : BasePageModel<HistoryModel>
{
    public IList<AuditLogViewModel> AuditLogList { get; set; } = [];
    public FurnitureAndFixtureBrandModelViewModel FurnitureAndFixtureBrandModel { get; set; } = new();
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetFurnitureAndFixtureBrandModelByIdQuery(id))).Select(l=> Mapper.Map(l, FurnitureAndFixtureBrandModel));  
        AuditLogList = await Mediatr.Send(new GetAuditLogsByPrimaryKeyQuery(id));
        return Page();
    }
}
