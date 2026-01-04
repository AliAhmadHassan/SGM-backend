using AutoMapper;
using SBEISK.SGM.Domain.Queries.Login;
using SBEISK.SGM.Presentation.API.ViewModels.Login;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class LoginMapProfile : Profile
    {
        public LoginMapProfile()
        {
            CreateMap<LoginViewModel, LoginQuery>();
        }
    }
}
