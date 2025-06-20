using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESG.IDMS.Web.Areas.Identity.Pages.Account;
[AllowAnonymous]
public class NotActiveModel : PageModel
{
    public void OnGet()
    {
    }
}
