using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.Material;
using SBEISK.SGM.Presentation.API.ViewModels.Project;
using SBEISK.SGM.Presentation.API.ViewModels.Receivement;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ReceivementProfile : Profile
    {
        public ReceivementProfile()
        {
            CreateMap<ReceivementInvoiceOrders, ReceivementInvoiceOrderViewModel>()
            .ReverseMap();

            CreateMap<ReceiverRequestViewModel, Receiver>();

            CreateMap<ReceivementMaterial, WithoutOrder>()
                .ForMember(x => x.MaterialCode, map => map.MapFrom(x => x.Material.Code))
                .ForMember(x => x.Description, map => map.MapFrom(x => x.Material.Description))
                .ForMember(x => x.Unit, map => map.MapFrom(x => x.Material.Unity))
                .ForMember(x => x.UnityPrice, map => map.MapFrom(x => x.Material.UnitCost));
        }
    }
}