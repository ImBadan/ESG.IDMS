using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record RadioEquipmentBrandModelState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<RadioEquipmentState>? RadioEquipmentList { get; set; }
	
}
