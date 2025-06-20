using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Queries;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.AuditTrail;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.OtherEquipmentBrandModel;

[Authorize(Policy = Permission.OtherEquipmentBrandModel.History)]
public class HistoryModel : BasePageModel<HistoryModel>
{
    public IList<AuditLogViewModel> AuditLogList { get; set; } = [];
    public OtherEquipmentBrandModelViewModel OtherEquipmentBrandModel { get; set; } = new();
    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        _ = (await Mediatr.Send(new GetOtherEquipmentBrandModelByIdQuery(id))).Select(l=> Mapper.Map(l, OtherEquipmentBrandModel));  
        AuditLogList = await Mediatr.Send(new GetAuditLogsByPrimaryKeyQuery(id));
        return Page();
    }
}
