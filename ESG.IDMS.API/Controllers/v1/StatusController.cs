using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.Status.Commands;
using ESG.IDMS.Application.Features.IDMS.Status.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class StatusController : BaseApiController<StatusController>
{
    [Authorize(Policy = Permission.Status.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<StatusState>>> GetAsync([FromQuery] GetStatusQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Status.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<StatusState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetStatusByIdQuery(id)));

    [Authorize(Policy = Permission.Status.Create)]
    [HttpPost]
    public async Task<ActionResult<StatusState>> PostAsync([FromBody] StatusViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddStatusCommand>(request)));

    [Authorize(Policy = Permission.Status.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<StatusState>> PutAsync(string id, [FromBody] StatusViewModel request)
    {
        var command = Mapper.Map<EditStatusCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Status.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<StatusState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteStatusCommand { Id = id }));
}

public record StatusViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
