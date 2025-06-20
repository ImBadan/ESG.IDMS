using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class FurnitureAndFixtureBrandModelController : BaseApiController<FurnitureAndFixtureBrandModelController>
{
    [Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FurnitureAndFixtureBrandModelState>>> GetAsync([FromQuery] GetFurnitureAndFixtureBrandModelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureBrandModelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetFurnitureAndFixtureBrandModelByIdQuery(id)));

    [Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.Create)]
    [HttpPost]
    public async Task<ActionResult<FurnitureAndFixtureBrandModelState>> PostAsync([FromBody] FurnitureAndFixtureBrandModelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddFurnitureAndFixtureBrandModelCommand>(request)));

    [Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureBrandModelState>> PutAsync(string id, [FromBody] FurnitureAndFixtureBrandModelViewModel request)
    {
        var command = Mapper.Map<EditFurnitureAndFixtureBrandModelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.FurnitureAndFixtureBrandModel.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureBrandModelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteFurnitureAndFixtureBrandModelCommand { Id = id }));
}

public record FurnitureAndFixtureBrandModelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
