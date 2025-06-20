using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.Users;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Users;

[Authorize(Policy = Permission.Users.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public UserViewModel UserModel { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetUsersQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
                e.Email,
                e.IsActive
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.Data.TotalItemCount));
    }
}
