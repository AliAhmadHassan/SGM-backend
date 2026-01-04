using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Domain.Entities.Views;
using SBEISK.SGM.Presentation.API.ViewModels.RMA;
using SBEISK.SGM.Presentation.API.ViewModels.RMAAttendance;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class RMAAttendanceMapProfile : Profile
    {
        public RMAAttendanceMapProfile()
        {
            CreateMap<RMAAttendances, RMAAttendancesWithItemsResponse>();
        
            CreateMap<RMAMaterial, MaterialRequisition>()
            .ForMember(x => x.Description, map => map.MapFrom(x => x.Material.Description))
            .ForMember(x => x.Unity, map => map.MapFrom(x => x.Material.Unity))
            .ForMember(x => x.Discipline, map => map.MapFrom(x => x.Discipline.Description))  
            .ForMember(x => x.Requested, map => map.MapFrom(x => x.Quantity))
            .ForMember(x => x.Attended, map => map.MapFrom(x => x.AmountReceived)) 
            .ForMember(x => x.Pendency, map => map.MapFrom(x => x.Quantity - x.AmountReceived));

            CreateMap<RMAAttendanceRequest, RMAAttendance>()
            .ForMember(x => x.IsDraft, map => map.MapFrom(x => false))
            .ForMember(x => x.Attachments, map => map.Ignore())
            .ForMember(x => x.Emails, map => map.Ignore());

            CreateMap<RMAAttendanceAsDraftRequest, RMAAttendance>()
            .ForMember(x => x.IsDraft, map => map.MapFrom(x => true))
            .ForMember(x => x.Attachments, map => map.Ignore())
            .ForMember(x => x.Emails, map => map.Ignore());

            //CreateMap<RMAMaterial, RMAMaterialRequest>(); 
        }
    }
}
