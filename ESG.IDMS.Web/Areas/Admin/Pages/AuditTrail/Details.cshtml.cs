using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.AuditTrail;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Pages.AuditTrail;

[Authorize(Policy = Permission.AuditTrail.View)]
public class DetailsModel(UserManager<ApplicationUser> userManager) : BasePageModel<DetailsModel>
{
    public AuditLogViewModel AuditLog { get; set; } = new();

    public async Task<IActionResult> OnGet(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetAuditLogByIdQuery((int)id)).ToActionResult(
            someAsync: async e =>
            {
                Mapper.Map(e, AuditLog);
				if (!string.IsNullOrEmpty(e.UserId))
				{
					var user = await userManager.FindByIdAsync(e.UserId);
					AuditLog.User = Mapper.Map<AuditLogUserViewModel>(user);
				}      
				return Page();
            },
            none: null);
    }
}
