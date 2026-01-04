using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class STMAttachment : Entity
    {
        public byte[] Document { get; set; }
        public int STMId { get; set; }
        public STM STM { get; set; }
    }
}