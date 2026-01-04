using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Divergence;
using SBEISK.SGM.Presentation.API.ViewModels.FileExample;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class DivergenceMapProfile : Profile
    {
        public DivergenceMapProfile()
        {
            CreateMap<Divergence, DivergenceRequestViewModel>()
            .ForMember(x => x.File, map => map.Ignore())
            .ReverseMap();

            CreateMap<DivergenceFiles, DivergenceFileRequestViewModel>().ReverseMap();
        }
    }
}