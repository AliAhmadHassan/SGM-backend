namespace SBEISK.SGM.Domain.Projections.ReceivementDevolutionMaterial
{
    public class ReceivementDevolutionMaterialProjection
    {
        public string MaterialCode { get; set; }
        public decimal Amount { get; set; }
        public int MaterialStatusId { get; set; }
        public int DevolutionReasonId { get; set; }
        public string AdditionalController { get; set; }
    }
}