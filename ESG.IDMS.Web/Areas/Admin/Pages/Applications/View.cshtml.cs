using ESG.Common.Web.Utility.Authorization;
using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Core.Oidc;
using LanguageExt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.View)]
public class ViewModel(OpenIddictApplicationManager<OidcApplication> manager, IdentityContext context) : BasePageModel<ViewModel>
{
    public ApplicationViewModel Application { get; set; } = new();
    public IList<PermissionViewModel> ApplicationPermissions { get; set; } = [];

    public async Task<IActionResult> OnGet(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        return await Optional(await manager.FindByClientIdAsync(id))
            .ToActionResult(async application =>
            {
                var descriptor = new OpenIddictApplicationDescriptor();
                await manager.PopulateAsync(descriptor, application!);
                var scopes = descriptor.Permissions.Where(p => p.StartsWith(Permissions.Prefixes.Scope))
                                                   .Map(p => p[4..]);
                ApplicationPermissions = Permission.GenerateAllPermissions()
                                                   .Map(p => new PermissionViewModel
                                                   {
                                                       Permission = p,
                                                       Enabled = scopes.Any(s => s == p),
                                                   }).ToList();
                Application = new()
                {
                    ClientId = descriptor.ClientId ?? "",
                    DisplayName = descriptor.DisplayName ?? "",
                    RedirectUri = string.Join(" ", descriptor.RedirectUris),
                    PostLogoutRedirectUris = string.Join(" ", descriptor.PostLogoutRedirectUris),
                    Scopes = string.Join(" ", scopes.Where(s => !s.StartsWith(AuthorizationClaimTypes.Permission))),
                    Entity = await context.GetEntityName(application!.Entity).IfNoneAsync(""),
                };
                return Page();
            }, none: null);
    }
}
