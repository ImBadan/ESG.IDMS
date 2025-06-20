using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Application.DTOs;

public record VehicleBrandModelListDto : BaseDto
{
	public string Description { get; init; } = "";
	
	
}
