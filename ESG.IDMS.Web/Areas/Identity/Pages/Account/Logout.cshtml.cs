using ESG.IDMS.Web.Areas.Admin.Commands.AuditTrail;
using ESG.IDMS.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LogoutModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                   ILogger<LogoutModel> logger, IMediator mediator) : BasePageModel<LogoutModel>
{
    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        var user = await userManager.GetUserAsync(User);
        await signInManager.SignOutAsync();
        await mediator.Send(new AddAuditLogCommand() { UserId = user?.Id, Type = "User logged out", TraceId = TraceId });
        logger.LogInformation("User logged out, Email = {Email}", user?.Email);
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }

    public async Task<IActionResult> OnPost(string? returnUrl = null)
    {
        var user = await userManager.GetUserAsync(User);
        await signInManager.SignOutAsync();
        await mediator.Send(new AddAuditLogCommand() { UserId = user?.Id, Type = "User logged out", TraceId = TraceId });
        logger.LogInformation("User logged out, Email = {Email}", user?.Email);
        if (returnUrl != null)
        {
            return LocalRedirect(returnUrl);
        }
        else
        {
            return RedirectToPage("/Index");
        }
    }
}