using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Core.IDMS;

public record RadioEquipmentState : BaseEntity
{
	public int ItemQty { get; init; }
	public string ItemDescription { get; init; } = "";
	public string SerialNo { get; init; } = "";
	public string RadioEquipmentBrandModelId { get; init; } = "";
	public string LocationId { get; init; } = "";
	public string StatusId { get; init; } = "";
	public string RemarksId { get; init; } = "";
	public string? FreeTextRemarks { get; init; }
	public DateTime ExpirationDate { get; init; }
	public string? IssuedBy { get; init; }
	public string? IssuedTo { get; init; }
	public DateTime IssueDate { get; init; }


	public RadioEquipmentBrandModelState? RadioEquipmentBrandModel { get; init; }
	public LocationState? Location { get; init; }
	public StatusState? Status { get; init; }
	public RemarksState? Remarks { get; init; }
	
	
}
