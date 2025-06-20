using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.RadioEquipment.Commands;
using ESG.IDMS.Application.Features.IDMS.RadioEquipment.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class RadioEquipmentController : BaseApiController<RadioEquipmentController>
{
    [Authorize(Policy = Permission.RadioEquipment.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RadioEquipmentState>>> GetAsync([FromQuery] GetRadioEquipmentQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.RadioEquipment.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RadioEquipmentState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRadioEquipmentByIdQuery(id)));

    [Authorize(Policy = Permission.RadioEquipment.Create)]
    [HttpPost]
    public async Task<ActionResult<RadioEquipmentState>> PostAsync([FromBody] RadioEquipmentViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRadioEquipmentCommand>(request)));

    [Authorize(Policy = Permission.RadioEquipment.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RadioEquipmentState>> PutAsync(string id, [FromBody] RadioEquipmentViewModel request)
    {
        var command = Mapper.Map<EditRadioEquipmentCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.RadioEquipment.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RadioEquipmentState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRadioEquipmentCommand { Id = id }));
}

public record RadioEquipmentViewModel
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
	public string RadioEquipmentBrandModelId { get;set; } = "";
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
