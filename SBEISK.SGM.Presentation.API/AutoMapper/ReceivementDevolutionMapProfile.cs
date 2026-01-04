using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ReceivementDevolutionMapProfile : Profile
    {
        public ReceivementDevolutionMapProfile()
        {
            CreateMap<DevolutionRequest, ReceivementDevolutionReceiver>()
                .ForMember(d => d.IsDraft, map => map.MapFrom(d => false))
                .ForMember(d => d.Emails, map => map.Ignore())
                .ForMember(d => d.Attachments, map => map.Ignore())
                .ForMember(d => d.DevolutionMaterialsReceiver, map => map.Ignore());

            CreateMap<DevolutionDraftRequest, ReceivementDevolutionReceiver>()
                .ForMember(d => d.IsDraft, map => map.MapFrom(d => true))
                .ForMember(d => d.Emails, map => map.Ignore())
                .ForMember(d => d.Attachments, map => map.Ignore())
                .ForMember(d => d.DevolutionMaterialsReceiver, map => map.Ignore());
        }
    }
}