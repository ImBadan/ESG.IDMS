using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class FireArmsBrandModelController : BaseApiController<FireArmsBrandModelController>
{
    [Authorize(Policy = Permission.FireArmsBrandModel.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FireArmsBrandModelState>>> GetAsync([FromQuery] GetFireArmsBrandModelQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.FireArmsBrandModel.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<FireArmsBrandModelState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetFireArmsBrandModelByIdQuery(id)));

    [Authorize(Policy = Permission.FireArmsBrandModel.Create)]
    [HttpPost]
    public async Task<ActionResult<FireArmsBrandModelState>> PostAsync([FromBody] FireArmsBrandModelViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddFireArmsBrandModelCommand>(request)));

    [Authorize(Policy = Permission.FireArmsBrandModel.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<FireArmsBrandModelState>> PutAsync(string id, [FromBody] FireArmsBrandModelViewModel request)
    {
        var command = Mapper.Map<EditFireArmsBrandModelCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.FireArmsBrandModel.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FireArmsBrandModelState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteFireArmsBrandModelCommand { Id = id }));
}

public record FireArmsBrandModelViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
