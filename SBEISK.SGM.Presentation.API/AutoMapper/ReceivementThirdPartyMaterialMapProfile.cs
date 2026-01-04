using System.Linq;
using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ReceivementThirdPartyMaterialMapProfile : Profile
    {
        public ReceivementThirdPartyMaterialMapProfile()
        {   
            CreateMap<ReceivementThirdPartyMaterialAsDraftRequest, ReceivementInvoiceOrderProjection>();
            CreateMap<ReceivementThirdPartyMaterialRequest, ReceivementInvoiceOrderProjection>();

              CreateMap<ReceivementThirdPartyMaterialAsDraftRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.DocumentDate))
                .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
                .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => true))
                .ForMember(x => x.ReceivementAttachments, map => map.Ignore())
                .ForMember(x => x.ReceivementPhotos, map => map.Ignore())
                .ForMember(x => x.Invoice, map => map.MapFrom(x => x.DocumentNumber))
                .ForMember(x => x.DriverNumber, map => map.MapFrom(x => x.DriverTelephone))
                .ForMember(x => x.ReceivementProviderReasons, map => map.Ignore());

            CreateMap<ReceivementThirdPartyMaterialRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.DocumentDate))
                .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
                .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => true))
                .ForMember(x => x.UserId, map => map.MapFrom(x => x.ReceiverUser))
                .ForMember(x => x.ReceivementAttachments, map => map.Ignore())
                .ForMember(x => x.ReceivementPhotos, map => map.Ignore())
                .ForMember(x => x.DriverNumber, map => map.MapFrom(x => x.DriverTelephone))
                .ForMember(x => x.ReceivementProviderReasons, map => map.Ignore());

        }
    }
}