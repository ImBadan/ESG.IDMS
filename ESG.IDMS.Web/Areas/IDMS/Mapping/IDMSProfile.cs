using AutoMapper;
using ESG.IDMS.Core.IDMS;
using ESG.IDMS.Web.Areas.IDMS.Models;
using ESG.IDMS.Application.Features.IDMS.Report.Commands;
using ESG.IDMS.Application.DTOs;

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


namespace ESG.IDMS.Web.Areas.IDMS.Mapping;

public class IDMSProfile : Profile
{
    public IDMSProfile()
    {
		CreateMap<ReportViewModel, AddReportCommand>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportViewModel, EditReportCommand>()
           .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => new ReportRoleAssignmentState { RoleName = x })));
        CreateMap<ReportState, ReportViewModel>()
            .ForMember(dest => dest.ReportRoleAssignmentList, opt => opt.MapFrom(src => src.ReportRoleAssignmentList!.Select(x => x.RoleName)));
        CreateMap<ReportViewModel, ReportState>();      
        CreateMap<ReportQueryFilterState, ReportQueryFilterViewModel>().ForPath(e => e.ForeignKeyReport, o => o.MapFrom(s => s.Report!.ReportName));
        CreateMap<ReportQueryFilterViewModel, ReportQueryFilterState>();
        CreateMap<ReportResultModel, ReportResultViewModel>().ReverseMap();
        CreateMap<ReportQueryFilterModel, ReportQueryFilterViewModel>().ReverseMap();
		CreateMap<ReportViewModel, AddReportWithSQLQueryFromAICommand>();
		
        CreateMap<FireArmsViewModel, AddFireArmsCommand>();
		CreateMap<FireArmsViewModel, EditFireArmsCommand>();
		CreateMap<FireArmsState, FireArmsViewModel>().ForPath(e => e.ReferenceFieldRemarksId, o => o.MapFrom(s => s.Remarks!.Id)).ForPath(e => e.ReferenceFieldLocationId, o => o.MapFrom(s => s.Location!.Id)).ForPath(e => e.ReferenceFieldStatusId, o => o.MapFrom(s => s.Status!.Id)).ForPath(e => e.ReferenceFieldFireArmsBrandModelId, o => o.MapFrom(s => s.FireArmsBrandModel!.Id));
		CreateMap<FireArmsViewModel, FireArmsState>();
		CreateMap<RadioEquipmentViewModel, AddRadioEquipmentCommand>();
		CreateMap<RadioEquipmentViewModel, EditRadioEquipmentCommand>();
		CreateMap<RadioEquipmentState, RadioEquipmentViewModel>().ForPath(e => e.ReferenceFieldStatusId, o => o.MapFrom(s => s.Status!.Id)).ForPath(e => e.ReferenceFieldRadioEquipmentBrandModelId, o => o.MapFrom(s => s.RadioEquipmentBrandModel!.Id)).ForPath(e => e.ReferenceFieldRemarksId, o => o.MapFrom(s => s.Remarks!.Id)).ForPath(e => e.ReferenceFieldLocationId, o => o.MapFrom(s => s.Location!.Id));
		CreateMap<RadioEquipmentViewModel, RadioEquipmentState>();
		CreateMap<VehicleViewModel, AddVehicleCommand>();
		CreateMap<VehicleViewModel, EditVehicleCommand>();
		CreateMap<VehicleState, VehicleViewModel>().ForPath(e => e.ReferenceFieldStatusId, o => o.MapFrom(s => s.Status!.Id)).ForPath(e => e.ReferenceFieldVehicleBrandModelId, o => o.MapFrom(s => s.VehicleBrandModel!.Id)).ForPath(e => e.ReferenceFieldRemarksId, o => o.MapFrom(s => s.Remarks!.Id)).ForPath(e => e.ReferenceFieldLocationId, o => o.MapFrom(s => s.Location!.Id));
		CreateMap<VehicleViewModel, VehicleState>();
		CreateMap<OtherEquipmentViewModel, AddOtherEquipmentCommand>();
		CreateMap<OtherEquipmentViewModel, EditOtherEquipmentCommand>();
		CreateMap<OtherEquipmentState, OtherEquipmentViewModel>().ForPath(e => e.ReferenceFieldRemarksId, o => o.MapFrom(s => s.Remarks!.Id)).ForPath(e => e.ReferenceFieldOtherEquipmentBrandModelId, o => o.MapFrom(s => s.OtherEquipmentBrandModel!.Id)).ForPath(e => e.ReferenceFieldStatusId, o => o.MapFrom(s => s.Status!.Id)).ForPath(e => e.ReferenceFieldLocationId, o => o.MapFrom(s => s.Location!.Id));
		CreateMap<OtherEquipmentViewModel, OtherEquipmentState>();
		CreateMap<FurnitureAndFixtureViewModel, AddFurnitureAndFixtureCommand>();
		CreateMap<FurnitureAndFixtureViewModel, EditFurnitureAndFixtureCommand>();
		CreateMap<FurnitureAndFixtureState, FurnitureAndFixtureViewModel>().ForPath(e => e.ReferenceFieldRemarksId, o => o.MapFrom(s => s.Remarks!.Id)).ForPath(e => e.ReferenceFieldLocationId, o => o.MapFrom(s => s.Location!.Id)).ForPath(e => e.ReferenceFieldFurnitureAndFixtureBrandModelId, o => o.MapFrom(s => s.FurnitureAndFixtureBrandModel!.Id)).ForPath(e => e.ReferenceFieldStatusId, o => o.MapFrom(s => s.Status!.Id));
		CreateMap<FurnitureAndFixtureViewModel, FurnitureAndFixtureState>();
		CreateMap<LocationViewModel, AddLocationCommand>();
		CreateMap<LocationViewModel, EditLocationCommand>();
		CreateMap<LocationState, LocationViewModel>().ReverseMap();
		CreateMap<StatusViewModel, AddStatusCommand>();
		CreateMap<StatusViewModel, EditStatusCommand>();
		CreateMap<StatusState, StatusViewModel>().ReverseMap();
		CreateMap<FireArmsBrandModelViewModel, AddFireArmsBrandModelCommand>();
		CreateMap<FireArmsBrandModelViewModel, EditFireArmsBrandModelCommand>();
		CreateMap<FireArmsBrandModelState, FireArmsBrandModelViewModel>().ReverseMap();
		CreateMap<RadioEquipmentBrandModelViewModel, AddRadioEquipmentBrandModelCommand>();
		CreateMap<RadioEquipmentBrandModelViewModel, EditRadioEquipmentBrandModelCommand>();
		CreateMap<RadioEquipmentBrandModelState, RadioEquipmentBrandModelViewModel>().ReverseMap();
		CreateMap<VehicleBrandModelViewModel, AddVehicleBrandModelCommand>();
		CreateMap<VehicleBrandModelViewModel, EditVehicleBrandModelCommand>();
		CreateMap<VehicleBrandModelState, VehicleBrandModelViewModel>().ReverseMap();
		CreateMap<OtherEquipmentBrandModelViewModel, AddOtherEquipmentBrandModelCommand>();
		CreateMap<OtherEquipmentBrandModelViewModel, EditOtherEquipmentBrandModelCommand>();
		CreateMap<OtherEquipmentBrandModelState, OtherEquipmentBrandModelViewModel>().ReverseMap();
		CreateMap<FurnitureAndFixtureBrandModelViewModel, AddFurnitureAndFixtureBrandModelCommand>();
		CreateMap<FurnitureAndFixtureBrandModelViewModel, EditFurnitureAndFixtureBrandModelCommand>();
		CreateMap<FurnitureAndFixtureBrandModelState, FurnitureAndFixtureBrandModelViewModel>().ReverseMap();
		CreateMap<RemarksViewModel, AddRemarksCommand>();
		CreateMap<RemarksViewModel, EditRemarksCommand>();
		CreateMap<RemarksState, RemarksViewModel>().ReverseMap();
		
		
    }
}
