using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Presentation.API.ViewModels.Action;
using SBEISK.SGM.Presentation.API.ViewModels.Address;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class AcionMapProfile : Profile
    {
        public AcionMapProfile()
        {
            CreateMap<Action, ActionResponseViewModel>()
            .ForMember(a => a.Id, map => map.MapFrom(av => av.Id))
            .ForMember(a => a.Description, map => map.MapFrom(av => av.Description));

            CreateMap<ParentPermissionsProjection, ParentActionViewModel>()
                .ForMember(p => p.Id, map => map.MapFrom(pp => pp.Id))
                .ForMember(p => p.Description, map => map.MapFrom(pp => pp.Description))
                .ForMember( p => p.Actions, map => map.MapFrom(pp => pp.Actions));
        }
    }
}