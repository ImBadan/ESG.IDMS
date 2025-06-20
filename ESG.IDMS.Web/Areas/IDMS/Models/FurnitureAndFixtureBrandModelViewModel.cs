using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ESG.Common.Web.Utility.Annotations;

namespace ESG.IDMS.Web.Areas.IDMS.Models;

public record FurnitureAndFixtureBrandModelViewModel : BaseViewModel
{	
	[Display(Name = "Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<FurnitureAndFixtureViewModel>? FurnitureAndFixtureList { get; set; }
	
}
