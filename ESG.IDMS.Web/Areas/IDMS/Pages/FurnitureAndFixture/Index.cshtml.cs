using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Web.Models;
using ESG.IDMS.ExcelProcessor.Services;
using ESG.IDMS.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.FurnitureAndFixture;

[Authorize(Policy = Permission.FurnitureAndFixture.View)]
public class IndexModel(IConfiguration configuration) : BasePageModel<IndexModel>
{   
	private readonly string? _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");
    [DataTablesRequest]
    public DataTablesRequest? DataRequest { get; set; }
	[BindProperty]
    public BatchUploadModel BatchUpload { get; set; } = new();
    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostListAllAsync()
    {
		var result = await Mediatr.Send(DataRequest!.ToQuery<GetFurnitureAndFixtureQuery>());
		return new JsonResult(result.Data.ToDataTablesResponse(DataRequest, result.TotalCount, result.Data.TotalItemCount));	
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetFurnitureAndFixtureQuery>(nameof(FurnitureAndFixtureState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, FurnitureAndFixtureState>(BatchUpload.BatchUploadForm, nameof(FurnitureAndFixtureState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<FurnitureAndFixtureState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
