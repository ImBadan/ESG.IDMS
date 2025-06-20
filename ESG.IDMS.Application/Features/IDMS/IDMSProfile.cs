using AutoMapper;
using ESG.Common.Core.Mapping;

using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Application.Features.IDMS.Report.Commands;
using ESG.IDMS.Application.Features.IDMS.FireArms.Commands;
using ESG.IDMS.Application.Features.IDMS.RadioEquipment.Commands;
using ESG.IDMS.Application.Features.IDMS.Vehicle.Commands;
using ESG.IDMS.Application.Features.IDMS.OtherEquipment.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixture.Commands;
using ESG.IDMS.Application.Features.IDMS.Location.Commands;
using ESG.IDMS.Application.Features.IDMS.Status.Commands;
using ESG.IDMS.Application.Features.IDMS.FireArmsBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.RadioEquipmentBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.VehicleBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.OtherEquipmentBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.FurnitureAndFixtureBrandModel.Commands;
using ESG.IDMS.Application.Features.IDMS.Remarks.Commands;



namespace ESG.IDMS.Application.Features.IDMS;

public class IDMSProfile : Profile
{
    public IDMSProfile()
    {
		CreateMap<AddReportCommand, ReportState>();
        CreateMap<EditReportCommand, ReportState>().IgnoreBaseEntityProperties();
		CreateMap<AddReportAnalyticsCommand, ReportAIIntegrationState>();
		CreateMap<AddReportWithSQLQueryFromAICommand, ReportState>();
		
        CreateMap<AddFireArmsCommand, FireArmsState>();
		CreateMap <EditFireArmsCommand, FireArmsState>().IgnoreBaseEntityProperties();
		CreateMap<AddRadioEquipmentCommand, RadioEquipmentState>();
		CreateMap <EditRadioEquipmentCommand, RadioEquipmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddVehicleCommand, VehicleState>();
		CreateMap <EditVehicleCommand, VehicleState>().IgnoreBaseEntityProperties();
		CreateMap<AddOtherEquipmentCommand, OtherEquipmentState>();
		CreateMap <EditOtherEquipmentCommand, OtherEquipmentState>().IgnoreBaseEntityProperties();
		CreateMap<AddFurnitureAndFixtureCommand, FurnitureAndFixtureState>();
		CreateMap <EditFurnitureAndFixtureCommand, FurnitureAndFixtureState>().IgnoreBaseEntityProperties();
		CreateMap<AddLocationCommand, LocationState>();
		CreateMap <EditLocationCommand, LocationState>().IgnoreBaseEntityProperties();
		CreateMap<AddStatusCommand, StatusState>();
		CreateMap <EditStatusCommand, StatusState>().IgnoreBaseEntityProperties();
		CreateMap<AddFireArmsBrandModelCommand, FireArmsBrandModelState>();
		CreateMap <EditFireArmsBrandModelCommand, FireArmsBrandModelState>().IgnoreBaseEntityProperties();
		CreateMap<AddRadioEquipmentBrandModelCommand, RadioEquipmentBrandModelState>();
		CreateMap <EditRadioEquipmentBrandModelCommand, RadioEquipmentBrandModelState>().IgnoreBaseEntityProperties();
		CreateMap<AddVehicleBrandModelCommand, VehicleBrandModelState>();
		CreateMap <EditVehicleBrandModelCommand, VehicleBrandModelState>().IgnoreBaseEntityProperties();
		CreateMap<AddOtherEquipmentBrandModelCommand, OtherEquipmentBrandModelState>();
		CreateMap <EditOtherEquipmentBrandModelCommand, OtherEquipmentBrandModelState>().IgnoreBaseEntityProperties();
		CreateMap<AddFurnitureAndFixtureBrandModelCommand, FurnitureAndFixtureBrandModelState>();
		CreateMap <EditFurnitureAndFixtureBrandModelCommand, FurnitureAndFixtureBrandModelState>().IgnoreBaseEntityProperties();
		CreateMap<AddRemarksCommand, RemarksState>();
		CreateMap <EditRemarksCommand, RemarksState>().IgnoreBaseEntityProperties();
		
		
    }
}
