using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class STMEmail : Entity
    {
        public int UserId { get; set; }
        public int STMId { get; set; }
        public User User { get; set; }
        public STM STM { get; set; }
    }
}