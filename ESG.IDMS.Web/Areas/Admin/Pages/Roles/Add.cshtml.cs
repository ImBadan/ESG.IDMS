using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Core.Identity;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static ESG.IDMS.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Roles;

[Authorize(Policy = Permission.Roles.Create)]
public class AddModel(RoleManager<ApplicationRole> roleManager) : BasePageModel<AddModel>
{
    [BindProperty]
    public RoleViewModel Role { get; set; } = new();

    [BindProperty]
    public IList<PermissionViewModel> Permissions { get; set; } = [];

    public IActionResult OnGet()
    {
        Permissions = Permission.GenerateAllPermissions()
                                .Map(p => new PermissionViewModel { Permission = p })
                                .ToList();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        return await Optional(await roleManager.FindByNameAsync(Role.Name))
            .MatchAsync(
                Some: role => Fail<Error, ApplicationRole>($"Role with name {role.Name} already exists"),
                None: async () => await CreateRole(new ApplicationRole(Role.Name)).BindT(
                    async r => await AddPermissionsToRole(r))
                )
            .ToActionResult(
            success: role =>
            {
                scope.Complete();
                Logger.LogInformation("Created Role. ID: {ID}, Role: {Role}", role.Id, role.ToString());
                NotyfService.Success(Localizer["Record saved successfully"]);
                return RedirectToPage("View", new { id = role.Id });
            },
            fail: errors =>
            {
                Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
                ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
                return Page();
            });
    }

    async Task<Validation<Error, ApplicationRole>> CreateRole(ApplicationRole role)
    {
        return await TryAsync<Validation<Error, ApplicationRole>>(async () =>
        {
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => e.Description).Map(e => Error.New(e)).ToSeq();
            }
            return role;
        }).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception in OnPost");
            return Error.New(ex.Message);
        });
    }

    async Task<Validation<Error, ApplicationRole>> AddPermissionsToRole(ApplicationRole role)
    {
        var permissions = Permissions.Where(p => p.Enabled).Select(p => p.Permission);
        return await roleManager.AddPermissionClaims(role, permissions);
    }
}
