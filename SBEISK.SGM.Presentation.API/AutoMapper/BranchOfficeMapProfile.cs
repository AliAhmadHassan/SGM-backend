using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.BranchOffice;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class BranchOfficeMapProfile : Profile
    {
        public BranchOfficeMapProfile()
        {
            CreateMap<BranchOffice, BranchOfficeViewModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.Description))
                .ReverseMap();
        }
    }
}
