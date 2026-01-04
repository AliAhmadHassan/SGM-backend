using AutoMapper;
using SBEISK.SGM.Domain.Projections.User;
using SBEISK.SGM.Presentation.API.ViewModels.Menu;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class MenuMapProfile : Profile
    {
        public MenuMapProfile()
        {
            CreateMap<InstalationsBranchOffice, BranchOfficeViewModel>();
            CreateMap<Installation, InstallationsViewModel>();
        }
    }
}