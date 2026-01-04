namespace SBEISK.SGM.Presentation.API.ViewModels.Material
{
    public class WithoutOrder
    {
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public decimal ReceivementAmount { get; set; }
        public decimal UnityPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}