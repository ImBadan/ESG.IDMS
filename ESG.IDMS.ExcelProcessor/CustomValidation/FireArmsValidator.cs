using ESG.IDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.ExcelProcessor.Models;
using ESG.IDMS.ExcelProcessor.Helper;


namespace ESG.IDMS.ExcelProcessor.CustomValidation
{
    public static class FireArmsValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            
			var itemDescription = rowValue[nameof(FireArmsState.ItemDescription)]?.ToString();
			if (!string.IsNullOrEmpty(itemDescription))
			{
				var itemDescriptionMaxLength = 255;
				if (itemDescription.Length > itemDescriptionMaxLength)
				{
					errorValidation += $"Item Description should be less than {itemDescriptionMaxLength} characters.;";
				}
			}
			var serialNo = rowValue[nameof(FireArmsState.SerialNo)]?.ToString();
			if (!string.IsNullOrEmpty(serialNo))
			{
				var serialNoMaxLength = 255;
				if (serialNo.Length > serialNoMaxLength)
				{
					errorValidation += $"SerialNo should be less than {serialNoMaxLength} characters.;";
				}
			}
			var fireArmsBrandModelId = rowValue[nameof(FireArmsState.FireArmsBrandModelId)]?.ToString();
			var fireArmsBrandModel = await context.FireArmsBrandModel.Where(l => l.Id == fireArmsBrandModelId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(fireArmsBrandModel == null) {
				errorValidation += $"Firearms Brand / Model is not valid.; ";
			}
			else
			{
				rowValue[nameof(FireArmsState.FireArmsBrandModelId)] = fireArmsBrandModel?.Id;
			}
			if (!string.IsNullOrEmpty(fireArmsBrandModelId))
			{
				var fireArmsBrandModelIdMaxLength = 255;
				if (fireArmsBrandModelId.Length > fireArmsBrandModelIdMaxLength)
				{
					errorValidation += $"Firearms Brand / Model should be less than {fireArmsBrandModelIdMaxLength} characters.;";
				}
			}
			var locationId = rowValue[nameof(FireArmsState.LocationId)]?.ToString();
			var location = await context.Location.Where(l => l.Id == locationId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(location == null) {
				errorValidation += $"Location is not valid.; ";
			}
			else
			{
				rowValue[nameof(FireArmsState.LocationId)] = location?.Id;
			}
			if (!string.IsNullOrEmpty(locationId))
			{
				var locationIdMaxLength = 255;
				if (locationId.Length > locationIdMaxLength)
				{
					errorValidation += $"Location should be less than {locationIdMaxLength} characters.;";
				}
			}
			var statusId = rowValue[nameof(FireArmsState.StatusId)]?.ToString();
			var status = await context.Status.Where(l => l.Id == statusId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(status == null) {
				errorValidation += $"Status is not valid.; ";
			}
			else
			{
				rowValue[nameof(FireArmsState.StatusId)] = status?.Id;
			}
			if (!string.IsNullOrEmpty(statusId))
			{
				var statusIdMaxLength = 255;
				if (statusId.Length > statusIdMaxLength)
				{
					errorValidation += $"Status should be less than {statusIdMaxLength} characters.;";
				}
			}
			var remarksId = rowValue[nameof(FireArmsState.RemarksId)]?.ToString();
			var remarks = await context.Remarks.Where(l => l.Id == remarksId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(remarks == null) {
				errorValidation += $"Remarks is not valid.; ";
			}
			else
			{
				rowValue[nameof(FireArmsState.RemarksId)] = remarks?.Id;
			}
			if (!string.IsNullOrEmpty(remarksId))
			{
				var remarksIdMaxLength = 255;
				if (remarksId.Length > remarksIdMaxLength)
				{
					errorValidation += $"Remarks should be less than {remarksIdMaxLength} characters.;";
				}
			}
			var freeTextRemarks = rowValue[nameof(FireArmsState.FreeTextRemarks)]?.ToString();
			if (!string.IsNullOrEmpty(freeTextRemarks))
			{
				var freeTextRemarksMaxLength = 255;
				if (freeTextRemarks.Length > freeTextRemarksMaxLength)
				{
					errorValidation += $"FreeTextRemarks should be less than {freeTextRemarksMaxLength} characters.;";
				}
			}
			
			var issuedBy = rowValue[nameof(FireArmsState.IssuedBy)]?.ToString();
			if (!string.IsNullOrEmpty(issuedBy))
			{
				var issuedByMaxLength = 255;
				if (issuedBy.Length > issuedByMaxLength)
				{
					errorValidation += $"Issued By should be less than {issuedByMaxLength} characters.;";
				}
			}
			var issuedTo = rowValue[nameof(FireArmsState.IssuedTo)]?.ToString();
			if (!string.IsNullOrEmpty(issuedTo))
			{
				var issuedToMaxLength = 255;
				if (issuedTo.Length > issuedToMaxLength)
				{
					errorValidation += $"Issued To should be less than {issuedToMaxLength} characters.;";
				}
			}
			
			
			if (!string.IsNullOrEmpty(errorValidation))
			{
				throw new Exception(errorValidation);
			}
            return rowValue;
        }
			
		public static Dictionary<string, HashSet<int>> DuplicateValidation(List<ExcelRecord> records)
		{
			List<string> listOfKeys = [
																																																								
			];
			return listOfKeys.Count > 0 ? DictionaryHelper.FindDuplicateRowNumbersPerKey(records, listOfKeys) : [];
		}
    }
}
