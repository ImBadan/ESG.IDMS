using System.ComponentModel.DataAnnotations;

namespace ESG.IDMS.Web.Areas.Admin.Models;

public record UserRoleViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Display(Name = "Role")]
    public string Name { get; set; } = "";
    [Display(Name = "Status")]
    public bool Selected { get; set; }
}
