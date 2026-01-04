using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Projections.Material;
using SBEISK.SGM.Presentation.API.ViewModels.Material;
using SBEISK.SGM.Presentation.API.ViewModels.ReceivementInvoiceOrder;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class MaterialMapProfile : Profile
    {
        public MaterialMapProfile()
        {
            CreateMap<Material, MaterialViewModel>();
            CreateMap<MaterialStatus, MaterialStatusViewModel>();
            CreateMap<MaterialWithoutOrder, MaterialWithoutOrderProjection>();
            CreateMap<Material, MaterialWithoutOrderViewModel>()
                .ForMember(x => x.MaterialCode, map => map.MapFrom(x => x.Code))
                .ForMember(x => x.MaterialDescription, map => map.MapFrom(x => x.Description))
                .ForMember(x => x.MaterialUnit, map => map.MapFrom(x => x.Unity));

            CreateMap<Material, WithoutOrder>()
                .ForMember(x => x.MaterialCode, map => map.MapFrom(x => x.Code))
                .ForMember(x => x.Description, map => map.MapFrom(x => x.Description))
                .ForMember(x => x.Unit, map => map.MapFrom(x => x.Unity))
                .ForMember(x => x.ReceivementAmount, map => map.Ignore())
                .ForMember(x => x.ReceivementAmount, map => map.Ignore())
                .ForMember(x => x.TotalPrice, map => map.Ignore());

            CreateMap<WithoutOrderProjection, WithoutOrder>();              
        }
    }
}