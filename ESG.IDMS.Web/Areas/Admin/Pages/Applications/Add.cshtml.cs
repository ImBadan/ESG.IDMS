using ESG.Common.Web.Utility.Authorization;
using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Core.Oidc;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;
using System.Text.Json;
using static LanguageExt.Prelude;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Applications;

[Authorize(Policy = Permission.Applications.Create)]
public class AddModel(OpenIddictApplicationManager<OidcApplication> manager, IdentityContext context) : BasePageModel<AddModel>
{
    [BindProperty]
    public ApplicationViewModel Application { get; set; } = new();

    [BindProperty]
    public IList<PermissionViewModel> ApplicationPermissions { get; set; } = [];

    public async Task<IActionResult> OnGet()
    {
        ApplicationPermissions = Permission.GenerateAllPermissions().Map(p => new PermissionViewModel { Permission = p }).ToList();
        Application.Entities = await context.GetEntitiesList(Application.EntityId);
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        Application.Entities = await context.GetEntitiesList(Application.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return (await CreateApplication())
            .ToActionResult(
            success: application =>
            {
                NotyfService.Success(Localizer["Record saved successfully"]);
                Logger.LogInformation("Created Application. Client ID: {ClientId}, Application: {Application}", application.ClientId, application.ToString());
                TempData.Put("Application", Application);
                return RedirectToPage("Details");
            },
            fail: errors =>
            {
                errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                Logger.LogError("Error in OnPostAddAsync. Error: {Errors}", string.Join(",", errors));
                return Page();
            });
    }

    async Task<Validation<Error, ApplicationViewModel>> CreateApplication()
    {
        return await TryAsync(async () =>
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
            Application.ClientId = Guid.NewGuid().ToString();
            Application.ClientSecret = Guid.NewGuid().ToString();
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
            var application = new OidcApplication
            {
                ClientId = Application.ClientId,
                DisplayName = Application.DisplayName,
                RedirectUris = JsonSerializer.Serialize(redirectUris),
                PostLogoutRedirectUris = JsonSerializer.Serialize(postLogoutRedirectUris),
                Permissions = JsonSerializer.Serialize(permissions),
                Entity = Application.EntityId,
            };
            await manager.CreateAsync(application, Application.ClientSecret);
            return Success<Error, ApplicationViewModel>(Application);
        }).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception in OnPostAddAsync");
            return Fail<Error, ApplicationViewModel>(Localizer[$"Something went wrong. Please contact the system administrator."] + $" TraceId = {HttpContext.TraceIdentifier}");
        });
    }
}
