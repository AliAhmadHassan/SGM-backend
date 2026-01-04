namespace SBEISK.SGM.Domain.Projections.Order
{
    public class OrderItemProjection
    {
        public int Item { get; set; }
        public string materialCode { get; set; }
        public string ProviderProductCode { get ;set; }
        public string MaterialDescription { get; set; }
        public string Unity { get; set;}
        public int Quantity { get; set; }
        public decimal UnitaryCost { get; set; }
        public decimal TotalCost { get; set; }
        public int AmountReceived { get; set; }
        public int Pending { get; set; }
    }
}