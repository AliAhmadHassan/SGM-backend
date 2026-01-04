using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Project;
using SBEISK.SGM.Presentation.API.ViewModels.Receiver;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ReceiverProfile : Profile
    {
        public ReceiverProfile()
        {
            CreateMap<Receiver, ReceiverViewModel>()
                .ForMember(x => x.ReceiverTypeText, opt => opt.MapFrom(x => x.ReceiverType.Description))
            .ReverseMap();

            CreateMap<ReceiverRequestViewModel, Receiver>();
        }
    }
}