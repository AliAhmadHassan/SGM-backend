
using SBEISK.SGM.Presentation.API.ViewModels.City;
using SBEISK.SGM.Presentation.API.ViewModels.Uf;

namespace SBEISK.SGM.Presentation.API.ViewModels.Address
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string PublicPlace { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }
        public string Neighborhood { get; set; }
        public string Complement { get; set; }
        public string Reference { get; set; }
        public string Cep { get; set; }
        public CityViewModel City { get; set; }
        public UfResponseViewModel Uf { get; set; }
    }
}