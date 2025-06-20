using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class RadioEquipmentBrandModelController : BaseApiController<RadioEquipmentBrandModelController>
{
    [Authorize(Policy = Permission.RadioEquipmentBrandModel.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RadioEquipmentBrandModelState>>> GetAsync([FromQuery] GetRadioEquipmentBrandModelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.RadioEquipmentBrandModel.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RadioEquipmentBrandModelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRadioEquipmentBrandModelByIdQuery(id)));

    [Authorize(Policy = Permission.RadioEquipmentBrandModel.Create)]
    [HttpPost]
    public async Task<ActionResult<RadioEquipmentBrandModelState>> PostAsync([FromBody] RadioEquipmentBrandModelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRadioEquipmentBrandModelCommand>(request)));

    [Authorize(Policy = Permission.RadioEquipmentBrandModel.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RadioEquipmentBrandModelState>> PutAsync(string id, [FromBody] RadioEquipmentBrandModelViewModel request)
    {
        var command = Mapper.Map<EditRadioEquipmentBrandModelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.RadioEquipmentBrandModel.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RadioEquipmentBrandModelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRadioEquipmentBrandModelCommand { Id = id }));
}

public record RadioEquipmentBrandModelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
