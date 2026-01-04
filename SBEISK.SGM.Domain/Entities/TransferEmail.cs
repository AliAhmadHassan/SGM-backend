using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class TransferEmail : Entity
    {
        public int UserId { get; set; }
        public int TransferId { get; set; }
        public User User { get; set; }
        public Transfer Transfer { get; set; }
    }
}