using System.ComponentModel.DataAnnotations;

namespace ESG.IDMS.Web.Areas.Admin.Models;

public record RoleViewModel
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; } = "";
}
