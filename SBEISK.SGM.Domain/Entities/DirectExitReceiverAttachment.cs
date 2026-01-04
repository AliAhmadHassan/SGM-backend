using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DirectExitReceiverAttachment : Entity
    {
        public byte[] Document { get; set; }
        public int DirectExitReceiverId { get; set; }
        public DirectExitReceiver DirectExitReceiver { get; set; }
    }
}