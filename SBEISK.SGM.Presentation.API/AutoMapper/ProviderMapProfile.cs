using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Provider;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ProviderProfile : Profile
    {
        public ProviderProfile()
        {
            CreateMap<Provider, ProviderViewModel>().ReverseMap();
        }
    }
}