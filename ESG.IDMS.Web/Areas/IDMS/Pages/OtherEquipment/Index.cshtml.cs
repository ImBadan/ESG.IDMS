using ESG.IDMS.Application.Features.IDMS.OtherEquipment.Queries;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Web.Models;
using ESG.IDMS.ExcelProcessor.Services;
using ESG.IDMS.Web.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ESG.IDMS.Web.Areas.IDMS.Pages.OtherEquipment;

[Authorize(Policy = Permission.OtherEquipment.View)]
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
		var result = await Mediatr.Send(DataRequest!.ToQuery<GetOtherEquipmentQuery>());
		return new JsonResult(result.Data.ToDataTablesResponse(DataRequest, result.TotalCount, result.Data.TotalItemCount));	
    } 
	
	public async Task<IActionResult> OnGetSelect2Data([FromQuery] Select2Request request)
    {
        var result = await Mediatr.Send(request.ToQuery<GetOtherEquipmentQuery>(nameof(OtherEquipmentState.Id)));
        return new JsonResult(result.ToSelect2Response(e => new Select2Result { Id = e.Id, Text = e.Id }));
    }
	public async Task<IActionResult> OnPostBatchUploadAsync()
    {        
        return await BatchUploadAsync<IndexModel, OtherEquipmentState>(BatchUpload.BatchUploadForm, nameof(OtherEquipmentState), "Index");
    }

    public IActionResult OnPostDownloadTemplate()
    {
        ModelState.Clear();
		BatchUpload.BatchUploadFileName = ExcelService.ExportTemplate<OtherEquipmentState>(_uploadPath + "\\" + WebConstants.ExcelTemplateSubFolder);
        NotyfService.Success(Localizer["Successfully downloaded upload template."]);
        return Page();
    }
}
