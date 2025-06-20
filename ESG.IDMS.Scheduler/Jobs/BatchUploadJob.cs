using ESG.Common.Services.Shared.Interfaces;
using ESG.Common.Services.Shared.Models.Mail;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.ExcelProcessor.Services;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using ESG.IDMS.ExcelProcessor.CustomTemplate;
using ESG.IDMS.ExcelProcessor.CustomProcessor;

namespace ESG.IDMS.Scheduler.Jobs
{
    [DisallowConcurrentExecution]
    public class BatchUploadJob(ApplicationContext context, ILogger<BatchUploadJob> logger, IConfiguration configuration, IMailService emailSender, ExcelService excelService, IdentityContext identityContext) : IJob
    {
        private readonly string? _uploadPath = configuration.GetValue<string>("UsersUpload:UploadFilesPath");

        public async Task Execute(IJobExecutionContext context)
        {
            await ProcessBatchUploadAsync();
        }
        private async Task ProcessBatchUploadAsync()
        {
            var uploadProcessorList = await context.UploadProcessor.Where(l => l.Status == Core.Constants.FileUploadStatus.Pending).IgnoreQueryFilters().AsNoTracking()
                .OrderBy(l => l.CreatedDate).ToListAsync();
            var exceptionFilePath = "";
            foreach (var item in uploadProcessorList)
            {
                try
                {
                    //Tag Start Date/Time
                    item.SetStart();
                    context.Update(item);
                    await context.UpdateBatchRecordAsync(item.CreatedBy, item);
                    //Start Processing
                    exceptionFilePath = await ValidateBatchUpload(item.Module, item.Path, item.CreatedBy!);
                    if (string.IsNullOrEmpty(exceptionFilePath))
                    {
                        item.SetDone();
                    }
                    else
                    {
                        item.SetFailed(exceptionFilePath, "Error from the file.");
                    }
                    context.Update(item);
                    await context.UpdateBatchRecordAsync(item.CreatedBy, item);
                }
                catch (Exception ex)
                {
					context.DetachAllTrackedEntities();
                    logger.LogError(ex, @"ProcessBatchUploadAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                    item.SetFailed("", ex.Message);
                    context.Update(item);
                    await context.UpdateBatchRecordAsync(item.CreatedBy, item);
                }
                try
                {
                    if (!string.IsNullOrEmpty(exceptionFilePath))
                    {
                        var email = (await identityContext.Users.Where(l => l.Id == item.CreatedBy).AsNoTracking().FirstOrDefaultAsync())!.Email!;
                        await SendValidatedBatchUploadFile(email, item.Module, exceptionFilePath);
                    }
                }
                catch (Exception ex)
                {               
                    logger.LogError(ex, @"ProcessBatchUploadAsync Error Message : {Message} / StackTrace : {StackTrace}", ex.Message, ex.StackTrace);
                }              
            }
        }
		private async Task<string?> ValidateBatchUpload(string module, string path, string processedByUserId)
        {
            string? exceptionFilePath = null;   
            switch (module)
            {
                case nameof(FireArmsState):
					var fireArmsImportResult = await excelService.ImportAsync<FireArmsState>(path);
					if (fireArmsImportResult.IsSuccess)
					{
						await context.AddRangeAsync(fireArmsImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<FireArmsState>(fireArmsImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(RadioEquipmentState):
					var radioEquipmentImportResult = await excelService.ImportAsync<RadioEquipmentState>(path);
					if (radioEquipmentImportResult.IsSuccess)
					{
						await context.AddRangeAsync(radioEquipmentImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<RadioEquipmentState>(radioEquipmentImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(VehicleState):
					var vehicleImportResult = await excelService.ImportAsync<VehicleState>(path);
					if (vehicleImportResult.IsSuccess)
					{
						await context.AddRangeAsync(vehicleImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<VehicleState>(vehicleImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(OtherEquipmentState):
					var otherEquipmentImportResult = await excelService.ImportAsync<OtherEquipmentState>(path);
					if (otherEquipmentImportResult.IsSuccess)
					{
						await context.AddRangeAsync(otherEquipmentImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<OtherEquipmentState>(otherEquipmentImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(FurnitureAndFixtureState):
					var furnitureAndFixtureImportResult = await excelService.ImportAsync<FurnitureAndFixtureState>(path);
					if (furnitureAndFixtureImportResult.IsSuccess)
					{
						await context.AddRangeAsync(furnitureAndFixtureImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<FurnitureAndFixtureState>(furnitureAndFixtureImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(LocationState):
					var locationImportResult = await excelService.ImportAsync<LocationState>(path);
					if (locationImportResult.IsSuccess)
					{
						await context.AddRangeAsync(locationImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<LocationState>(locationImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(StatusState):
					var statusImportResult = await excelService.ImportAsync<StatusState>(path);
					if (statusImportResult.IsSuccess)
					{
						await context.AddRangeAsync(statusImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<StatusState>(statusImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(FireArmsBrandModelState):
					var fireArmsBrandModelImportResult = await excelService.ImportAsync<FireArmsBrandModelState>(path);
					if (fireArmsBrandModelImportResult.IsSuccess)
					{
						await context.AddRangeAsync(fireArmsBrandModelImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<FireArmsBrandModelState>(fireArmsBrandModelImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(RadioEquipmentBrandModelState):
					var radioEquipmentBrandModelImportResult = await excelService.ImportAsync<RadioEquipmentBrandModelState>(path);
					if (radioEquipmentBrandModelImportResult.IsSuccess)
					{
						await context.AddRangeAsync(radioEquipmentBrandModelImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<RadioEquipmentBrandModelState>(radioEquipmentBrandModelImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(VehicleBrandModelState):
					var vehicleBrandModelImportResult = await excelService.ImportAsync<VehicleBrandModelState>(path);
					if (vehicleBrandModelImportResult.IsSuccess)
					{
						await context.AddRangeAsync(vehicleBrandModelImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<VehicleBrandModelState>(vehicleBrandModelImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(OtherEquipmentBrandModelState):
					var otherEquipmentBrandModelImportResult = await excelService.ImportAsync<OtherEquipmentBrandModelState>(path);
					if (otherEquipmentBrandModelImportResult.IsSuccess)
					{
						await context.AddRangeAsync(otherEquipmentBrandModelImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<OtherEquipmentBrandModelState>(otherEquipmentBrandModelImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(FurnitureAndFixtureBrandModelState):
					var furnitureAndFixtureBrandModelImportResult = await excelService.ImportAsync<FurnitureAndFixtureBrandModelState>(path);
					if (furnitureAndFixtureBrandModelImportResult.IsSuccess)
					{
						await context.AddRangeAsync(furnitureAndFixtureBrandModelImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<FurnitureAndFixtureBrandModelState>(furnitureAndFixtureBrandModelImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				case nameof(RemarksState):
					var remarksImportResult = await excelService.ImportAsync<RemarksState>(path);
					if (remarksImportResult.IsSuccess)
					{
						await context.AddRangeAsync(remarksImportResult.SuccessRecords);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<RemarksState>(remarksImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
				
                case nameof(SampleTableState): //Sample Only For Custom Processing
					var unitImportResult = await excelService.ImportAsync<SampleTableState>(path);
					if (unitImportResult.IsSuccess)
					{
						await SampleTableCustomProcessor.Process(context, unitImportResult.SuccessRecords, processedByUserId);
					}
					else
					{
						exceptionFilePath = ExcelService.UpdateExistingExcelValidationResult<SampleTableState>(unitImportResult.FailedRecords, _uploadPath + "\\BatchUploadErrors", path);
					}
					break;
                default: break;
            }           
            return exceptionFilePath;
        }
        
        public async Task SendValidatedBatchUploadFile(string email, string module, string exceptionFilePath)
        {
            string wordToRemove = "State";
            if (module.EndsWith(wordToRemove))
            {
                module = module[..^wordToRemove.Length];
            }
            string subject = $"Batch Upload - " + module;
            string message = GenerateEmailBody();
            var emailRequest = new MailRequest()
            {
                Subject = subject,
                Body = message,
                Attachments = [ exceptionFilePath ],              
                To = email
            };           
            await emailSender.SendAsync(emailRequest);
        }
        private static string GenerateEmailBody()
        {
            string str = "<span style='font-size:10pt; font-family:Arial;'> ";
            str += "Your uploaded file has been failed on processing. Please see attached file for the validation remarks.";
            str += "<br />";           
            str += "</span> ";
            return str;
        }
    }
}
