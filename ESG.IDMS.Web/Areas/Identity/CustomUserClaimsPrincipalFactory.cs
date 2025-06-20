using ESG.Common.Web.Utility.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Identity;

public class CustomUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
                                        RoleManager<ApplicationRole> roleManager,
                                        IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>(userManager, roleManager, options)
{
    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        ((ClaimsIdentity)principal.Identity!).AddClaims([
            new(CustomClaimTypes.Entity, user.EntityId!),
        ]);
        return principal;
    }

    protected override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
    {
        return base.GenerateClaimsAsync(user);
    }
}