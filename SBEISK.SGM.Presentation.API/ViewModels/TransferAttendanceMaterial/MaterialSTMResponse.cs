namespace SBEISK.SGM.Presentation.API.ViewModels.TransferAttendanceMaterial
{
    public class MaterialSTMResponse
    {
        public string MaterialCode { get; set; }
        public string Description { get; set; }
        public string Unity { get; set; }
        public string Discipline { get; set; }
        public decimal Requested { get; set; }

        public int Item { get; set; }
        public decimal Amount { get; set; }
    }
}
