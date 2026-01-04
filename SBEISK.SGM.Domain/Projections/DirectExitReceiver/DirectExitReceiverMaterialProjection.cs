namespace SBEISK.SGM.Domain.Projections.DirectExitReceiver
{
    public class DirectExitReceiverMaterialProjection
    {
        public decimal Amount { get; set; }
        public string MaterialCode { get; set; }
        public int DisciplineId { get; set; }
    }
}