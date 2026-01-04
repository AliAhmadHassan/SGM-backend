using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class ReceivementDevolutionEmail : Entity
    {
        public string Email { get; set; }
        public int ReceivementDevolutionReceiverId { get; set; }
        public ReceivementDevolutionReceiver ReceivementDevolutionReceiver { get; set; }
    }
}