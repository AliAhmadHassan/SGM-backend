using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Uf;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class UfProfile : Profile
    {
        public UfProfile()
        {
            CreateMap<Uf, UfViewModel>();
        }
    }
}