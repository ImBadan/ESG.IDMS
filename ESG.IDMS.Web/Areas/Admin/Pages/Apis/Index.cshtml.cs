using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.IDMS.Web.Areas.Admin.Queries.Scopes;
using ESG.IDMS.Web.Models;
using ESG.IDMS.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.Admin.Pages.Apis;

[Authorize(Policy = Permission.Apis.View)]
public class IndexModel : BasePageModel<IndexModel>
{
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }

    public ScopeViewModel Scope { get; set; } = new();

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
        var result = await Mediatr.Send(DataRequest!.ToQuery<GetScopesQuery>());
        return new JsonResult(result.Data
            .Select(e => new
            {
                e.Id,
                e.Name,
            })
            .ToDataTablesResponse(DataRequest, result.TotalCount, result.Data.TotalItemCount));
    }
}
