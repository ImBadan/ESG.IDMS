using ESG.Common.Core.Base.Models;
using System.ComponentModel;

namespace ESG.IDMS.Application.DTOs;

public record FurnitureAndFixtureListDto : BaseDto
{
	public int ItemQty { get; init; }
	public string ItemQtyFormatted { get { return this.ItemQty.ToString("#,##0"); } }
	public string ItemDescription { get; init; } = "";
	public string SerialNo { get; init; } = "";
	public string FurnitureAndFixtureBrandModelId { get; init; } = "";
	public string LocationId { get; init; } = "";
	public string StatusId { get; init; } = "";
	public string RemarksId { get; init; } = "";
	public string FreeTextRemarks { get; init; } = "";
	public DateTime ExpirationDate { get; init; }
	public string ExpirationDateFormatted { get { return this.ExpirationDate.ToString("MMM dd, yyyy HH:mm"); } }
	public string IssuedBy { get; init; } = "";
	public string IssuedTo { get; init; } = "";
	public DateTime IssueDate { get; init; }
	public string IssueDateFormatted { get { return this.IssueDate.ToString("MMM dd, yyyy HH:mm"); } }
	
	
}
