using ESG.IDMS.Application.Features.IDMS.Remarks.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Web.Models;
using ESG.IDMS.ExcelProcessor.Services;
using ESG.IDMS.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.Remarks;

[Authorize(Policy = Permission.Remarks.View)]
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
		var result = await Mediatr.Send(DataRequest!.ToQuery<GetRemarksQuery>());
		return new JsonResult(result.Data.ToDataTablesResponse(DataRequest, result.TotalCount, result.Data.TotalItemCount));	
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetRemarksQuery>(nameof(RemarksState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Description }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, RemarksState>(BatchUpload.BatchUploadForm, nameof(RemarksState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<RemarksState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
