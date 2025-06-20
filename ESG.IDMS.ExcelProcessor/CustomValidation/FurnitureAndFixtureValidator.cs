using ESG.IDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.ExcelProcessor.Models;
using ESG.IDMS.ExcelProcessor.Helper;


namespace ESG.IDMS.ExcelProcessor.CustomValidation
{
    public static class FurnitureAndFixtureValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            
			var itemDescription = rowValue[nameof(FurnitureAndFixtureState.ItemDescription)]?.ToString();
			if (!string.IsNullOrEmpty(itemDescription))
			{
				var itemDescriptionMaxLength = 255;
				if (itemDescription.Length > itemDescriptionMaxLength)
				{
					errorValidation += $"Item Description should be less than {itemDescriptionMaxLength} characters.;";
				}
			}
			var serialNo = rowValue[nameof(FurnitureAndFixtureState.SerialNo)]?.ToString();
			if (!string.IsNullOrEmpty(serialNo))
			{
				var serialNoMaxLength = 255;
				if (serialNo.Length > serialNoMaxLength)
				{
					errorValidation += $"SerialNo should be less than {serialNoMaxLength} characters.;";
				}
			}
			var furnitureAndFixtureBrandModelId = rowValue[nameof(FurnitureAndFixtureState.FurnitureAndFixtureBrandModelId)]?.ToString();
			var furnitureAndFixtureBrandModel = await context.FurnitureAndFixtureBrandModel.Where(l => l.Id == furnitureAndFixtureBrandModelId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(furnitureAndFixtureBrandModel == null) {
				errorValidation += $"Furniture and Fixture Brand / Model is not valid.; ";
			}
			else
			{
				rowValue[nameof(FurnitureAndFixtureState.FurnitureAndFixtureBrandModelId)] = furnitureAndFixtureBrandModel?.Id;
			}
			if (!string.IsNullOrEmpty(furnitureAndFixtureBrandModelId))
			{
				var furnitureAndFixtureBrandModelIdMaxLength = 255;
				if (furnitureAndFixtureBrandModelId.Length > furnitureAndFixtureBrandModelIdMaxLength)
				{
					errorValidation += $"Furniture and Fixture Brand / Model should be less than {furnitureAndFixtureBrandModelIdMaxLength} characters.;";
				}
			}
			var locationId = rowValue[nameof(FurnitureAndFixtureState.LocationId)]?.ToString();
			var location = await context.Location.Where(l => l.Id == locationId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(location == null) {
				errorValidation += $"Location is not valid.; ";
			}
			else
			{
				rowValue[nameof(FurnitureAndFixtureState.LocationId)] = location?.Id;
			}
			if (!string.IsNullOrEmpty(locationId))
			{
				var locationIdMaxLength = 255;
				if (locationId.Length > locationIdMaxLength)
				{
					errorValidation += $"Location should be less than {locationIdMaxLength} characters.;";
				}
			}
			var statusId = rowValue[nameof(FurnitureAndFixtureState.StatusId)]?.ToString();
			var status = await context.Status.Where(l => l.Id == statusId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(status == null) {
				errorValidation += $"Status is not valid.; ";
			}
			else
			{
				rowValue[nameof(FurnitureAndFixtureState.StatusId)] = status?.Id;
			}
			if (!string.IsNullOrEmpty(statusId))
			{
				var statusIdMaxLength = 255;
				if (statusId.Length > statusIdMaxLength)
				{
					errorValidation += $"Status should be less than {statusIdMaxLength} characters.;";
				}
			}
			var remarksId = rowValue[nameof(FurnitureAndFixtureState.RemarksId)]?.ToString();
			var remarks = await context.Remarks.Where(l => l.Id == remarksId).AsNoTracking().IgnoreQueryFilters().FirstOrDefaultAsync();
			if(remarks == null) {
				errorValidation += $"Remarks is not valid.; ";
			}
			else
			{
				rowValue[nameof(FurnitureAndFixtureState.RemarksId)] = remarks?.Id;
			}
			if (!string.IsNullOrEmpty(remarksId))
			{
				var remarksIdMaxLength = 255;
				if (remarksId.Length > remarksIdMaxLength)
				{
					errorValidation += $"Remarks should be less than {remarksIdMaxLength} characters.;";
				}
			}
			var freeTextRemarks = rowValue[nameof(FurnitureAndFixtureState.FreeTextRemarks)]?.ToString();
			if (!string.IsNullOrEmpty(freeTextRemarks))
			{
				var freeTextRemarksMaxLength = 255;
				if (freeTextRemarks.Length > freeTextRemarksMaxLength)
				{
					errorValidation += $"FreeTextRemarks should be less than {freeTextRemarksMaxLength} characters.;";
				}
			}
			
			var issuedBy = rowValue[nameof(FurnitureAndFixtureState.IssuedBy)]?.ToString();
			if (!string.IsNullOrEmpty(issuedBy))
			{
				var issuedByMaxLength = 255;
				if (issuedBy.Length > issuedByMaxLength)
				{
					errorValidation += $"Issued By should be less than {issuedByMaxLength} characters.;";
				}
			}
			var issuedTo = rowValue[nameof(FurnitureAndFixtureState.IssuedTo)]?.ToString();
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
