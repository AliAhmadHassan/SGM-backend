namespace SBEISK.SGM.Domain.Projections.Material
{
    public class MaterialWithoutOrderProjection
    {
        public string MaterialCode { get; set; }
        public decimal ReceivementAmount { get; set; }
        public decimal UnityPrice { get; set; }
    }
}