using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ESG.IDMS.Web.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet()
    {
        return Redirect("IDMS/Dashboard/Index");
    }
}
