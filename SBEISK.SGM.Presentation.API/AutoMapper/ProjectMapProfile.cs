using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.BranchOffice;
using SBEISK.SGM.Presentation.API.ViewModels.Project;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ProjectMapProfile : Profile
    {
        public ProjectMapProfile()
        {
            CreateMap<Project, ProjectViewModel>()
                .ForMember(t => t.BranchOffice, map => map.MapFrom(t => new BranchOfficeResponseViewModel(){
                    Id = t.BranchOffice.Id,
                    Description = t.BranchOffice.Description
                }));

            CreateMap<ProjectRequestViewModel, Project>();

            CreateMap<Project, Project>()
                .ForMember(p => p.CreatedAt, map => map.Ignore())
                .ForMember(p => p.UpdatedAt, map => map.Ignore());
        }
    }
}