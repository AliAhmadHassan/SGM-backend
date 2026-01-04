namespace SBEISK.SGM.Presentation.API.ViewModels.RMA
{
    public class MaterialRequisition
    {
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Unity { get; set; }
        public string Discipline { get; set; }
        public decimal Requested { get; set; }
        public decimal Attended { get; set; }
        public decimal Pendency { get; set; }
    }
}
