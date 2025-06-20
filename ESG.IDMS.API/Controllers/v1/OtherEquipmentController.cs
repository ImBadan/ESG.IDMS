using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.OtherEquipment.Commands;
using ESG.IDMS.Application.Features.IDMS.OtherEquipment.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class OtherEquipmentController : BaseApiController<OtherEquipmentController>
{
    [Authorize(Policy = Permission.OtherEquipment.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OtherEquipmentState>>> GetAsync([FromQuery] GetOtherEquipmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.OtherEquipment.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OtherEquipmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOtherEquipmentByIdQuery(id)));

    [Authorize(Policy = Permission.OtherEquipment.Create)]
    [HttpPost]
    public async Task<ActionResult<OtherEquipmentState>> PostAsync([FromBody] OtherEquipmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOtherEquipmentCommand>(request)));

    [Authorize(Policy = Permission.OtherEquipment.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OtherEquipmentState>> PutAsync(string id, [FromBody] OtherEquipmentViewModel request)
    {
        var command = Mapper.Map<EditOtherEquipmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.OtherEquipment.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OtherEquipmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOtherEquipmentCommand { Id = id }));
}

public record OtherEquipmentViewModel
{
    [Required]
	public int ItemQty { get;set; } = 0;
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string ItemDescription { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string SerialNo { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string OtherEquipmentBrandModelId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string LocationId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string StatusId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string RemarksId { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string FreeTextRemarks { get;set; } = "";
	[Required]
	public DateTime ExpirationDate { get;set; } = DateTime.Now;
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string IssuedBy { get;set; } = "";
	[Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string IssuedTo { get;set; } = "";
	[Required]
	public DateTime IssueDate { get;set; } = DateTime.Now;
	   
}
