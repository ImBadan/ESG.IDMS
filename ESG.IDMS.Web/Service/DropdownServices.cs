using Microsoft.AspNetCore.Mvc.Rendering;
using ESG.IDMS.Infrastructure.Data;
using ESG.IDMS.Core.IDMS;
using ESG.Common.Data;
using ESG.IDMS.Web.Areas.Admin.Queries.Users;
using MediatR;
using ESG.IDMS.Web.Areas.Admin.Queries.Roles;
using ESG.IDMS.Application.Features.IDMS.Report.Queries;
using System.Globalization;

namespace ESG.IDMS.Web.Service
{
    public class DropdownServices(ApplicationContext context, IMediator mediaTr)
    {
		private readonly ApplicationContext _context = context;
		private readonly IMediator _mediaTr = mediaTr;

        public async Task<IEnumerable<SelectListItem>> GetRoleList()
		{
			var query = new GetRolesQuery()
			{
				PageSize = -1
			};
			return (await _mediaTr.Send(query)).Data.Select(l => new SelectListItem() { Value = l.Name, Text = l.Name });
		}		
        public IEnumerable<SelectListItem> QueryTypeList()
        {
            IList<SelectListItem> items =
            [
                //new() { Text = Core.Constants.QueryType.QueryBuilder, Value = Core.Constants.QueryType.QueryBuilder, },
                new() { Text = Core.Constants.QueryType.TSql, Value = Core.Constants.QueryType.TSql, }
            ];
            return items;
        }
        public IEnumerable<SelectListItem> ReportChartTypeList()
        {
            IList<SelectListItem> items =
            [
                new() { Text = Core.Constants.ReportChartType.HorizontalBar, Value = Core.Constants.ReportChartType.HorizontalBar, },
                new() { Text = Core.Constants.ReportChartType.Pie, Value = Core.Constants.ReportChartType.Pie, },
                new() { Text = Core.Constants.ReportChartType.Table, Value = Core.Constants.ReportChartType.Table, },
            ];
            return items;
        }
        public IEnumerable<SelectListItem> DataTypeList()
        {
            IList<SelectListItem> items =
            [
                new() { Text = Core.Constants.DataTypes.CustomDropdown, Value = Core.Constants.DataTypes.CustomDropdown, },
                new() { Text = Core.Constants.DataTypes.Date, Value = Core.Constants.DataTypes.Date, },
                new() { Text = Core.Constants.DataTypes.DropdownFromTable, Value = Core.Constants.DataTypes.DropdownFromTable, },
                new() { Text = Core.Constants.DataTypes.Months, Value = Core.Constants.DataTypes.Months, },
                new() { Text = Core.Constants.DataTypes.Years, Value = Core.Constants.DataTypes.Years, },
            ];
            return items;
        }
        public IEnumerable<SelectListItem> GetDropdownFromCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return [];
            }
            return value.Split(',')
                         .Select(option => new SelectListItem() { Text = option.Trim(), Value = option.Trim() })
                         .ToList();
        }
        public IEnumerable<SelectListItem> GetYearsList(int yearsPrevious, int yearsAdvance)
        {
            List<SelectListItem> yearsList = [];
            int currentYear = DateTime.Now.Year;
            int startYear = currentYear - yearsPrevious;
            int endYear = currentYear + yearsAdvance;
            for (int year = startYear; year <= endYear; year++)
            {
                SelectListItem listItem = new()
                {
                    Text = year.ToString(),
                    Value = year.ToString(),
                };
                yearsList.Add(listItem);
            }
            return yearsList;
        }
        public IEnumerable<SelectListItem> GetMonthsList()
        {
            List<SelectListItem> monthsList = [];
            // Loop through the months and create SelectListItem objects for each month
            for (int month = 1; month <= 12; month++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                SelectListItem listItem = new()
                {
                    Text = monthName,
                    Value = month.ToString() // Month number as the 'Value'
                };
                monthsList.Add(listItem);
            }
            return monthsList;
        }
        public async Task<IEnumerable<SelectListItem>> GetDropdownFromTableKeyValue(string tableKeyValue, string? filter)
        {
            var dropdownValues = await _mediaTr.Send(new GetDropdownValuesQuery(tableKeyValue, filter));
            List<SelectListItem> selectListItems = [];
            foreach (var item in dropdownValues)
            {
                string? key = item.TryGetValue("Key", out string? keyOutput) ? keyOutput : "";
                string? value = item.TryGetValue("Value", out string? valueOutput) ? valueOutput : "";
                selectListItems.Add(new()
                {
                    Text = value,
                    Value = key
                });
            }
            return selectListItems;
        }
        public async Task<IList<Dictionary<string, string>>> GetReportList()
        {
            return await _mediaTr.Send(new GetReportListQuery());
        }
        public SelectList GetRemarksList(string? id)
		{
			return _context.GetSingle<RemarksState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetLocationList(string? id)
		{
			return _context.GetSingle<LocationState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetStatusList(string? id)
		{
			return _context.GetSingle<StatusState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetFireArmsBrandModelList(string? id)
		{
			return _context.GetSingle<FireArmsBrandModelState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetRadioEquipmentBrandModelList(string? id)
		{
			return _context.GetSingle<RadioEquipmentBrandModelState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetVehicleBrandModelList(string? id)
		{
			return _context.GetSingle<VehicleBrandModelState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetFurnitureAndFixtureBrandModelList(string? id)
		{
			return _context.GetSingle<FurnitureAndFixtureBrandModelState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		public SelectList GetOtherEquipmentBrandModelList(string? id)
		{
			return _context.GetSingle<OtherEquipmentBrandModelState>(e => e.Id == id, new()).Result.Match(
				Some: e => new SelectList(new List<SelectListItem> { new() { Value = e.Id, Text = e.Description } }, "Value", "Text", e.Id),
				None: () => new SelectList(new List<SelectListItem>(), "Value", "Text")
			);
		}
		
        
    }
}

// Note: The code above is a service class that provides methods to retrieve dropdown lists and other related data for the application.