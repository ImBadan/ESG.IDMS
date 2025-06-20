using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ESG.Common.Web.Utility.Annotations;

namespace ESG.IDMS.Web.Areas.IDMS.Models;

public record StatusViewModel : BaseViewModel
{	
	[Display(Name = "Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get; init; } = "";
	
	public DateTime LastModifiedDate { get; set; }
		
	public IList<FireArmsViewModel>? FireArmsList { get; set; }
	public IList<RadioEquipmentViewModel>? RadioEquipmentList { get; set; }
	public IList<VehicleViewModel>? VehicleList { get; set; }
	public IList<OtherEquipmentViewModel>? OtherEquipmentList { get; set; }
	public IList<FurnitureAndFixtureViewModel>? FurnitureAndFixtureList { get; set; }
	
}
