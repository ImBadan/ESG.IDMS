using ESG.IDMS.Web.Areas.Admin.Commands.AuditTrail;
using ESG.IDMS.Web.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Identity.Pages.Account;

[AllowAnonymous]
public class LoginModel(SignInManager<ApplicationUser> signInManager,
                  UserManager<ApplicationUser> userManager,
                  IMediator mediator,
                  IConfiguration configuration) : BasePageModel<LoginModel>
{
    private readonly bool _siteIsAvailable = configuration.GetValue<bool>("SiteIsAvailable")!;

    [BindProperty]
    public InputModel? Input { get; set; }

    public IList<AuthenticationScheme>? ExternalLogins { get; set; }

    public string? ReturnUrl { get; set; }

    [TempData]
    public string? ErrorMessage { get; set; }

    public class InputModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public async Task<IActionResult> OnGetAsync(string? returnUrl = null)
    {
        if(!_siteIsAvailable)
        {
            return RedirectToPage("./SiteNotAvailable");
        }       
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        ReturnUrl = returnUrl;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        if (ModelState.IsValid)
        {
            var user = await userManager.FindByNameAsync(Input!.Email!);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                Logger.LogWarning("Invalid login attempt, Email = {Email}", Input!.Email);
                return Page();
            }
            if (!user.IsActive)
            {
                await mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User is not active", TraceId = TraceId });
                Logger.LogWarning("User is not active, Email = {Email}", Input!.Email);
                return RedirectToPage("./NotActive");
            }
            var result = await signInManager.PasswordSignInAsync(Input!.Email!, Input!.Password!, Input!.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                await mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User logged in", TraceId = TraceId });
                Logger.LogInformation("User logged in, Email = {Email}", Input!.Email);
                NotyfService.Success($"Logged in as {Input!.Email}");
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }
            if (result.IsLockedOut)
            {
                await mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "User account locked out", TraceId = TraceId });
                Logger.LogWarning("User account locked out, Email = {Email}", Input!.Email);
                return RedirectToPage("./Lockout");
            }
            else
            {
                await mediator.Send(new AddAuditLogCommand() { UserId = user.Id, Type = "Invalid login attempt", TraceId = TraceId });
                user.LastUnsucccessfulLogin = DateTime.UtcNow;
                _ = await userManager.UpdateAsync(user);
                Logger.LogWarning("Invalid login attempt, Email = {Email}", Input!.Email);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }
        }

        // If we got this far, something failed, redisplay form
        return Page();
    }
}