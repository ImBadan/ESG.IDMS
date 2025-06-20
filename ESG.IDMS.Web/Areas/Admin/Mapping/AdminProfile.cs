using AutoMapper;
using ESG.IDMS.Web.Areas.Admin.Commands.Entities;
using ESG.IDMS.Web.Areas.Admin.Models;
using ESG.Common.Data;
using Microsoft.AspNetCore.Identity;
using ESG.IDMS.Core.Identity;

namespace ESG.IDMS.Web.Areas.Admin.Mapping;

public class AdminProfile : Profile
{
    public AdminProfile()
    {
        CreateMap<Entity, EntityViewModel>().ReverseMap();
        CreateMap<EntityViewModel, AddOrEditEntityCommand>();
        CreateMap<AddOrEditEntityCommand, Entity>();

        CreateMap<ApplicationRole, RoleViewModel>().ReverseMap();

        CreateMap<Audit, AuditLogViewModel>();
        CreateMap<ApplicationUser, AuditLogUserViewModel>();
    }
}
