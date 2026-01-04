using SBEISK.SGM.Domain.Entities.Base;

namespace SBEISK.SGM.Domain.Entities
{
    public class TransferPhoto : Entity
    {
        public byte[] Photo { get; set; }
        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }
    }
}