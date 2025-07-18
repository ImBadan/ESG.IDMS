using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record OtherEquipmentBrandModelState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<OtherEquipmentState>? OtherEquipmentList { get; set; }
	
}
