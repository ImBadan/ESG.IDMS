using ESG.IDMS.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.ExcelProcessor.Models;
using ESG.IDMS.ExcelProcessor.Helper;


namespace ESG.IDMS.ExcelProcessor.CustomValidation
{
    public static class FurnitureAndFixtureBrandModelValidator
    {
        public static async Task<Dictionary<string, object?>>  ValidatePerRecordAsync(ApplicationContext context, Dictionary<string, object?> rowValue)
        {
			string errorValidation = "";
            var description = rowValue[nameof(FurnitureAndFixtureBrandModelState.Description)]?.ToString();
			if (!string.IsNullOrEmpty(description))
			{
				var descriptionMaxLength = 255;
				if (description.Length > descriptionMaxLength)
				{
					errorValidation += $"Description should be less than {descriptionMaxLength} characters.;";
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
