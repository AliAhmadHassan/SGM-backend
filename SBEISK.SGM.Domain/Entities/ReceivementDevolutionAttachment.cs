using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementDevolutionAttachment : Entity
    {
        public byte[] Document { get; set; }
        public int ReceivementDevolutionReceiverId { get; set; }
        public ReceivementDevolutionReceiver ReceivementDevolutionReceiver { get; set; }
    }
}