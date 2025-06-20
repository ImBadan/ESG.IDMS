using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class VehicleBrandModelController : BaseApiController<VehicleBrandModelController>
{
    [Authorize(Policy = Permission.VehicleBrandModel.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<VehicleBrandModelState>>> GetAsync([FromQuery] GetVehicleBrandModelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.VehicleBrandModel.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<VehicleBrandModelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetVehicleBrandModelByIdQuery(id)));

    [Authorize(Policy = Permission.VehicleBrandModel.Create)]
    [HttpPost]
    public async Task<ActionResult<VehicleBrandModelState>> PostAsync([FromBody] VehicleBrandModelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddVehicleBrandModelCommand>(request)));

    [Authorize(Policy = Permission.VehicleBrandModel.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<VehicleBrandModelState>> PutAsync(string id, [FromBody] VehicleBrandModelViewModel request)
    {
        var command = Mapper.Map<EditVehicleBrandModelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.VehicleBrandModel.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<VehicleBrandModelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteVehicleBrandModelCommand { Id = id }));
}

public record VehicleBrandModelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
