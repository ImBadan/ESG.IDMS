using ESG.IDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.ExcelProcessor.Models;
using ESG.IDMS.ExcelProcessor.Helper;


namespace ESG.IDMS.ExcelProcessor.CustomValidation
{
    public static class VehicleValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            
			var itemDescription = rowValue[nameof(VehicleState.ItemDescription)]?.ToString();
			if (!string.IsNullOrEmpty(itemDescription))
			{
				var itemDescriptionMaxLength = 255;
				if (itemDescription.Length > itemDescriptionMaxLength)
				{
					errorValidation += $"Item Description should be less than {itemDescriptionMaxLength} characters.;";
				}
			}
			var serialNo = rowValue[nameof(VehicleState.SerialNo)]?.ToString();
			if (!string.IsNullOrEmpty(serialNo))
			{
				var serialNoMaxLength = 255;
				if (serialNo.Length > serialNoMaxLength)
				{
					errorValidation += $"SerialNo should be less than {serialNoMaxLength} characters.;";
				}
			}
			var vehicleBrandModelId = rowValue[nameof(VehicleState.VehicleBrandModelId)]?.ToString();
			var vehicleBrandModel = await context.VehicleBrandModel.Where(l => l.Id == vehicleBrandModelId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(vehicleBrandModel == null) {
				errorValidation += $"Vehicle Brand / Model is not valid.; ";
			}
			else
			{
				rowValue[nameof(VehicleState.VehicleBrandModelId)] = vehicleBrandModel?.Id;
			}
			if (!string.IsNullOrEmpty(vehicleBrandModelId))
			{
				var vehicleBrandModelIdMaxLength = 255;
				if (vehicleBrandModelId.Length > vehicleBrandModelIdMaxLength)
				{
					errorValidation += $"Vehicle Brand / Model should be less than {vehicleBrandModelIdMaxLength} characters.;";
				}
			}
			var locationId = rowValue[nameof(VehicleState.LocationId)]?.ToString();
			var location = await context.Location.Where(l => l.Id == locationId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(location == null) {
				errorValidation += $"Location is not valid.; ";
			}
			else
			{
				rowValue[nameof(VehicleState.LocationId)] = location?.Id;
			}
			if (!string.IsNullOrEmpty(locationId))
			{
				var locationIdMaxLength = 255;
				if (locationId.Length > locationIdMaxLength)
				{
					errorValidation += $"Location should be less than {locationIdMaxLength} characters.;";
				}
			}
			var statusId = rowValue[nameof(VehicleState.StatusId)]?.ToString();
			var status = await context.Status.Where(l => l.Id == statusId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(status == null) {
				errorValidation += $"Status is not valid.; ";
			}
			else
			{
				rowValue[nameof(VehicleState.StatusId)] = status?.Id;
			}
			if (!string.IsNullOrEmpty(statusId))
			{
				var statusIdMaxLength = 255;
				if (statusId.Length > statusIdMaxLength)
				{
					errorValidation += $"Status should be less than {statusIdMaxLength} characters.;";
				}
			}
			var remarksId = rowValue[nameof(VehicleState.RemarksId)]?.ToString();
			var remarks = await context.Remarks.Where(l => l.Id == remarksId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(remarks == null) {
				errorValidation += $"Remarks is not valid.; ";
			}
			else
			{
				rowValue[nameof(VehicleState.RemarksId)] = remarks?.Id;
			}
			if (!string.IsNullOrEmpty(remarksId))
			{
				var remarksIdMaxLength = 255;
				if (remarksId.Length > remarksIdMaxLength)
				{
					errorValidation += $"Remarks should be less than {remarksIdMaxLength} characters.;";
				}
			}
			var freeTextRemarks = rowValue[nameof(VehicleState.FreeTextRemarks)]?.ToString();
			if (!string.IsNullOrEmpty(freeTextRemarks))
			{
				var freeTextRemarksMaxLength = 255;
				if (freeTextRemarks.Length > freeTextRemarksMaxLength)
				{
					errorValidation += $"FreeTextRemarks should be less than {freeTextRemarksMaxLength} characters.;";
				}
			}
			
			var issuedBy = rowValue[nameof(VehicleState.IssuedBy)]?.ToString();
			if (!string.IsNullOrEmpty(issuedBy))
			{
				var issuedByMaxLength = 255;
				if (issuedBy.Length > issuedByMaxLength)
				{
					errorValidation += $"Issued By should be less than {issuedByMaxLength} characters.;";
				}
			}
			var issuedTo = rowValue[nameof(VehicleState.IssuedTo)]?.ToString();
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
