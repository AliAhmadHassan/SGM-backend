using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitMaterial : Entity
    {
        public decimal Amount { get; set; }
        public int DirectExitId { get; set; }
        public int MaterialId { get; set; }
        public DirectExit DirectExit { get; set; }
        public Material Material { get; set; }
    }
}