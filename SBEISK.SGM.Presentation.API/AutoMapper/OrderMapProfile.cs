using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Order;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class OrderMapProfile : Profile
    {
        public OrderMapProfile()
        {
            CreateMap<Order, OrderViewModel>();

            CreateMap<OrderItem, OrderItemViewModel>()
            .ForMember(x => x.Item, map => map.MapFrom(x => x.SequenceNumber))
            .ForMember(x => x.materialCode, map => map.MapFrom(x => x.Material.Code))
            .ForMember(x => x.ProviderProductCode, map => map.MapFrom(x => x.Material.SbeiCode))
            .ForMember(x => x.MaterialDescription, map => map.MapFrom(x => x.Material.Description))
            .ForMember(x => x.Quantity, map => map.MapFrom(x => x.Quantity))
            .ForMember(x => x.UnitaryCost, map => map.MapFrom(x => x.UnitaryCost))
            .ForMember(x => x.AmountReceived, map => map.MapFrom(x => x.AmountReceived))
            .ForMember(x => x.Unity, map => map.MapFrom(x => x.Material.Unity))
            .ForMember(x => x.TotalCost, map => map.MapFrom( x => x.Quantity * x.UnitaryCost))
            .ForMember(x => x.Pending, map => map.MapFrom( x => x.Quantity - x.AmountReceived))
            .ReverseMap();
        }
    }
}
