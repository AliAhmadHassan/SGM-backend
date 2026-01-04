using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.Installation;
using SBEISK.SGM.Presentation.API.ViewModels.SelectList;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class InstallationMapProfile : Profile
    {
        public InstallationMapProfile()
        {
            CreateMap<Installation, InstallationViewModel>()
            .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.ThirdMaterialPermission, opt => opt.MapFrom(x => x.ThirdMaterial))
            .ForMember(x => x.Address, opt => opt.MapFrom(x => x.Address.Description))
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.InstallationType.Description))
            .ForMember(x => x.Project, opt => opt.MapFrom(x => x.Project.Description))
            .ForMember(x => x.ThirdMaterialPermission, opt => opt.MapFrom(x => x.ThirdMaterial ? "Sim" : "Não"))
            .ReverseMap();

            CreateMap<InstallationRequestViewModel, Installation>()
            .ForMember(x => x.ThirdMaterial, opt => opt.MapFrom(x => x.ThirdMaterialPermission))
            .ReverseMap();

            CreateMap<Installations, InstallationViewModel>()
            .ForMember(x => x.Code, opt => opt.MapFrom(x => x.Id))
            .ForMember(x => x.Type, opt => opt.MapFrom(x => x.TypeDescription))
            .ForMember(x => x.Project, opt => opt.MapFrom(x => x.ProjectDescription))
            .ForMember(x => x.ThirdMaterialPermission, opt => opt.MapFrom(x => x.ThirdMaterial))
            .ReverseMap();

            CreateMap<Installation, SelectItem<int, string>>().ConstructUsing(x => new SelectItem<int, string>(x.Id, x.Name));

            CreateMap<Installation, Installation>()
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore());

            CreateMap<Installation, UserProfileInstallation>()
                .ForPath(ui => ui.Installation.Name, map => map.MapFrom(i => i.Name));
        }
    }
}
