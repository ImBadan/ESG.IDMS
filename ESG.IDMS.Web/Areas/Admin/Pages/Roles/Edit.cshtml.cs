using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Core.Identity;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.Roles;
using ESG.IDMS.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static ESG.IDMS.Web.Areas.Identity.IdentityExtensions;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Roles;

[Authorize(Policy = Permission.Roles.Edit)]
public class EditModel(RoleManager<ApplicationRole> roleManager) : BasePageModel<EditModel>
{
    [BindProperty]
    public RoleViewModel Role { get; set; } = new();

    [BindProperty]
    public IList<PermissionViewModel> Permissions { get; set; } = [];

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Mediatr.Send(new GetRoleByIdQuery(id))
                            .ToActionResult(async e =>
                            {
                                Role = Mapper.Map<RoleViewModel>(e);
                                Permissions = await GetPermisionsForRole(e);
                                return Page();
                            }, none: null);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Mediatr.Send(new GetRoleByIdQuery(Role.Id)).ToActionResult(
            async r =>
            {
                using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
                return await UpdateRoleFromModel(r)
                .BindT(async r => await UpdatePermissionsForRole(r))
                .ToActionResult(
                    success: role =>
                    {
                        scope.Complete();
                        Logger.LogInformation("Updated Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                        NotyfService.Success(Localizer["Record saved successfully"]);
                        return RedirectToPage("View", new { id = role.Id });
                    },
                    fail: errors =>
                    {
                        Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                        ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                        return Page();
                    });
            },
            none: null);
    }

    async Task<IList<PermissionViewModel>> GetPermisionsForRole(ApplicationRole role)
    {
        var roleClaims = (await roleManager.GetClaimsAsync(role)).Map(c => c.Value);
        return Permission.GenerateAllPermissions().Map(p => new PermissionViewModel
        {
            Permission = p,
            Enabled = roleClaims.Any(c => c == p)
        }).ToList();
    }

    async Task<Validation<Error, ApplicationRole>> UpdateRoleFromModel(ApplicationRole roleToUpdate)
    {
        roleToUpdate.Name = Role.Name;
        roleToUpdate.NormalizedName = Role.Name.ToUpper();
        return await UpdateRole(roleToUpdate);
    }

    async Task<Validation<Error, ApplicationRole>> UpdatePermissionsForRole(ApplicationRole role) =>
        await roleManager.RemoveAllPermissionClaims(role)
                          .BindT(async r => await AddPermissionsToRole(r));

    async Task<Validation<Error, ApplicationRole>> AddPermissionsToRole(ApplicationRole role)
    {
        var permissions = Permissions.Where(p => p.Enabled).Select(p => p.Permission);
        return await roleManager.AddPermissionClaims(role, permissions);
    }

    Func<ApplicationRole, Task<Validation<Error, ApplicationRole>>> UpdateRole =>
        ToValidation<ApplicationRole>(roleManager.UpdateAsync);
}
