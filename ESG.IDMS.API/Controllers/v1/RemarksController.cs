using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.Remarks.Commands;
using ESG.IDMS.Application.Features.IDMS.Remarks.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class RemarksController : BaseApiController<RemarksController>
{
    [Authorize(Policy = Permission.Remarks.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<RemarksState>>> GetAsync([FromQuery] GetRemarksQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.Remarks.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<RemarksState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetRemarksByIdQuery(id)));

    [Authorize(Policy = Permission.Remarks.Create)]
    [HttpPost]
    public async Task<ActionResult<RemarksState>> PostAsync([FromBody] RemarksViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddRemarksCommand>(request)));

    [Authorize(Policy = Permission.Remarks.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<RemarksState>> PutAsync(string id, [FromBody] RemarksViewModel request)
    {
        var command = Mapper.Map<EditRemarksCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.Remarks.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<RemarksState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteRemarksCommand { Id = id }));
}

public record RemarksViewModel
{
    [Required]
	[StringLength(255, ErrorMessage = "{0} length can't be more than {1}.")]
	public string Description { get;set; } = "";
	   
}
