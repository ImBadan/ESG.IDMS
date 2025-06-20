using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Core.Identity;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.Roles;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Roles;

[Authorize(Policy = Permission.Roles.View)]
public class ViewModel(RoleManager<ApplicationRole> roleManager) : BasePageModel<ViewModel>
{
    public RoleViewModel Role { get; set; } = new();
    public IList<PermissionViewModel> Permissions { get; set; } = [];

    public async Task<IActionResult> OnGet(string id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetRoleByIdQuery(id))
                            .ToActionResult(async role =>
                            {
                                Role = Mapper.Map<RoleViewModel>(role);
                                var roleClaims = (await roleManager.GetClaimsAsync(role)).Map(c => c.Value);
                                Permissions = Permission.GenerateAllPermissions().Map(p => new PermissionViewModel
                                {
                                    Permission = p,
                                    Enabled = roleClaims.Any(c => c == p)
                                }).ToList();
                                return Page();
                            }, none: null);
    }
}
