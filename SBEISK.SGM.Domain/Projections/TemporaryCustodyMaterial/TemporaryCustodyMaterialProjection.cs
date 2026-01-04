namespace SBEISK.SGM.Domain.Projections.TemporaryCustodyMaterial
{
    public class TemporaryCustodyMaterialProjection
    {
        public string MaterialCode { get; set; }
        public decimal Amount { get; set; }
        public int ReasonOfTemporaryCustodyId { get; set; }
    }
}