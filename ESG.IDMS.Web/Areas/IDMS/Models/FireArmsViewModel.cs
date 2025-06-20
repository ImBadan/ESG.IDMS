using ESG.Common.Web.Utility.Extensions;
using ESG.IDMS.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using ESG.Common.Web.Utility.Annotations;

namespace ESG.IDMS.Web.Areas.IDMS.Models;

public record FireArmsViewModel : BaseViewModel
{	
	[Display(Name = "Item Qty")]
	[Required]
	public int ItemQty { get; init; } = 0;
	[Display(Name = "Item Description")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ItemDescription { get; init; } = "";
	[Display(Name = "SerialNo")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SerialNo { get; init; } = "";
	[Display(Name = "Firearms Brand / Model")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FireArmsBrandModelId { get; init; } = "";
	public string?  ReferenceFieldFireArmsBrandModelId { get; set; }
	[Display(Name = "Location")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LocationId { get; init; } = "";
	public string?  ReferenceFieldLocationId { get; set; }
	[Display(Name = "Status")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string StatusId { get; init; } = "";
	public string?  ReferenceFieldStatusId { get; set; }
	[Display(Name = "Remarks")]
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RemarksId { get; init; } = "";
	public string?  ReferenceFieldRemarksId { get; set; }
	[Display(Name = "Free Text Remarks")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? FreeTextRemarks { get; init; }
	[Display(Name = "Expiration Date")]
	[Required]
	public DateTime ExpirationDate { get; init; } = DateTime.Now;
	[Display(Name = "Issued By")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? IssuedBy { get; init; }
	[Display(Name = "Issued To")]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string? IssuedTo { get; init; }
	[Display(Name = "Issue Date")]
	[Required]
	public DateTime IssueDate { get; init; } = DateTime.Now;
	
	public DateTime LastModifiedDate { get; set; }
	public FireArmsBrandModelViewModel? FireArmsBrandModel { get; init; }
	public LocationViewModel? Location { get; init; }
	public StatusViewModel? Status { get; init; }
	public RemarksViewModel? Remarks { get; init; }
		
	
}
