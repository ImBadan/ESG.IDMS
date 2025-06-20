using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Core.Oidc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using static LanguageExt.Prelude;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Apis;

[Authorize(Policy = Permission.Apis.View)]
public class ViewModel(OpenIddictScopeManager<OidcScope> manager) : BasePageModel<ViewModel>
{
    public ScopeViewModel Scope { get; set; } = new();

    public async Task<IActionResult> OnGet(string? name)
    {
        if (name == null)
        {
            return NotFound();
        }
        return await Optional(await manager.FindByNameAsync(name))
            .ToActionResult(async scope =>
            {
                var descriptor = new OpenIddictScopeDescriptor();
                await manager.PopulateAsync(descriptor, scope!);
                Scope = new()
                {
                    Name = descriptor.Name ?? "",
                    DisplayName = descriptor.DisplayName ?? "",
                    Resources = string.Join(" ", descriptor.Resources)
                };
                return Page();
            }, none: null);
    }
}
