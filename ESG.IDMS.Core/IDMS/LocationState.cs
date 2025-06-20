using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record LocationState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<FireArmsState>? FireArmsList { get; set; }
	public IList<RadioEquipmentState>? RadioEquipmentList { get; set; }
	public IList<VehicleState>? VehicleList { get; set; }
	public IList<OtherEquipmentState>? OtherEquipmentList { get; set; }
	public IList<FurnitureAndFixtureState>? FurnitureAndFixtureList { get; set; }
	
}
