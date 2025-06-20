using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record FireArmsBrandModelState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<FireArmsState>? FireArmsList { get; set; }
	
}
