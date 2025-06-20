using AutoMapper;
using ESG.IDMS.API.Controllers.v1;
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


namespace ESG.IDMS.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<FireArmsViewModel, AddFireArmsCommand>();
		CreateMap <FireArmsViewModel, EditFireArmsCommand>();
		CreateMap<RadioEquipmentViewModel, AddRadioEquipmentCommand>();
		CreateMap <RadioEquipmentViewModel, EditRadioEquipmentCommand>();
		CreateMap<VehicleViewModel, AddVehicleCommand>();
		CreateMap <VehicleViewModel, EditVehicleCommand>();
		CreateMap<OtherEquipmentViewModel, AddOtherEquipmentCommand>();
		CreateMap <OtherEquipmentViewModel, EditOtherEquipmentCommand>();
		CreateMap<FurnitureAndFixtureViewModel, AddFurnitureAndFixtureCommand>();
		CreateMap <FurnitureAndFixtureViewModel, EditFurnitureAndFixtureCommand>();
		CreateMap<LocationViewModel, AddLocationCommand>();
		CreateMap <LocationViewModel, EditLocationCommand>();
		CreateMap<StatusViewModel, AddStatusCommand>();
		CreateMap <StatusViewModel, EditStatusCommand>();
		CreateMap<FireArmsBrandModelViewModel, AddFireArmsBrandModelCommand>();
		CreateMap <FireArmsBrandModelViewModel, EditFireArmsBrandModelCommand>();
		CreateMap<RadioEquipmentBrandModelViewModel, AddRadioEquipmentBrandModelCommand>();
		CreateMap <RadioEquipmentBrandModelViewModel, EditRadioEquipmentBrandModelCommand>();
		CreateMap<VehicleBrandModelViewModel, AddVehicleBrandModelCommand>();
		CreateMap <VehicleBrandModelViewModel, EditVehicleBrandModelCommand>();
		CreateMap<OtherEquipmentBrandModelViewModel, AddOtherEquipmentBrandModelCommand>();
		CreateMap <OtherEquipmentBrandModelViewModel, EditOtherEquipmentBrandModelCommand>();
		CreateMap<FurnitureAndFixtureBrandModelViewModel, AddFurnitureAndFixtureBrandModelCommand>();
		CreateMap <FurnitureAndFixtureBrandModelViewModel, EditFurnitureAndFixtureBrandModelCommand>();
		CreateMap<RemarksViewModel, AddRemarksCommand>();
		CreateMap <RemarksViewModel, EditRemarksCommand>();
		
    }
}
