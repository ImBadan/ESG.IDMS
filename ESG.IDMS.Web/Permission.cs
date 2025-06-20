using System.Reflection;
namespace ESG.IDMS.Web;

public static class Permission
{
    public static IEnumerable<string> GenerateAllPermissions()
	{
		var permissions = new List<string>();
		// Get all nested classes in the Permissions class
		var nestedClasses = typeof(Permission).GetNestedTypes();
		foreach (var nestedClass in nestedClasses)
		{
			// Get all public static string fields in the nested class
			var permissionsInClass = nestedClass.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(string))
				.Select(f => f.GetValue(null)!.ToString());

			permissions.AddRange(permissionsInClass!);
		}
		return permissions.OrderBy(l=>l);
	}
	public static IEnumerable<string> GeneratePermissionsForModule(string module)
	{
		var permissions = new List<string>();
		// Get the nested class for the specified module
		var moduleType = typeof(Permission).GetNestedType(module);
		if (moduleType != null)
		{
			// Get all public static string fields in the module class
			var modulePermissions = moduleType.GetFields(BindingFlags.Public | BindingFlags.Static)
				.Where(f => f.FieldType == typeof(string))
				.Select(f => f.GetValue(null)!.ToString());
			permissions.AddRange(modulePermissions!);
		}     
		return permissions.OrderBy(l => l);
	}

    public static class Admin
    {
        public const string View = "Permission.Admin.View";
    }

    public static class Entities
    {
        public const string View = "Permission.Entities.View";
        public const string Create = "Permission.Entities.Create";
        public const string Edit = "Permission.Entities.Edit";
        public const string Delete = "Permission.Entities.Delete";
    }

    public static class Roles
    {
        public const string View = "Permission.Roles.View";
        public const string Create = "Permission.Roles.Create";
        public const string Edit = "Permission.Roles.Edit";
        public const string Delete = "Permission.Roles.Delete";
    }

    public static class Users
    {
        public const string View = "Permission.Users.View";
        public const string Create = "Permission.Users.Create";
        public const string Edit = "Permission.Users.Edit";
        public const string Delete = "Permission.Users.Delete";
    }

    public static class Apis
    {
        public const string View = "Permission.Apis.View";
        public const string Create = "Permission.Apis.Create";
        public const string Edit = "Permission.Apis.Edit";
        public const string Delete = "Permission.Apis.Delete";
    }

    public static class Applications
    {
        public const string View = "Permission.Applications.View";
        public const string Create = "Permission.Applications.Create";
        public const string Edit = "Permission.Applications.Edit";
        public const string Delete = "Permission.Applications.Delete";
    }

    public static class AuditTrail
    {
        public const string View = "Permission.AuditTrail.View";
        public const string Create = "Permission.AuditTrail.Create";
        public const string Edit = "Permission.AuditTrail.Edit";
        public const string Delete = "Permission.AuditTrail.Delete";
    }
	public static class Report
    {
        public const string View = "Permission.Report.View";
		public const string AIDrivenDataAnalysisAndInsights = "Permission.Report.AIDrivenDataAnalysisAndInsights";
    }
    public static class ReportSetup
    {
        public const string View = "Permission.ReportSetup.View";
        public const string Create = "Permission.ReportSetup.Create";
        public const string Edit = "Permission.ReportSetup.Edit";
        public const string Delete = "Permission.ReportSetup.Delete";
        public const string Approve = "Permission.ReportSetup.Approve";
    }
    public static class FireArms
	{
		public const string View = "Permission.FireArms.View";
		public const string Create = "Permission.FireArms.Create";
		public const string Edit = "Permission.FireArms.Edit";
		public const string Delete = "Permission.FireArms.Delete";
		public const string Upload = "Permission.FireArms.Upload";
		public const string History = "Permission.FireArms.History";
	}
	public static class RadioEquipment
	{
		public const string View = "Permission.RadioEquipment.View";
		public const string Create = "Permission.RadioEquipment.Create";
		public const string Edit = "Permission.RadioEquipment.Edit";
		public const string Delete = "Permission.RadioEquipment.Delete";
		public const string Upload = "Permission.RadioEquipment.Upload";
		public const string History = "Permission.RadioEquipment.History";
	}
	public static class Vehicle
	{
		public const string View = "Permission.Vehicle.View";
		public const string Create = "Permission.Vehicle.Create";
		public const string Edit = "Permission.Vehicle.Edit";
		public const string Delete = "Permission.Vehicle.Delete";
		public const string Upload = "Permission.Vehicle.Upload";
		public const string History = "Permission.Vehicle.History";
	}
	public static class OtherEquipment
	{
		public const string View = "Permission.OtherEquipment.View";
		public const string Create = "Permission.OtherEquipment.Create";
		public const string Edit = "Permission.OtherEquipment.Edit";
		public const string Delete = "Permission.OtherEquipment.Delete";
		public const string Upload = "Permission.OtherEquipment.Upload";
		public const string History = "Permission.OtherEquipment.History";
	}
	public static class FurnitureAndFixture
	{
		public const string View = "Permission.FurnitureAndFixture.View";
		public const string Create = "Permission.FurnitureAndFixture.Create";
		public const string Edit = "Permission.FurnitureAndFixture.Edit";
		public const string Delete = "Permission.FurnitureAndFixture.Delete";
		public const string Upload = "Permission.FurnitureAndFixture.Upload";
		public const string History = "Permission.FurnitureAndFixture.History";
	}
	public static class Location
	{
		public const string View = "Permission.Location.View";
		public const string Create = "Permission.Location.Create";
		public const string Edit = "Permission.Location.Edit";
		public const string Delete = "Permission.Location.Delete";
		public const string Upload = "Permission.Location.Upload";
		public const string History = "Permission.Location.History";
	}
	public static class Status
	{
		public const string View = "Permission.Status.View";
		public const string Create = "Permission.Status.Create";
		public const string Edit = "Permission.Status.Edit";
		public const string Delete = "Permission.Status.Delete";
		public const string Upload = "Permission.Status.Upload";
		public const string History = "Permission.Status.History";
	}
	public static class FireArmsBrandModel
	{
		public const string View = "Permission.FireArmsBrandModel.View";
		public const string Create = "Permission.FireArmsBrandModel.Create";
		public const string Edit = "Permission.FireArmsBrandModel.Edit";
		public const string Delete = "Permission.FireArmsBrandModel.Delete";
		public const string Upload = "Permission.FireArmsBrandModel.Upload";
		public const string History = "Permission.FireArmsBrandModel.History";
	}
	public static class RadioEquipmentBrandModel
	{
		public const string View = "Permission.RadioEquipmentBrandModel.View";
		public const string Create = "Permission.RadioEquipmentBrandModel.Create";
		public const string Edit = "Permission.RadioEquipmentBrandModel.Edit";
		public const string Delete = "Permission.RadioEquipmentBrandModel.Delete";
		public const string Upload = "Permission.RadioEquipmentBrandModel.Upload";
		public const string History = "Permission.RadioEquipmentBrandModel.History";
	}
	public static class VehicleBrandModel
	{
		public const string View = "Permission.VehicleBrandModel.View";
		public const string Create = "Permission.VehicleBrandModel.Create";
		public const string Edit = "Permission.VehicleBrandModel.Edit";
		public const string Delete = "Permission.VehicleBrandModel.Delete";
		public const string Upload = "Permission.VehicleBrandModel.Upload";
		public const string History = "Permission.VehicleBrandModel.History";
	}
	public static class OtherEquipmentBrandModel
	{
		public const string View = "Permission.OtherEquipmentBrandModel.View";
		public const string Create = "Permission.OtherEquipmentBrandModel.Create";
		public const string Edit = "Permission.OtherEquipmentBrandModel.Edit";
		public const string Delete = "Permission.OtherEquipmentBrandModel.Delete";
		public const string Upload = "Permission.OtherEquipmentBrandModel.Upload";
		public const string History = "Permission.OtherEquipmentBrandModel.History";
	}
	public static class FurnitureAndFixtureBrandModel
	{
		public const string View = "Permission.FurnitureAndFixtureBrandModel.View";
		public const string Create = "Permission.FurnitureAndFixtureBrandModel.Create";
		public const string Edit = "Permission.FurnitureAndFixtureBrandModel.Edit";
		public const string Delete = "Permission.FurnitureAndFixtureBrandModel.Delete";
		public const string Upload = "Permission.FurnitureAndFixtureBrandModel.Upload";
		public const string History = "Permission.FurnitureAndFixtureBrandModel.History";
	}
	public static class Remarks
	{
		public const string View = "Permission.Remarks.View";
		public const string Create = "Permission.Remarks.Create";
		public const string Edit = "Permission.Remarks.Edit";
		public const string Delete = "Permission.Remarks.Delete";
		public const string Upload = "Permission.Remarks.Upload";
		public const string History = "Permission.Remarks.History";
	}
	
	
}
