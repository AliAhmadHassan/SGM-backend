using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.STM;
using SBEISK.SGM.Presentation.API.ViewModels.Transfer;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class TransferMapProfile : Profile
    {
        public TransferMapProfile()
        {
            CreateMap<TransferRequestViewModel, Transfer>()
                .ForMember(t => t.UserReceiverId, map => map.MapFrom(t => t.UserReceiverId))
                .ForMember(t => t.PrevisionDate, map => map.MapFrom(t => t.ReceivementDate))
                .ForMember(t => t.InvoiceDate, map => map.MapFrom(t => t.InvoiceDate))
                .ForMember(t => t.IsDraft, map => map.MapFrom(x => false))
                .ForMember(t => t.Emails, map => map.Ignore())
                .ForMember(t => t.Attachments, map => map.Ignore())
                .ForMember(t => t.Photos, map => map.Ignore())
                .ForMember(t => t.TransferMaterials, map => map.Ignore());

            CreateMap<TransferRequestDraftViewModel, Transfer>()
                .ForMember(t => t.UserReceiverId, map => map.MapFrom(t => t.UserReceiverId))
                .ForMember(t => t.PrevisionDate, map => map.MapFrom(t => t.ReceivementDate))
                .ForMember(t => t.InvoiceDate, map => map.MapFrom(t => t.InvoiceDate))
                .ForMember(t => t.IsDraft, map => map.MapFrom(x => true))
                .ForMember(t => t.Emails, map => map.Ignore())
                .ForMember(t => t.Attachments, map => map.Ignore())
                .ForMember(t => t.Photos, map => map.Ignore())
                .ForMember(t => t.TransferMaterials, map => map.Ignore());

            CreateMap<Transfer, STMViewModel>()
                .ForMember(x => x.STM, map => map.MapFrom(x => x.STMId))
                .ForMember(x => x.InstallationDestiny, map => map.MapFrom(x => x.STM.InstallationDestiny.Name))
                .ForMember(x => x.InstallationSource, map => map.MapFrom(x => x.STM.InstallationSource.Name))
                .ForMember(x => x.Approver, map => map.MapFrom(x => x.STM.UserWithdraw.Name))
                .ForMember(x => x.Requester, map => map.MapFrom(x => x.STM.UserRequester.Name))
                .ForMember(x => x.Status, map => map.MapFrom(x => x.TransferStatus.Description))
                .ForMember(x => x.Transfer, map => map.MapFrom(x => x.Id))
                .ForMember(x => x.EmissionDate, map => map.MapFrom(x => x.STM.EmissionDate));
        }
    }
}