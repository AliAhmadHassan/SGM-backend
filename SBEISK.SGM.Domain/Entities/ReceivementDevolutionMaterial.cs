using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementDevolutionMaterial : Entity
    {
        public decimal Amount { get; set; }
        public string AddtionalControl { get; set; }
        public int MaterialId { get; set; }
        public int ReceivementDevolutionReceiverId { get; set; }
        public int MaterialStatusId { get; set; }
        public int DevolutionReasonId { get; set; }
        public Material Material { get; set; }
        public ReceivementDevolutionReceiver ReceivementDevolutionReceiver { get; set; }
        public MaterialStatus MaterialStatus { get; set; }
        public DevolutionReason DevolutionReason { get; set; }
    }
}