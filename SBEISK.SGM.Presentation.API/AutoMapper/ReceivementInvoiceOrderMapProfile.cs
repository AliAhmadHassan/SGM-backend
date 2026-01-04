using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ReceivementInvoiceOrderMapProfile : Profile
    {
        public ReceivementInvoiceOrderMapProfile()
        {
            CreateMap<ReceivementInvoiceOrders, ReceivementInvoiceOrderViewModel>()
            .ReverseMap();

            CreateMap<ReceivementInvoiceOrder, ReceivementInvoiceOrderRequest>()
                .ForMember(x => x.InvoiceDate, map => map.MapFrom(x => x.InvoiceCreatedAt))
                .ForMember(x => x.ReceiverUser, map => map.MapFrom(x => x.ReceiverUser))
                .ForMember(x => x.DriverTelephone, map => map.MapFrom(x => x.DriverNumber));

            CreateMap<ReceivementInvoiceOrder, ReceivementInvoiceOrders>()
                 .ForMember(x => x.BranchOfficeDescription, map => map.MapFrom(x => x.Installation.Project.BranchOffice.Description))
                .ForMember(x => x.CNPJ, map => map.MapFrom(x => x.ReceiverUser))
                .ForMember(x => x.OrderCode, map => map.MapFrom(x => x.DriverNumber))
                .ForMember(x => x.OrderEmission, map => map.MapFrom(x => x.DriverNumber))
                .ForMember(x => x.OrderStatus, map => map.MapFrom(x => x.Order.OrderStatusId))
                .ForMember(x => x.ProviderName, map => map.MapFrom(x => x.Order.Provider.CompanyName));

            CreateMap<ReceivementInvoiceOrderRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.InvoiceDate))
                .ForMember(x => x.ReceivementDate, map => map.MapFrom(x => x.ReceivementDate))
                .ForMember(x => x.OrderId, map => map.MapFrom(x => x.OrderId))
                .ForMember(x => x.DriverNumber, map => map.MapFrom(x => x.DriverTelephone))
                .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
                .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => false))
                .ForMember(x => x.ReceivementAttachments, map => map.Ignore())
                .ForMember(x => x.ReceivementPhotos, map => map.Ignore())
                .ReverseMap();


            CreateMap<ReceivementInvoiceOrderDraftRequest, ReceivementInvoiceOrder>()
            .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.InvoiceDate))
            .ForMember(x => x.ReceivementDate, map => map.MapFrom(x => x.ReceivementDate))
            .ForMember(x => x.DriverNumber, map => map.MapFrom(x => x.DriverTelephone))
            .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
            .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => false))
            .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
            .ReverseMap();

            CreateMap<ReceivementInvoiceOrderRequest, ReceivementInvoiceOrderProjection>();

            CreateMap<ReceivementInvoiceOrder, ReceivementInvoiceOrder>()
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore());


            CreateMap<ReceivementInvoiceOrderDraftRequest, ReceivementInvoiceOrderProjection>();


            CreateMap<ReceivementInvoiceOrderDraftRequest, ReceivementInvoiceOrderProjection>();


            CreateMap<ReceivementInvoiceWithoutOrderDraft, ReceivementInvoiceWithoutOrderProjection>()
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true));

            CreateMap<ReceivementInvoiceWithoutOrderRequest, ReceivementInvoiceWithoutOrderProjection>()
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false));

            CreateMap<ReceivementInvoiceWithoutOrderRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.InvoiceDate))
                .ForMember(x => x.ReceivementDate, map => map.MapFrom(x => x.ReceivementDate))
                .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
                .ForMember(x => x.ReceivementAttachments, map => map.Ignore())
                .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => false))
                .ForMember(x => x.ReceivementPhotos, map => map.Ignore())
                .ForMember(x => x.ReceivementProviderReasons, map => map.Ignore());

            CreateMap<ReceivementInvoiceWithoutOrderDraft, ReceivementInvoiceOrder>()
                .ForMember(x => x.InvoiceCreatedAt, map => map.MapFrom(x => x.InvoiceDate))
                .ForMember(x => x.ReceivementDate, map => map.MapFrom(x => x.ReceivementDate))
                .ForMember(x => x.Note, map => map.MapFrom(x => x.Comments))
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
                .ForMember(x => x.ThirdPartyMaterial, map => map.MapFrom(x => false))
                .ForMember(x => x.ReceivementAttachments, map => map.Ignore())
                .ForMember(x => x.ReceivementPhotos, map => map.Ignore())
                .ForMember(x => x.ReceivementProviderReasons, map => map.Ignore());

            CreateMap<ReceivementInvoiceOrder, ReceivementInvoiceWithoutOrderViewModel>()
                .ForMember(x => x.Provider, map => map.MapFrom(x => string.Join(" - ", x.Installation.Name, x.Installation.Description)))
                .ForMember(x => x.InvoiceNumber, map => map.MapFrom(x => x.Invoice))
                .ForMember(x => x.InvoiceDate, map => map.MapFrom(x => x.InvoiceCreatedAt))
                .ForMember(x => x.ReceiverUser, map => map.MapFrom(x => x.UserReceivementInvoiceOrder.Name))
                .ForMember(x => x.ReceivementDate, map => map.MapFrom(x => x.ReceivementDate));

            CreateMap<DevolutionDraftRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => true));

            CreateMap<DevolutionRequest, ReceivementInvoiceOrder>()
                .ForMember(x => x.IsDraft, map => map.MapFrom(x => false));
        }
    }
}