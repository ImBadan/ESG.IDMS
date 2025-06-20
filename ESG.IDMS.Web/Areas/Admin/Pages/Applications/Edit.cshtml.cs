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
using System.Text.Json;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.Edit)]
public class EditModel(OpenIddictApplicationManager<OidcApplication> manager, IdentityContext context) : BasePageModel<EditModel>
{
    [BindProperty]
    public ApplicationViewModel Application { get; set; } = new();

    [BindProperty]
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
                    EntityId = application!.Entity,
                    Entities = await context.GetEntitiesList(application!.Entity)
                };
                return Page();
            }, none: null);
    }

    public async Task<IActionResult> OnPostGenerateAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Optional(await manager.FindByClientIdAsync(Application.ClientId))
            .ToActionResult(async application => await GenerateNewSecret(application!), none: null);
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return await Optional(await manager.FindByClientIdAsync(Application.ClientId))
            .ToActionResult(async application => await UpdateApplication(application!), none: null);
    }

    async Task<IActionResult> GenerateNewSecret(OidcApplication application)
    {
        return await TryAsync<IActionResult>(async () =>
        {
            Application.ClientSecret = Guid.NewGuid().ToString();
            var descriptor = new OpenIddictApplicationDescriptor();
            await manager.PopulateAsync(descriptor, application);
            descriptor.ClientSecret = Application.ClientSecret;
            await manager.UpdateAsync(application, descriptor, new());
            NotyfService.Success(Localizer["Generated new client secret"]);
            Logger.LogInformation("Updated Client Secret. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
            TempData.Put("Application", Application);
            return RedirectToPage("Details");
        }).IfFail(ex =>
        {
            ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            Logger.LogError(ex, "Exception in OnPostEditAsync");
            return Page();
        });
    }

    async Task<IActionResult> UpdateApplication(OidcApplication application)
    {
        return await TryAsync<IActionResult>(async () =>
        {
            var redirectUris = new System.Collections.Generic.HashSet<Uri> { new(Application.RedirectUri) };
            var postLogoutRedirectUris = new System.Collections.Generic.HashSet<Uri> { new(Application.PostLogoutRedirectUris) };
            var permissions = new System.Collections.Generic.HashSet<string>
            {
                Permissions.Endpoints.Authorization,
                Permissions.Endpoints.Device,
                Permissions.Endpoints.Logout,
                Permissions.Endpoints.Token,
                Permissions.GrantTypes.AuthorizationCode,
                Permissions.GrantTypes.ClientCredentials,
                Permissions.GrantTypes.DeviceCode,
                Permissions.GrantTypes.Password,
                Permissions.GrantTypes.RefreshToken,
                Permissions.ResponseTypes.Code,
                Permissions.Prefixes.Scope + AuthorizationClaimTypes.Permission,
            };
            permissions =
            [
                .. permissions,
                .. Application.Scopes.Split(" ")
                .Map(e => Permissions.Prefixes.Scope + e),
            ];
            permissions =
            [
                .. permissions,
                .. ApplicationPermissions.Filter(e => e.Enabled)
                .Map(e => Permissions.Prefixes.Scope + e.Permission),
            ];
            application.DisplayName = Application.DisplayName;
            application.RedirectUris = JsonSerializer.Serialize(redirectUris);
            application.PostLogoutRedirectUris = JsonSerializer.Serialize(postLogoutRedirectUris);
            application.Permissions = JsonSerializer.Serialize(permissions);
            application.Entity = Application.EntityId;
            await manager.UpdateAsync(application);
            NotyfService.Success(Localizer["Record saved successfully"]);
            Logger.LogInformation("Updated Application. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
            return RedirectToPage("View", new { id = Application.ClientId });
        }).IfFail(ex =>
        {
            ModelState.AddModelError("", Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
            Logger.LogError(ex, "Exception in OnPostEditAsync");
            return Page();
        });
    }
}
