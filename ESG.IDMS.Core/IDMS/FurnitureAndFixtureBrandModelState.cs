using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record FurnitureAndFixtureBrandModelState : BaseEntity
{
	public string Description { get; init; } = "";
	
	
	public IList<FurnitureAndFixtureState>? FurnitureAndFixtureList { get; set; }
	
}
