using ESG.IDMS.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace ESG.IDMS.Web.Areas.Identity.Data;

public static class UserRole
{
    public static async Task Seed(IServiceProvider serviceProvider)
    {     
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        var userRole = new ApplicationRole(Core.Constants.Roles.User);
        if (!await roleManager.RoleExistsAsync(userRole.Name!))
        {
            _ = await roleManager.CreateAsync(userRole);
        }        
    }
}
