using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Action;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class ActionMapProfile : Profile
    {
        public ActionMapProfile()
        {
            CreateMap<Action, ActionResponseViewModel>();
        }
    }
}