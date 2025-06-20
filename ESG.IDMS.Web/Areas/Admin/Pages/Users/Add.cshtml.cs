using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Web.Models;
using LanguageExt;
using LanguageExt.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
using static ESG.IDMS.Web.Areas.Identity.IdentityExtensions;
using static LanguageExt.Prelude;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.Create)]
public class AddModel(UserManager<ApplicationUser> userManager, IdentityContext context, RoleManager<ApplicationRole> roleManager) : BasePageModel<AddModel>
{
    [BindProperty]
    public UserViewModel UserModel { get; set; } = new();
    public async Task<IActionResult> OnGetAsync()
    {
        UserModel.Entities = await context.GetEntitiesList(UserModel.EntityId);
        UserModel.Roles = GetRoles();
        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        UserModel.Entities = await context.GetEntitiesList(UserModel.EntityId);
        if (!ModelState.IsValid)
        {
            return Page();
        }
        if (string.IsNullOrEmpty(UserModel.Password) || string.IsNullOrEmpty(UserModel.ConfirmPassword))
        {
            NotyfService.Error(Localizer["Password and Confirm Password are required."]);
            return Page();
        }
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        return await Optional(await userManager.FindByEmailAsync(UserModel.Email))
            .MatchAsync(
                Some: user => Fail<Error, ApplicationUser>($"User with email {user.Email} already exists"),
                None: async () => await CreateUserAsync()).BindT(async u => await AddRolesToUser(u))
            .ToActionResult(
            success: user =>
            {
                scope.Complete();
                Logger.LogInformation("Created User. Email: {Email}, User: {User}", user.Email, user.ToString());
                NotyfService.Success(Localizer["Record saved successfully"]);
                return RedirectToPage("View", new { id = user.Id });
            },
            fail: errors =>
            {
                Logger.LogError("Error in OnPost. Error: {Errors}", string.Join(",", errors));
			    errors.Iter(error => ModelState.AddModelError("", error.ToString()));
                return Page();
            });
    }
    public IActionResult OnPostChangeFormValue()
    {
        ModelState.Clear();

        return Partial("_InputFieldsPartial", UserModel);
    }
    List<UserRoleViewModel> GetRoles()
    {
        return
        [
            .. roleManager.Roles.Map(r => new UserRoleViewModel
            {
                Id = r.Id,
                Name = r.Name!,
            }),
        ];
    }

    async Task<Validation<Error, ApplicationUser>> CreateUserAsync()
    {
        return await TryAsync<Validation<Error, ApplicationUser>>(async () =>
        {
            var user = new ApplicationUser
            {
                UserName = UserModel.Email,
                Email = UserModel.Email,
                Name = UserModel.Name,              
                EntityId = UserModel.EntityId,
                IsActive = true,
                EmailConfirmed = true,
            };
            var result = await userManager.CreateAsync(user, UserModel.Password!);
            if (!result.Succeeded)
            {
                return result.Errors.Select(e => e.Description).Map(e => Error.New(e)).ToSeq();
            }
            return user;
        }).IfFail(ex =>
        {
            Logger.LogError(ex, "Exception in OnPost");
            return Error.New(ex.Message);
        });
    }

    async Task<Validation<Error, ApplicationUser>> AddRolesToUser(ApplicationUser user)
    {
        var roles = UserModel.Roles.Where(r => r.Selected).Select(r => r.Name);
        return await userManager.AddRoles(user, roles);
    }
}