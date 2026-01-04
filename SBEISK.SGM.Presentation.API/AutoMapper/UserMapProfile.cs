using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Presentation.API.ViewModels;
using SBEISK.SGM.Presentation.API.ViewModels.User;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<User, UserRequestProjection>().ReverseMap()
                .ForMember(u => u.UserProfileInstallations, map => map.MapFrom(u => u.Associations));

            CreateMap<User, UserRequestViewModel>().ReverseMap()
                .ForMember(u => u.UserProfileInstallations, map => map.MapFrom(u => u.Associations));

                

            CreateMap<UserRequestProjection, UserRequestViewModel>().ReverseMap()
                .ForMember(u => u.Associations, map => map.MapFrom(u => u.Associations));

            CreateMap<int, UserProfileInstallation>().ForMember(u => u.InstallationId, map => map.MapFrom(u => u));
                    

            CreateMap<User, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom((x, y) => x.Email ?? y.Email));

            CreateMap<User, UserViewModel>()
                .ForMember(u => u.ProfileInstallation, map => map.MapFrom(u => u.UserProfileInstallations))
                .AfterMap((user, viewmodel) => {
                    List<String> installationProfile = new List<String>();
                    foreach(var item in user.UserProfileInstallations.Where(x => x.Installation != null).Select(ins => new { installationName = ins.Installation.Name, profileName = ins.UserProfile.Name}))
                    {
                        installationProfile.Add($"{item.installationName}({item.profileName})");
                    }
                    viewmodel.NameInstallations = string.Join(", ", installationProfile);
                });
        }
    }
}