using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record VehicleBrandModelState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<VehicleState>? VehicleList { get; set; }
	
}
