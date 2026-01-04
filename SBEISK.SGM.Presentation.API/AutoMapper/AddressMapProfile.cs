using AutoMapper;
using SBEISK.SGM.Domain.Entities;
using SBEISK.SGM.Presentation.API.ViewModels.Address;
using SBEISK.SGM.Presentation.API.ViewModels.City;
using SBEISK.SGM.Presentation.API.ViewModels.Uf;

namespace SBEISK.SGM.Presentation.API.AutoMapper
{
    public class AddressMapProfile : Profile
    {
        public AddressMapProfile()
        {
            CreateMap<Address, AddressViewModel>()
                .ForMember(t => t.City, map => map.MapFrom(t =>  new CityViewModel(){
                    Id = t.City.Id,
                    Name = t.City.Name
                }))
                .ForMember(t => t.Uf, map => map.MapFrom(t =>  new UfResponseViewModel(){
                    Id = t.Uf.Id,
                    Name = t.Uf.Initials
                }));
            
            CreateMap<AddressRequestViewModel, Address>();

            CreateMap<Address, Address>()
            .ForMember(x => x.CreatedAt, map => map.Ignore())
            .ForMember(x => x.UpdatedAt, map => map.Ignore());
        }
    }
}