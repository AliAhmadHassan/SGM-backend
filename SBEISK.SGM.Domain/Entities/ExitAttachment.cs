using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ExitAttachment : Entity
    {
        public byte[] Document { get; set; }
        public bool Fiscal { get; set; }
        public bool Authorization { get; set; }
        public int DirectExitId { get; set; }
        public DirectExit DirectExit { get; set; }
    }
}