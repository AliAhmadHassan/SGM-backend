using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DirectExitReceiverMaterial : Entity
    {
        public decimal Amount { get; set; }
        public int MaterialId { get; set; }
        public int DirectExitReceiverId { get; set; }
        public int DisciplineId { get; set; }
        public int ReasonOfTemporaryCustodyId { get; set; }
        public Material Material { get; set; }
        public DirectExitReceiver DirectExitReceiver { get; set; }
        public Discipline Discipline { get; set; }
    }
}