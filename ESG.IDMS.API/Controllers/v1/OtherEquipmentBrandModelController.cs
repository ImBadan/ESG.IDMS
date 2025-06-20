using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class OtherEquipmentBrandModelController : BaseApiController<OtherEquipmentBrandModelController>
{
    [Authorize(Policy = Permission.OtherEquipmentBrandModel.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<OtherEquipmentBrandModelState>>> GetAsync([FromQuery] GetOtherEquipmentBrandModelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.OtherEquipmentBrandModel.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<OtherEquipmentBrandModelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetOtherEquipmentBrandModelByIdQuery(id)));

    [Authorize(Policy = Permission.OtherEquipmentBrandModel.Create)]
    [HttpPost]
    public async Task<ActionResult<OtherEquipmentBrandModelState>> PostAsync([FromBody] OtherEquipmentBrandModelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddOtherEquipmentBrandModelCommand>(request)));

    [Authorize(Policy = Permission.OtherEquipmentBrandModel.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<OtherEquipmentBrandModelState>> PutAsync(string id, [FromBody] OtherEquipmentBrandModelViewModel request)
    {
        var command = Mapper.Map<EditOtherEquipmentBrandModelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.OtherEquipmentBrandModel.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<OtherEquipmentBrandModelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteOtherEquipmentBrandModelCommand { Id = id }));
}

public record OtherEquipmentBrandModelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
