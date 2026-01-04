
namespace SBEISK.SGM.Domain.Projections.RMA
{
    public class RMAMaterialProjection
    {
        public string MaterialCode { get; set; }
        public int DisciplineId { get; set; }
        public decimal AmountReceived { get; set; }
    }
}