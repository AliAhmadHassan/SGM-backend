using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class DirectExitReceiverEmail : Entity
    {
        public string Email { get; set; }
        public int DirectExitReceiverId { get; set; }
        public DirectExitReceiver DirectExitReceiver { get; set; }
    }
}