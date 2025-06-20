using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.Vehicle.Commands;
using ESG.IDMS.Application.Features.IDMS.Vehicle.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class VehicleController : BaseApiController<VehicleController>
{
    [Authorize(Policy = Permission.Vehicle.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<VehicleState>>> GetAsync([FromQuery] GetVehicleQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Vehicle.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetVehicleByIdQuery(id)));

    [Authorize(Policy = Permission.Vehicle.Create)]
    [HttpPost]
    public async Task<ActionResult<VehicleState>> PostAsync([FromBody] VehicleViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddVehicleCommand>(request)));

    [Authorize(Policy = Permission.Vehicle.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleState>> PutAsync(string id, [FromBody] VehicleViewModel request)
    {
        var command = Mapper.Map<EditVehicleCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Vehicle.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VehicleState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteVehicleCommand { Id = id }));
}

public record VehicleViewModel
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
	public string VehicleBrandModelId { get;set; } = "";
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
