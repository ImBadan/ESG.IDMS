using ESG.Common.Utility.Models;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Queries;
using ESG.IDMS.Core.IDMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ESG.Common.API.Controllers;
using Asp.Versioning;

namespace ESG.IDMS.API.Controllers.v1;

[ApiVersion("1.0")]
public class FurnitureAndFixtureController : BaseApiController<FurnitureAndFixtureController>
{
    [Authorize(Policy = Permission.FurnitureAndFixture.View)]
    [HttpGet]
    public async Task<ActionResult<PagedListResponse<FurnitureAndFixtureState>>> GetAsync([FromQuery] GetFurnitureAndFixtureQuery query) =>
        Ok(await Mediator.Send(query));

    [Authorize(Policy = Permission.FurnitureAndFixture.View)]
    [HttpGet("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureState>> GetAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new GetFurnitureAndFixtureByIdQuery(id)));

    [Authorize(Policy = Permission.FurnitureAndFixture.Create)]
    [HttpPost]
    public async Task<ActionResult<FurnitureAndFixtureState>> PostAsync([FromBody] FurnitureAndFixtureViewModel request) =>
        await ToActionResult(async () => await Mediator.Send(Mapper.Map<AddFurnitureAndFixtureCommand>(request)));

    [Authorize(Policy = Permission.FurnitureAndFixture.Edit)]
    [HttpPut("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureState>> PutAsync(string id, [FromBody] FurnitureAndFixtureViewModel request)
    {
        var command = Mapper.Map<EditFurnitureAndFixtureCommand>(request);
        return await ToActionResult(async () => await Mediator.Send(command with { Id = id }));
    }

    [Authorize(Policy = Permission.FurnitureAndFixture.Delete)]
    [HttpDelete("{id}")]
    public async Task<ActionResult<FurnitureAndFixtureState>> DeleteAsync(string id) =>
        await ToActionResult(async () => await Mediator.Send(new DeleteFurnitureAndFixtureCommand { Id = id }));
}

public record FurnitureAndFixtureViewModel
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
	public string FurnitureAndFixtureBrandModelId { get;set; } = "";
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
