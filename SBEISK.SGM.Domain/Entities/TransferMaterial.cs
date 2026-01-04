using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class TransferMaterial : Entity
    {
        public decimal Amount { get; set; }
        public int TransferId { get; set; }
        public int MaterialId { get; set; }
        public int STMMaterialId { get; set; }
        public Transfer Transfer { get; set; }
        public Material Material { get; set; }
        public STMMaterial STMMaterial { get; set; }
    }
}