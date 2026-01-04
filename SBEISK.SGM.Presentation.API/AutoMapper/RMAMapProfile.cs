using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Domain.Projections.RMA;
using SBEISK.SGM.Presentation.API.ViewModels.RMA;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class RMAMapProfile : Profile
    {
        public RMAMapProfile()
        {
            CreateMap<RMARequest, RequisitionOfMaterialForApplication>()
            .ForMember(x => x.ReceiverUserId, map => map.MapFrom(x => x.ResponsableId))
            .ForMember(x => x.StatusId, map => map.MapFrom(x => 1))
            .ForMember(x => x.ApproverUserId, map => map.Ignore())
            .ForMember(x => x.Materials, map => map.Ignore())
            .ForMember(x => x.RMAattachments, map => map.Ignore());

            CreateMap<RMAMaterial, MaterialRMAResponse>()
            .ForPath(x => x.MaterialCode, map => map.MapFrom(x => x.Material.Code))
            .ForPath(x => x.Description, map => map.MapFrom(x => x.Material.Description))
            .ForPath(x => x.Unity, map => map.MapFrom(x => x.Material.Unity))
            .ForPath(x => x.Discipline, map => map.MapFrom(x => x.Discipline.Description))
            .ForMember(x => x.Requested, map => map.MapFrom(x => x.Quantity));

            CreateMap<RMA, RMAResponse>()
            .ReverseMap();

            CreateMap<RMADetails, RMADetailsResponse>()
            .ReverseMap();

            CreateMap<GenericFileClass, RMAAttendanceAttachments>().ForMember(x => x.RMAAttendanceId, map => map.MapFrom(x => x.ReferenceId));
            CreateMap<GenericFileClass, RMAattachments>().ForMember(x => x.RMAId, map => map.MapFrom(x => x.ReferenceId));
        }
    }
}
