using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.FireArms.Commands;
using ESG.IDMS.Application.Features.IDMS.FireArms.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class FireArmsController : BaseApiController<FireArmsController>
{
    [Authorize(Policy = Permission.FireArms.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FireArmsState>>> GetAsync([FromQuery] GetFireArmsQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.FireArms.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<FireArmsState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetFireArmsByIdQuery(id)));

    [Authorize(Policy = Permission.FireArms.Create)]
    [HttpPost]
    public async Task<ActionResult<FireArmsState>> PostAsync([FromBody] FireArmsViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddFireArmsCommand>(request)));

    [Authorize(Policy = Permission.FireArms.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<FireArmsState>> PutAsync(string id, [FromBody] FireArmsViewModel request)
    {
        var command = Mapper.Map<EditFireArmsCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.FireArms.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FireArmsState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteFireArmsCommand { Id = id }));
}

public record FireArmsViewModel
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
	public string FireArmsBrandModelId { get;set; } = "";
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
