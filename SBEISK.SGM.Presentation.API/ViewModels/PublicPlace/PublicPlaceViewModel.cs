using SBEISK.SGM.Presentation.API.ViewModels.City;

namespace SBEISK.SGM.Presentation.API.ViewModels.PublicPlace
{
    public class PublicPlaceViewModel
    {
        public string Cep{ get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Neighborhood { get; set; }
        public CityViewModel City { get ; set;}
    }
}