using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels;
using SBEISK.SGM.Presentation.API.ViewModels.DirectExitTransfer;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class DirectExitMapProfile : Profile
    {
        public DirectExitMapProfile()
        {
            CreateMap<DirectExitReceiverRequest, DirectExitReceiver>()
                .ForMember(e => e.InstallationId, map => map.MapFrom(e => e.InstallationSourceId))
                .ForMember(e => e.Attachments, map => map.Ignore())
                .ForMember(e => e.Emails, map => map.Ignore())
                .ForMember(e => e.DirectExitReceiverMaterials, map => map.Ignore());   

            CreateMap<DirectExitTransferRequest, DirectExit>()
                .ForMember(x => x.InstallationDestinyId, map => map.MapFrom(x => x.InstallationIdDestiny))
                .ForMember(x => x.InstallationSourceId, map => map.MapFrom(x => x.InstallationIdSource))
                .ForMember(x => x.UserDeliveryId, map => map.MapFrom(x => x.UserIdResponsableDelivery))
                .ForMember(x => x.UserWithdrawId, map => map.MapFrom(x => x.UserIdResponsableWithdraw))
                .ForMember(x => x.ContactDelivery, map => map.MapFrom(x => x.DeliveryContact))
                .ForMember(x => x.TelephoneDelivery, map => map.MapFrom(x => x.ContactPhone))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
                .ForMember(x => x.Photos, map => map.Ignore())
                .ForMember(x => x.Attachments, map => map.Ignore())
                .ForMember(x => x.Emails, map => map.Ignore())
                .ForMember(x => x.ExitMaterials, map => map.Ignore());   

            CreateMap<DirectExitTransferDraftRequest, DirectExit>()
                .ForMember(x => x.InstallationDestinyId, map => map.MapFrom(x => x.InstallationIdDestiny))
                .ForMember(x => x.InstallationSourceId, map => map.MapFrom(x => x.InstallationIdSource))
                .ForMember(x => x.UserDeliveryId, map => map.MapFrom(x => x.UserIdResponsableDelivery))
                .ForMember(x => x.UserWithdrawId, map => map.MapFrom(x => x.UserIdResponsableWithdraw))
                .ForMember(x => x.ContactDelivery, map => map.MapFrom(x => x.DeliveryContact))
                .ForMember(x => x.TelephoneDelivery, map => map.MapFrom(x => x.ContactPhone))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
                .ForMember(x => x.Photos, map => map.Ignore())
                .ForMember(x => x.Attachments, map => map.Ignore())
                .ForMember(x => x.Emails, map => map.Ignore())
                .ForMember(x => x.ExitMaterials, map => map.Ignore());
        }
    }
}