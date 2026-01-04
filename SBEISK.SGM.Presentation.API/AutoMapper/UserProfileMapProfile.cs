using System.Linq;
using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.Action;
using SBEISK.SGM.Presentation.API.ViewModels.Profiles;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class UserProfileMapProfile : Profile
    {
        public UserProfileMapProfile()
        {
            CreateMap<UserProfile, UserProfileViewModel>()
            .AfterMap((userProfile, userProfileViewModel) => {
                var configuration = (IConfigurationProvider)new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(UserProfileMapProfile));
                });
                
                var mapper = (IMapper)new Mapper(configuration);
                
                userProfileViewModel.Permissions = (userProfile.ProfileActions)
                .Select(p => mapper.Map<int>(p.Action.Id))
                .ToList();
            });


            CreateMap<ProfilePermissions, ProfilePermissionViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.ProfileId))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.ProfileName))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.ProfileDescription))
                .ForMember(x => x.Permissions, opt => opt.MapFrom(x => x.Permissions))
            .ReverseMap();

            CreateMap<UserProfileRequest, UserProfile>()
                .ForMember(x => x.ProfileActions, opt => opt.MapFrom(x => x.Permissions));
             
            CreateMap<int, ProfileAction>()
                .ForMember(x => x.ActionId, opt => opt.MapFrom(x => x));

            CreateMap<UserProfile, UserProfile>()
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore());

            CreateMap<ProfileAction, ProfileActionViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Action.Id))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Action.Description));

            CreateMap<UserProfile, ProfileResponseViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<UserProfile, UserProfileInstallation>()
                .ForPath(ui => ui.UserProfile.Name, map => map.MapFrom(i => i.Name));
        }
    }
}