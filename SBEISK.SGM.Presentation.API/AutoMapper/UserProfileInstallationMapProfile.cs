using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.Installation;
using SBEISK.SGM.Presentation.API.ViewModels.Profiles;
using SBEISK.SGM.Presentation.API.ViewModels.UserProfileInstallation;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class UserProfileInstallationMapProfile : Profile
    {
        public UserProfileInstallationMapProfile()
        {
            CreateMap<UserProfileInstallationRequest, UserProfileInstallation>();
            CreateMap<MasterUserResponseViewModel, UserProfileInstallation>();

            CreateMap<InstallationResponseViewModel, MasterUserResponseViewModel>()
                .ForMember(i => i.InstallationId, map => map.MapFrom(u => u.Id))
                .ForMember(i => i.InstallationName, map => map.MapFrom(u => u.Name))
                .ForMember(i => i.ProfileId, map => map.MapFrom(u => -1))
                .ForMember(i => i.ProfileName, map => map.MapFrom(u => "Master"));

            CreateMap<UserProfileInstallation, InstallationResponseViewModel>()
                .ForMember(i => i.Id, map => map.MapFrom(u => u.Installation.Id))
                .ForMember(i => i.Name, map => map.MapFrom(u => u.Installation.Name));

            CreateMap<UserProfileInstallation, UserProfile>()
                .ForMember(i => i.Id, map => map.MapFrom(u => u.UserProfile.Id))
                .ForMember(i => i.Name, map => map.MapFrom(u => u.UserProfile.Name));

            CreateMap<UserProfileInstallation, UserResponseViewModel>()
                .ForMember(u => u.InstallationId, map => map.MapFrom(u => u.Installation.Id))
                .ForMember(u => u.InstallationName, map => map.MapFrom(u => u.Installation.Name))
                .ForMember(u => u.ProfileId, map => map.MapFrom(u => u.UserProfile.Id))
                .ForMember(u => u.ProfileName, map => map.MapFrom(u => u.UserProfile.Name));

            CreateMap<Domain.Projections.UserProfileInstallations.Association, Association>().ReverseMap();

            CreateMap<UserProfileInstallation, ViewModels.UserProfileInstallation.Association>().ReverseMap();
        }
    }
}
